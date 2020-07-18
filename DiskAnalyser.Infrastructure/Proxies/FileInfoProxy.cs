using DiskAnalyser.Infrastructure.Adapters;
using System.IO;

namespace DiskAnalyser.Infrastructure.Proxies
{
    public interface IFileInfoProxy
    {
        public string DirectoryName { get; }
        public IDirectoryInfoProxy Directory { get; }
        public long Length { get; }
        public string Name { get; }
    }

    public class FileInfoProxy : IFileInfoProxy
    {
        private readonly FileInfo fileInfo;

        private FileInfoProxy(FileInfo fileInfo)
        {
            this.fileInfo = fileInfo;
        }

        internal static IFileInfoProxy From(FileInfo fileInfo)
        {
            return new FileInfoProxy(fileInfo);
        }

        public string DirectoryName => fileInfo.DirectoryName;

        public IDirectoryInfoProxy Directory => DirectoryInfoProxy.From(fileInfo.Directory);

        public long Length => fileInfo.Length;

        public string Name => fileInfo.Name;
    }
}
