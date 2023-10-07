using DiskAnalyser.Models.Enums;

namespace DiskAnalyser.Models
{
    public class DriveModel : DirectoryModel
    {
        protected DriveModel(string name, string description)
            : base(name, name, null)
        {
            Description = description;
        }

        public string Description { get; }

        public override FileSystemTypes FileSystemType => FileSystemTypes.Drive;

        public static DriveModel From(string name, string description)
        {
            return new DriveModel(
                name,
                description);
        }
    }
}