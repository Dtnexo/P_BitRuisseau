using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace BitRuisseau
{
    public static class HandleMessage
    {
        public static event Action? OnDownloadComplete;

        public static void Handle(Message message, IProtocol protocol)
        {
            if (message.Sender == Environment.MachineName) return;

            if (string.IsNullOrEmpty(message.Recipient) || message.Recipient == "0.0.0.0" || message.Recipient == Environment.MachineName)
            {
                switch (message.Action)
                {
                    case "online":
                    case "IM_ONLINE":
                        Trace.WriteLine($"Online: {message.Sender}");
                        if (!P_BitRuisseau_321.Program.mediathequeSongs.ContainsKey(message.Sender))
                        {
                            P_BitRuisseau_321.Program.mediathequeSongs.Add(message.Sender, new List<Song>());
                            protocol.AskCatalog(message.Sender);
                        }
                        break;

                    case "askOnline":
                    case "GET_ONLINE":
                        protocol.SayOnline();
                        break;

                    case "askCatalog":
                    case "ASK_CATALOG":
                        protocol.SendCatalog(message.Sender);
                        break;
                    
                    case "sendCatalog":
                    case "SEND_CATALOG":
                        if (message.SongList != null)
                        {
                             if (P_BitRuisseau_321.Program.mediathequeSongs.ContainsKey(message.Sender))
                            {
                                P_BitRuisseau_321.Program.mediathequeSongs[message.Sender] = message.SongList;
                                Trace.WriteLine($"Received catalog from {message.Sender} with {message.SongList.Count} songs.");
                            }
                            else
                            {
                                P_BitRuisseau_321.Program.mediathequeSongs.Add(message.Sender, message.SongList);
                            }
                        }
                        break;

                    case "askMedia":
                    case "ASK_MEDIA":
                        Song song = null;
                        lock (P_BitRuisseau_321.Program.MySongsLock)
                        {
                             song = P_BitRuisseau_321.Program.MySongs.FirstOrDefault(s => s.Hash == message.Hash);
                        }
                        if (song != null && File.Exists(song.FilePath))
                        {
                            long fileSize = new FileInfo(song.FilePath).Length;
                            long start = message.StartByte ?? 0;
                            // Ensure valid range
                            long end = message.EndByte ?? fileSize - 1;
                            if (end >= fileSize) end = fileSize - 1;

                            protocol.SendMedia(song, message.Sender, start, end);
                        }
                        break;

                    case "sendMedia":
                    case "SEND_MEDIA":
                        HandleReceivedMedia(message);
                        break;
                }
            }
        }

        private static readonly object _fileLock = new object();
        
        // Track progress: Hash -> (List of intervals, TotalSize?)
        private class TransferState
        {
            public List<(long Start, long End)> ReceivedIntervals = new List<(long, long)>();
            public long? TotalSize = null;
        }
        private static Dictionary<string, TransferState> _transfers = new Dictionary<string, TransferState>();

        private static void HandleReceivedMedia(Message message)
        {
            string folder = Path.Combine(Application.StartupPath, "fileMp3");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string partFilePath = Path.Combine(folder, $"{message.Hash}.part");
            string finalFilePath = Path.Combine(folder, $"{message.Hash}.mp3");

            try
            {
                byte[] data = Convert.FromBase64String(message.SongData);
                long start = message.StartByte ?? 0;
                long end = message.EndByte ?? (start + data.Length - 1);

                bool isDownloadComplete = false;
                
                // Utilisation d'un verrou pour garantir l'accès exclusif au fichier
                lock (_fileLock)
                {
                    // Si c'est le début du téléchargement (octet 0)
                    // Nettoyage de l'état précédent pour garantir un départ propre
                    if (start == 0)
                    {
                        if (File.Exists(partFilePath)) File.Delete(partFilePath);
                        // Suppression du fichier final existant pour éviter toute confusion
                        if (File.Exists(finalFilePath)) File.Delete(finalFilePath);
                        if (_transfers.ContainsKey(message.Hash)) _transfers.Remove(message.Hash);
                    }

                    // Écriture des données reçues dans le fichier temporaire .part
                    using (var stream = new FileStream(partFilePath, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        stream.Seek(start, SeekOrigin.Begin);
                        stream.Write(data, 0, data.Length);
                    }

                    // Vérification de la complétion du téléchargement
                    // Cas simple : réception du premier morceau marqué comme dernier
                    if (start == 0 && message.IsLastChunk.GetValueOrDefault(true))
                    {
                        isDownloadComplete = true;
                    }
                    else
                    {
                        // Cas complexe : téléchargement fragmenté
                        // Suivi des morceaux reçus
                         if (!_transfers.ContainsKey(message.Hash))
                        {
                            _transfers[message.Hash] = new TransferState();
                        }
                        var state = _transfers[message.Hash];
                        
                        state.ReceivedIntervals.Add((start, end));

                        if (message.IsLastChunk.GetValueOrDefault(true))
                        {
                            state.TotalSize = end + 1;
                        }

                        // Tri des intervalles pour vérifier la continuité
                        state.ReceivedIntervals.Sort((a, b) => a.Start.CompareTo(b.Start));
                        var merged = new List<(long Start, long End)>();
                        foreach (var interval in state.ReceivedIntervals)
                        {
                            if (merged.Count == 0) merged.Add(interval);
                            else
                            {
                                var last = merged[merged.Count - 1];
                                if (last.End + 1 >= interval.Start)
                                {
                                    merged[merged.Count - 1] = (last.Start, Math.Max(last.End, interval.End));
                                }
                                else
                                {
                                    merged.Add(interval);
                                }
                            }
                        }
                        state.ReceivedIntervals = merged;

                        // Validation : vérification si la totalité du fichier est reçue (de 0 à la fin)
                        if (state.TotalSize.HasValue && state.ReceivedIntervals.Count == 1)
                        {
                            var interval = state.ReceivedIntervals[0];
                            if (interval.Start == 0 && interval.End == (state.TotalSize.Value - 1))
                            {
                                isDownloadComplete = true;
                            }
                        }
                    }

                    if (isDownloadComplete)
                    {
                        if (_transfers.ContainsKey(message.Hash)) _transfers.Remove(message.Hash);
                        
                        // Finalisation : renommage du fichier .part en .mp3
                        // Le flux est fermé avant cette opération
                        if (File.Exists(finalFilePath)) File.Delete(finalFilePath);
                        File.Move(partFilePath, finalFilePath);
                    }
                }

                if (isDownloadComplete)
                {
                    MessageBox.Show($"Téléchargement terminé pour {message.Hash}.mp3 !", "Succès");
                    OnDownloadComplete?.Invoke();
                }
                
                Trace.WriteLine($"Received chunk {start}-{end} for {message.Hash}. Intervals: { _transfers.GetValueOrDefault(message.Hash)?.ReceivedIntervals.Count }");
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Error receiving media: " + ex.Message);
            }
        }
    }
}
