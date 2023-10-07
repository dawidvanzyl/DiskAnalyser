using System.IO;

namespace DiskAnalyser.Models.ValueObjects
{
    public struct DriveValue
    {
        private readonly DriveInfo _driveInfo;

        public DriveValue(DriveInfo driveInfo)
        {
            _driveInfo = driveInfo;
        }

        public string Description => $"{_driveInfo.VolumeLabel} ({_driveInfo.Name.TrimEnd()})";

        public string Name => _driveInfo.Name;

        public static DriveValue From(DriveInfo driveInfo)
        {
            return new DriveValue(driveInfo);
        }
    }
}