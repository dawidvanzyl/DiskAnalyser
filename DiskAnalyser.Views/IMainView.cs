using DiskAnalyser.Models;
using System.Collections.Generic;

namespace DiskAnalyser.Views
{
    public interface IMainView
    {
        void DirectoryAdded(IFileSystemDescriptionModel subDirectory, IFileSystemDescriptionModel directory);

        void SetDrives(IList<DriveModel> driveModels);
    }
}