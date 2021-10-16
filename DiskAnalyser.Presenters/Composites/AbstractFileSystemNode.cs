using System.Collections.Immutable;

namespace DiskAnalyser.Presenters.Composites
{
    public interface IFileSystemNode
    {
        string FullName { get; }

        string Name { get; }

        IFileSystemNode ParentNode { get; }

        long Size { get; }

        long TotalSize { get; }

        ImmutableArray<IFileSystemNode> GetChildren();
    }

    public abstract class AbstractFileSystemNode : IFileSystemNode
    {
        protected AbstractFileSystemNode(string name, string fullName, IFileSystemNode ParentNode)
        {
            Name = name;
            FullName = fullName;
            this.ParentNode = ParentNode;
        }

        public string FullName { get; }

        public string Name { get; }

        public IFileSystemNode ParentNode { get; }

        public abstract long Size { get; }

        public abstract long TotalSize { get; }

        public abstract ImmutableArray<IFileSystemNode> GetChildren();
    }
}