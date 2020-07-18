using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace DiskAnalyser.Presenters.Proxies
{
    public interface IDirectoryProxy
    { 
        string Name { get; }
        string FullName { get; }

        ImmutableArray<IDirectoryProxy> GetDirectories();
        bool HasSubDirectories();
        long GetSize();
        bool HasFiles();
        ImmutableArray<IFileProxy> GetFiles();
    }

    public sealed class DirectoryProxy : IDirectoryProxy
    {
        private readonly DirectoryInfo directoryInfo;

        private DirectoryProxy(DirectoryInfo directoryInfo)
        {
            this.directoryInfo = directoryInfo;
        }

        public string Name => directoryInfo.Name;
        public string FullName => directoryInfo.FullName;        

        public ImmutableArray<IDirectoryProxy> GetDirectories()
        {
            return directoryInfo.GetDirectories()
                .Select(directoryInfo => DirectoryProxy.From(directoryInfo))
                .ToImmutableArray();
        }

        internal static IDirectoryProxy From(DirectoryInfo directoryInfo)
        {
            return new DirectoryProxy(directoryInfo);
        }
        public static IDirectoryProxy From(string drive)
        {
            return new DirectoryProxy(new DirectoryInfo(drive));
        }

        public bool HasSubDirectories()
        {
            try
            {
                return directoryInfo.GetDirectories().Length > 0;
            }
            catch (DirectoryNotFoundException)
            {
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }

        public long GetSize()
        {
            var files = directoryInfo.GetFiles();
            var numberOfFiles = files.Length;
            var size = files.Sum(fileInfo => fileInfo.Length);

            return size;
        }

        public ImmutableArray<IFileProxy> GetFiles()
        {
            try
            {
                return directoryInfo.GetFiles()
                        .Select(fileInfo => FileProxy.From(fileInfo))
                        .ToImmutableArray();
            }
            catch (DirectoryNotFoundException)
            {
                return new ImmutableArray<IFileProxy>();
            }
            catch (UnauthorizedAccessException)
            {
                return new ImmutableArray<IFileProxy>();
            }
        }

        public bool HasFiles()
        {
            var filesArray = GetFiles();
            return filesArray != null
                ? filesArray.Length > 0
                : false;
        }
    }
}
