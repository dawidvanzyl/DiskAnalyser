using DiskAnalyser.Models.Enums;
using System.Collections.Immutable;

namespace DiskAnalyser.Models.Abstracts
{
    public interface IFileSystemModel
    {
        FileSystemTypes FileSystemType { get; }

        string FullPath { get; }

        string Name { get; }

        IFileSystemModel Parent { get; }

        long Size { get; }

        long TotalSize { get; }

        ImmutableArray<IFileSystemModel> GetChildren();
    }

    public abstract class AbstractFileSystemModel : IFileSystemModel
    {
        protected AbstractFileSystemModel(string name, string fullName, IFileSystemModel parent)
        {
            Name = name;
            FullPath = fullName;
            Parent = parent;
        }

        public abstract FileSystemTypes FileSystemType { get; }

        public string FullPath { get; }

        public string Name { get; }

        public IFileSystemModel Parent { get; }

        public abstract long Size { get; }

        public abstract long TotalSize { get; }

        public abstract ImmutableArray<IFileSystemModel> GetChildren();

        public override string ToString()
        {
            return Name;
        }
    }
}