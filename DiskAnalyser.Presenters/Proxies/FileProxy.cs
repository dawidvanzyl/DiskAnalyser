using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DiskAnalyser.Presenters.Proxies
{
    public interface IFileProxy
    { 
        string Name { get; }
        long Size { get; }
    }

    public class FileProxy : IFileProxy
    {
        private readonly FileInfo fileInfo;

        private FileProxy(FileInfo fileInfo)
        {
            this.fileInfo = fileInfo;
        }

        public string Name => fileInfo.Name;

        public long Size => fileInfo.Length;

        internal static IFileProxy From(FileInfo fileInfo)
        {
            return new FileProxy(fileInfo);
        }
    }
}
