using P_BitRuisseau_321;
using System;
using System.IO;
using System.Security.Cryptography;

    public class Song : ISong
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public int Year { get; set; }
        public TimeSpan Duration { get; set; }
        public int Size { get; set; }
        public string[] Featuring { get; set; }
        public string Hash { get; private set; }

        // Constructeur appelé après lecture
        public Song(string filePath)
        {
            Size = (int)new FileInfo(filePath).Length;
            Hash = ComputeHash(filePath);
        }

        private string ComputeHash(string filePath)
        {
            using (var sha = SHA256.Create())
            using (var stream = File.OpenRead(filePath))
            {
                var hashBytes = sha.ComputeHash(stream);
                return BitConverter.ToString(hashBytes).Replace("-", "");
            }
        }
    }

