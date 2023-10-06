using System.IO;

namespace DiskAnalyser.Models
{
    public class DriveModel
    {
        public DriveModel(DriveInfo driveInfo)
        {
            Description = $"{driveInfo.VolumeLabel} ({driveInfo.Name.TrimEnd()})";
            DriveInfo = driveInfo;
        }

        public string Description { get; }
        public DriveInfo DriveInfo { get; }
    }
}