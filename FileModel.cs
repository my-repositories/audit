using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace Audit
{
    public class FileModel
    {
        private static readonly Dictionary<string, string> _fileTypes = new Dictionary<string, string>
        {
            { ".cs", "Visual C# Source File" },
            { ".csproj", "Visual C# Project File" },
            { ".sln", "Visual C# Solution File" },
            { ".md", "Markdown File" },
        };
        
        public string FullPath { get; }
        public string Name { get; }
        public string Type { get; }
        public string Hash { get; }
        public long Size { get; }

        public FileModel(string file)
        {
            Name = Path.GetFileName(file);
            FullPath = Path.GetDirectoryName(file);
            Size = new FileInfo(file).Length;
            Type = GetFileType(file);
            Hash = GetFileHash(file);
        }
        
        private static string GetFileHash(string file)
        {
            using var md5 = MD5.Create();
            using var stream = File.OpenRead(file);
            var hash = md5.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        private static string GetFileType(string file)
        {
            var type = Path.GetExtension(file)?.ToLowerInvariant() ?? "";

            return _fileTypes.TryGetValue(type, out var result)
                ? result
                : type;
        }
    }
}