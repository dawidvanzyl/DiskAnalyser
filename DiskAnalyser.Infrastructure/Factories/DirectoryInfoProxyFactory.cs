using DiskAnalyser.Infrastructure.Adapters;
using System.IO;

namespace DiskAnalyser.Infrastructure.Factories
{
    public interface IDirectoryInfoProxyFactory
    {
        IDirectoryInfoProxy Create(string directory);
    }

    public class DirectoryInfoProxyFactory : IDirectoryInfoProxyFactory
    {
        public IDirectoryInfoProxy Create(string directory)
        {
            return new DirectoryInfoProxy(new DirectoryInfo(directory));
        }
    }
}
