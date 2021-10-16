using DiskAnalyser.Presenters.Composites;
using System;

namespace DiskAnalyser.Presenters.EventArguments
{
    public class DirectoryAnalysedEventArgs : EventArgs
    {
        public DirectoryAnalysedEventArgs(string directory, DirectoryNode directoryNode)
        {
            Directory = directory;
            DirectoryNode = directoryNode;
        }

        public string Directory { get; }
        public DirectoryNode DirectoryNode { get; }
    }
}