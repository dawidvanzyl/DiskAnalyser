using DiskAnalyser.Presenters.Composites;
using System;

namespace DiskAnalyser.Presenters.EventArguments
{
    public class DirectoryAnalysedEventArgs : EventArgs
    {
        public DirectoryAnalysedEventArgs(string directory, DirectoryNode rootDirectoryNode)
        {
            Directory = directory;
            RootDirectoryNode = rootDirectoryNode;
        }

        public string Directory { get; }
        public DirectoryNode RootDirectoryNode { get; }
    }
}
