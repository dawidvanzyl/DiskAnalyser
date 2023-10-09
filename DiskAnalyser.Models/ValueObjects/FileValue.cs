using System.IO;

namespace DiskAnalyser.Models.ValueObjects
{
    public struct FileValue
    {
        private readonly FileInfo fileInfo;

        private FileValue(FileInfo fileInfo)
        {
            this.fileInfo = fileInfo;
        }

        public string FullPath => fileInfo.FullName;

        public string Name => fileInfo.Name;

        public long Size => fileInfo.Length;

        public static FileValue From(FileInfo fileInfo)
        {
            return new FileValue(fileInfo);
        }
    }
}