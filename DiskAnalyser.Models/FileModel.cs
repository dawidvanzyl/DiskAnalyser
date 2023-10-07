using DiskAnalyser.Models.Abstracts;
using DiskAnalyser.Models.Enums;

namespace DiskAnalyser.Models
{
    public class FileModel : AbstractFileSystemModel
    {
        private readonly long size;

        private FileModel(string name, string fullName, long size, DirectoryModel parentModel)
            : base(name, fullName, parentModel)
        {
            this.size = size;
        }

        public override FileSystemTypes FileSystemType => FileSystemTypes.File;

        public override long Size => size;

        public override long TotalSize => size;

        public static FileModel From(string name, string fullName, long size, DirectoryModel parent)
        {
            return new FileModel(name, fullName, size, parent);
        }
    }
}