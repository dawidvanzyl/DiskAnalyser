using DiskAnalyser.Models;
using DiskAnalyser.Presenters.Composites;
using DiskAnalyser.Presenters.EventArguments;
using DiskAnalyser.Presenters.Models;
using System;
using System.IO;
using System.Linq;

namespace DiskAnalyser.Presenters
{
    public interface IMainPresenter
    {
        event EventHandler<EventArgs> AnalysisCompleted;

        event EventHandler<DirectoryAnalysedEventArgs> DirectoryAnalysed;

        event EventHandler<DirectoryNodeAddedEventArgs> DirectoryNodeAdded;

        event EventHandler<DrivesInitializedEventArgs> DrivesInitialized;

        DriveNode AnalyseDrive();

        void ChangeSelectedDrive(string drive);

        void InitializeDrives();
    }

    public class MainPresenter : IMainPresenter
    {
        private readonly IMainModel model;

        public MainPresenter(IMainModel model)
        {
            this.model = model;
        }

        public event EventHandler<EventArgs> AnalysisCompleted;

        public event EventHandler<DirectoryAnalysedEventArgs> DirectoryAnalysed;

        public event EventHandler<DirectoryNodeAddedEventArgs> DirectoryNodeAdded;

        public event EventHandler<DrivesInitializedEventArgs> DrivesInitialized;

        public DriveNode AnalyseDrive()
        {
            var driveNode = DriveNode.From(model.Drive, model.Drive);
            var directory = DirectoryModel.From(model.Drive);
            AnalyseDirectory(directory, driveNode, driveNode, driveNode);

            AnalysisCompleted?.Invoke(this, new EventArgs());

            return driveNode;
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

        private void AnalyseDirectory(IDirectoryModel directory, DirectoryNode directoryNode, DirectoryNode rootDirectoryNode, DriveNode driveNode)
        {
            if (directory.HasSubDirectories())
            {
                var subDirectories = directory.GetDirectories();
                foreach (var subDirectory in subDirectories)
                {
                    var subDirectoryNode = DirectoryNode.From(subDirectory.Name, subDirectory.FullName, directoryNode, driveNode);
                    directoryNode.AddDirectoryNode(subDirectoryNode);
                    DirectoryNodeAdded?.Invoke(this, new DirectoryNodeAddedEventArgs(subDirectory, directoryNode));

                    AnalyseDirectory(subDirectory, subDirectoryNode, rootDirectoryNode, driveNode);
                }
            }

            if (directory.HasFiles())
            {
                var fileLeaves = directory.GetFiles()
                    .Select(fileModel => FileNode.From(fileModel, directoryNode));

                directoryNode.AddFileNodes(fileLeaves);
            }

            DirectoryAnalysed?.Invoke(this, new DirectoryAnalysedEventArgs(directory.FullName, rootDirectoryNode));
        }
    }
}