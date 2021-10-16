namespace DiskAnalyser.Presenters.Composites
{
    public class DriveNode : DirectoryNode
    {
        protected DriveNode(string name, string fullName)
            : base(name, fullName, null, null)
        {
        }

        internal static DriveNode From(string name, string fullName)
        {
            return new DriveNode(name, fullName);
        }
    }
}