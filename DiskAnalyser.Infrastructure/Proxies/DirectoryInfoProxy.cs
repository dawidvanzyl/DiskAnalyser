using DiskAnalyser.Infrastructure.Proxies;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace DiskAnalyser.Infrastructure.Adapters
{
    public interface IDirectoryInfoProxy
    {
        string Name { get; }
        bool Exists { get; }
        IDirectoryInfoProxy Parent { get; }
        IDirectoryInfoProxy Root { get; }

        ImmutableArray<IFileInfoProxy> GetDirectoryDetail();
        ImmutableArray<IDirectoryInfoProxy> GetDirectories();
    }

    public class DirectoryInfoProxy : IDirectoryInfoProxy
    {
        private readonly DirectoryInfo directoryInfo;

        private DirectoryInfoProxy(DirectoryInfo directoryInfo)
        {
            this.directoryInfo = directoryInfo;
        }

        internal static IDirectoryInfoProxy From(DirectoryInfo directoryInfo)
        {
            return new DirectoryInfoProxy(directoryInfo);
        }

        public string Name => directoryInfo.Name;
        public bool Exists => directoryInfo.Exists;
        public IDirectoryInfoProxy Parent => From(directoryInfo.Parent);
        public IDirectoryInfoProxy Root => From(directoryInfo.Root);
        
        public ImmutableArray<IFileInfoProxy> GetDirectoryDetail()
        {
            return directoryInfo.GetFiles()
                .Select(fileInfo => FileInfoProxy.From(fileInfo))
                .ToImmutableArray();
        }

        public ImmutableArray<IDirectoryInfoProxy> GetDirectories()
        {
            throw new System.NotImplementedException();
        }
    }
}
