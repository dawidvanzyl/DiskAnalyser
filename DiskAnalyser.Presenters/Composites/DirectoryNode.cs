using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;

namespace DiskAnalyser.Presenters.Composites
{
    public class DirectoryNode : AbstractFileSystemNode
    {
        private readonly List<IFileSystemNode> children;
        private readonly DirectoryNode parentNode;
        private long size;
        private long totalSize;

        private DirectoryNode(string name) 
            : base(name)
        {
            children = new List<IFileSystemNode>();
        }

        private DirectoryNode(string name, DirectoryNode parentNode)
            : this (name)
        {
            this.parentNode = parentNode;
        }

        public int FileNodes { get; private set; }
        public int TotalFileNodes { get; private set; }
        public override long Size => size;
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

        private void UpdateTotalSize(long size)
        {
            totalSize += size;
            parentNode?.UpdateTotalSize(size);
        }

        private void UpdateTotalFiles(int fileNodes)
        {
            TotalFileNodes += fileNodes;
            parentNode?.UpdateTotalFiles(fileNodes);
        }

        internal static DirectoryNode From(string name)
        {
            return new DirectoryNode(name);
        }

        internal static DirectoryNode From(string name, DirectoryNode parentNode)
        {
            return new DirectoryNode(name, parentNode);
        }

        public override ImmutableArray<IFileSystemNode> GetNodes()
        {
            return children.ToImmutableArray();
        }        
    }
}
