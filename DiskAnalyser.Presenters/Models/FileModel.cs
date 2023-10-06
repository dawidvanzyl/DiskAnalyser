using DiskAnalyser.Models;
using System.IO;

namespace DiskAnalyser.Presenters.Models
{
    public interface IFileModel : IFileSystemDescriptionModel
    {
        long Size { get; }
    }

    public class FileModel : IFileModel
    {
        private readonly FileInfo fileInfo;

        private FileModel(FileInfo fileInfo)
        {
            this.fileInfo = fileInfo;
        }

        public string FullName => fileInfo.FullName;
        public string Name => fileInfo.Name;
        public long Size => fileInfo.Length;

        internal static IFileModel From(FileInfo fileInfo)
        {
            return new FileModel(fileInfo);
        }
    }
}