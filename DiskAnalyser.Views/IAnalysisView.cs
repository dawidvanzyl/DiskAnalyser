using DiskAnalyser.Models;
using System;
using System.IO;

namespace DiskAnalyser.Views
{
    public interface IAnalysisView
    {
        IProgress<(IFileSystemDescriptionModel SubDirectory, IFileSystemDescriptionModel Directory, int counter)> Progress { get; }

        void DirectoryAdded(IFileSystemDescriptionModel subDirectory, IFileSystemDescriptionModel directory);

        void SetParent(IMainView mainView);

        void SetSelectedDrive(DriveInfo driveInfo);
    }
}