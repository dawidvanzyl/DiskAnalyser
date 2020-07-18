using DiskAnalyser.Models;
using DiskAnalyser.Presenters.Composites;
using DiskAnalyser.Presenters.EventArguments;
using DiskAnalyser.Presenters.Proxies;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DiskAnalyser.Presenters
{
    public interface IMainPresenter
    {
        void InitializeDrives();
        void ChangeSelectedDrive(string drive);
        DirectoryNode AnalyseDrive();

        event EventHandler<DrivesInitializedEventArgs> DrivesInitialized;        
        event EventHandler<DirectoryAnalysedEventArgs> DirectoryAnalysed;
        event EventHandler<EventArgs> AnalysisCompleted;
    }

    public class MainPresenter : IMainPresenter
    {
        private readonly IMainModel model;

        public event EventHandler<DrivesInitializedEventArgs> DrivesInitialized;
        public event EventHandler<DirectoryAnalysedEventArgs> DirectoryAnalysed;
        public event EventHandler<EventArgs> AnalysisCompleted;

        public MainPresenter(IMainModel model)
        {
            this.model = model;
        }

        public void ChangeSelectedDrive(string drive)
        {
            model.SelectedDriveChanged(drive);
        }

        public void InitializeDrives()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            var initialisedDrives = drives
                .Where(drive => drive.DriveType != DriveType.Removable)
                .ToDictionary(drive => drive.Name, drive => $"{drive.VolumeLabel} ({drive.Name.TrimEnd()})");

            DrivesInitialized?.Invoke(this, new DrivesInitializedEventArgs(initialisedDrives));
        }

        public DirectoryNode AnalyseDrive()
        {
            var rootDirectoryNode = DirectoryNode.From(model.Drive);
            var directory = DirectoryProxy.From(model.Drive);
            AnalyseDirectory(directory, rootDirectoryNode, rootDirectoryNode);

            AnalysisCompleted?.Invoke(this, new EventArgs());

            return rootDirectoryNode;
        }

        private void AnalyseDirectory(IDirectoryProxy directory, DirectoryNode directoryNode, DirectoryNode rootDirectoryNode)
        {
            if (directory.HasSubDirectories())
            {
                var subDirectories = directory.GetDirectories();
                foreach (var subDirectory in subDirectories)
                {
                    var subDirectoryNode = DirectoryNode.From(subDirectory.Name, directoryNode);
                    directoryNode.AddDirectoryNode(subDirectoryNode);

                    AnalyseDirectory(subDirectory, subDirectoryNode, rootDirectoryNode);
                }
            }

            if (directory.HasFiles())
            {
                var fileLeaves = directory.GetFiles()
                    .Select(fileProxy => FileNode.From(fileProxy));

                directoryNode.AddFileNodes(fileLeaves);
            }

            DirectoryAnalysed?.Invoke(this, new DirectoryAnalysedEventArgs(directory.FullName, rootDirectoryNode));
        }
    }
}
