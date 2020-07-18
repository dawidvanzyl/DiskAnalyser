using System.Collections.Immutable;

namespace DiskAnalyser.Presenters.Composites
{
    public interface IFileSystemNode
    {
        string Name { get; }

        ImmutableArray<IFileSystemNode> GetNodes();
        long Size { get; }
        long TotalSize { get; }
    }

    public abstract class AbstractFileSystemNode : IFileSystemNode
    {
        protected AbstractFileSystemNode(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public abstract ImmutableArray<IFileSystemNode> GetNodes();

        public abstract long Size { get; }
        public abstract long TotalSize { get; }
    }
}
