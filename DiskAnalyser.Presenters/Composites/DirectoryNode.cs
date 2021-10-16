using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace DiskAnalyser.Presenters.Composites
{
    public class DirectoryNode : AbstractFileSystemNode
    {
        private readonly List<IFileSystemNode> children;
        private readonly DriveNode driveNode;
        private readonly DirectoryNode parentNode;
        private long size;
        private long totalSize;

        protected DirectoryNode(string name, string fullName, DirectoryNode parentNode, DriveNode driveNode)
            : base(name, fullName, parentNode)
        {
            children = new List<IFileSystemNode>();
            this.parentNode = parentNode;
            this.driveNode = driveNode;
        }

        public IFileSystemNode DriveNode => driveNode;
        public int FileNodes { get; private set; }
        public override long Size => size;

        public int TotalFileNodes { get; private set; }
        public override long TotalSize => totalSize;

        public void AddDirectoryNode(DirectoryNode node)
        {
            children.Add(node);
        }

        public void AddFileNodes(IEnumerable<FileNode> nodes)
        {
            children.AddRange(nodes);

            FileNodes += nodes.Count();
            size += nodes.Sum(node => node.Size);
            UpdateTotalFiles(FileNodes);
            UpdateTotalSize(size);
        }

        public override ImmutableArray<IFileSystemNode> GetChildren()
        {
            return children.ToImmutableArray();
        }

        internal static DirectoryNode From(string name, string fullName, DirectoryNode parentNode, DriveNode driveNode)
        {
            return new DirectoryNode(name, fullName, parentNode, driveNode);
        }

        private void UpdateTotalFiles(int fileNodes)
        {
            TotalFileNodes += fileNodes;
            parentNode?.UpdateTotalFiles(fileNodes);
        }

        private void UpdateTotalSize(long size)
        {
            totalSize += size;
            parentNode?.UpdateTotalSize(size);
        }
    }
}