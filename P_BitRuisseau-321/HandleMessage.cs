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
                        break;

                    case "sendMedia":
                    case "SEND_MEDIA":
                        HandleReceivedMedia(message);
                        break;
                }
            }
        }

        private static void HandleReceivedMedia(Message message)
        {
            if (string.IsNullOrEmpty(message.SongData) || string.IsNullOrEmpty(message.Hash))
                return;

            try
            {
                byte[] data = Convert.FromBase64String(message.SongData);
                string folder = Path.Combine(Application.StartupPath, "fileMp3");
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                string filePath = Path.Combine(folder, $"{message.Hash}.mp3");

                using (var stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    if (message.StartByte.HasValue)
                    {
                        stream.Seek(message.StartByte.Value, SeekOrigin.Begin);
                    }
                    stream.Write(data, 0, data.Length);
                }
                
                Trace.WriteLine($"Received {data.Length} bytes for {message.Hash}");
                MessageBox.Show($"Musique reçue et sauvegardée : {message.Hash}.mp3", "Téléchargement Réussi");
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Error handling received media: {ex.Message}");
            }
        }
    }
}
