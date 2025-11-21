using BitRuisseau;
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
        public string? Description { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }   

    public Song(string filePath)
        {
            Size = (int)new FileInfo(filePath).Length / 1024 / 1024;
            Hash = ComputeHash(filePath);
            FileName = (string)new FileInfo(filePath).Name;
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

