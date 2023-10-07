using DiskAnalyser.Models.Abstracts;
using System.Collections.Immutable;

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

        public override long Size => size;

        public override long TotalSize => size;

        public static FileModel From(string name, string fullName, long size, DirectoryModel parent)
        {
            return new FileModel(name, fullName, size, parent);
        }

        public override ImmutableArray<IFileSystemModel> GetChildren()
        {
            return new ImmutableArray<IFileSystemModel>();
        }
    }
}