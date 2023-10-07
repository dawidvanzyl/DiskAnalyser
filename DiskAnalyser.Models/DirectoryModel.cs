using DiskAnalyser.Models.Abstracts;
using DiskAnalyser.Models.Enums;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace DiskAnalyser.Models
{
    public class DirectoryModel : AbstractFileSystemModel
    {
        private readonly List<IFileSystemModel> children;
        private readonly DirectoryModel parent;

        private long size;
        private long totalSize;

        protected DirectoryModel(string name, string fullName, DirectoryModel parent)
            : base(name, fullName, parent)
        {
            children = new List<IFileSystemModel>();
            this.parent = parent;
        }

        public int DirectoryCount { get; private set; }

        public int FileCount { get; private set; }

        public override FileSystemTypes FileSystemType => FileSystemTypes.Directory;

        public override long Size => size;

        public int TotalDirectoryCount { get; private set; }

        public int TotalFileCount { get; private set; }

        public override long TotalSize => totalSize;

        public static DirectoryModel From(string name, string fullName, DirectoryModel parent)
        {
            return new DirectoryModel(name, fullName, parent);
        }

        public void AddDirectory(DirectoryModel directory)
        {
            children.Add(directory);
            DirectoryCount++;
            UpdateTotalDirectoryCount();
        }

        public void AddFiles(IEnumerable<FileModel> files)
        {
            children.AddRange(files);

            FileCount += files.Count();
            size += files.Sum(file => file.Size);
            UpdateTotalFileCount(FileCount);
            UpdateTotalSize(size);
        }

        public override ImmutableArray<IFileSystemModel> GetChildren()
        {
            return children.ToImmutableArray();
        }

        public ImmutableArray<DirectoryModel> GetDirectories()
        {
            return children
                .Where(child => child.FileSystemType == FileSystemTypes.Directory)
                .Cast<DirectoryModel>()
                .ToImmutableArray();
        }

        public bool HasSubDirectories()
        {
            return children.Exists(child => child.FileSystemType == FileSystemTypes.Directory);
        }

        private void UpdateTotalDirectoryCount()
        {
            TotalDirectoryCount++;
            parent?.UpdateTotalDirectoryCount();
        }

        private void UpdateTotalFileCount(int fileCount)
        {
            TotalFileCount += fileCount;
            parent?.UpdateTotalFileCount(fileCount);
        }

        private void UpdateTotalSize(long size)
        {
            totalSize += size;
            parent?.UpdateTotalSize(size);
        }
    }
}