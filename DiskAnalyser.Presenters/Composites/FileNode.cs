using DiskAnalyser.Presenters.Models;
using System.Collections.Immutable;

namespace DiskAnalyser.Presenters.Composites
{
    public class FileNode : AbstractFileSystemNode
    {
        private readonly long size;

        private FileNode(string name, string fullName, DirectoryNode parentNode, long size)
            : base(name, fullName, parentNode)
        {
            this.size = size;
        }

        public override long Size => size;
        public override long TotalSize => size;

        public override ImmutableArray<IFileSystemNode> GetChildren()
        {
            return new ImmutableArray<IFileSystemNode>();
        }

        internal static FileNode From(IFileModel fileModel, DirectoryNode parentNode)
        {
            return new FileNode(fileModel.Name, fileModel.FullName, parentNode, fileModel.Size);
        }
    }
}