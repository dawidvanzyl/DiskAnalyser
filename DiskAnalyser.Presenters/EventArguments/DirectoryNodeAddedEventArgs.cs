using DiskAnalyser.Presenters.Composites;
using DiskAnalyser.Presenters.Models;

namespace DiskAnalyser.Presenters.EventArguments
{
    public class DirectoryNodeAddedEventArgs
    {
        public DirectoryNodeAddedEventArgs(IDirectoryModel directoryModel, IFileSystemNode directoryNode)
        {
            DirectoryModel = directoryModel;
            DirectoryNode = directoryNode;
        }

        public IDirectoryModel DirectoryModel { get; }
        public IFileSystemNode DirectoryNode { get; }
    }
}