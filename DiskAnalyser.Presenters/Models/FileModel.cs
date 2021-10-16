using System.IO;

namespace DiskAnalyser.Presenters.Models
{
    public interface IFileModel
    {
        string FullName { get; }
        string Name { get; }
        long Size { get; }
    }

    public class FileModel : IFileModel
    {
        private readonly FileInfo fileInfo;

        private FileModel(FileInfo fileInfo)
        {
            this.fileInfo = fileInfo;
        }

        public long Size => fileInfo.Length;
        public string FullName => fileInfo.FullName;
        public string Name => fileInfo.Name;

        internal static IFileModel From(FileInfo fileInfo)
        {
            return new FileModel(fileInfo);
        }
    }
}