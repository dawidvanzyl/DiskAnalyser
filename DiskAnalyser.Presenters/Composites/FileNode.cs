using DiskAnalyser.Presenters.Proxies;
using System.Collections.Immutable;

namespace DiskAnalyser.Presenters.Composites
{
    public class FileNode : AbstractFileSystemNode
    {
        private readonly long size;

        public override long Size => size;
        public override long TotalSize => size;

        private FileNode(string name, long size)
            : base(name)
        {
            this.size = size;
        }

        internal static FileNode From(IFileProxy fileProxy)
        {
            return new FileNode(fileProxy.Name, fileProxy.Size);
        }

        public override ImmutableArray<IFileSystemNode> GetNodes()
        {
            return new ImmutableArray<IFileSystemNode>();
        }
    }
}
