using System.Collections.Immutable;

namespace DiskAnalyser.Models.Abstracts
{
    public interface IFileSystemModel
    {
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

        public string FullPath { get; }

        public string Name { get; }

        public IFileSystemModel Parent { get; }

        public abstract long Size { get; }

        public abstract long TotalSize { get; }

        public abstract ImmutableArray<IFileSystemModel> GetChildren();
    }
}