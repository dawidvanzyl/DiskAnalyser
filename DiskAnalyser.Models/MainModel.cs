namespace DiskAnalyser.Models
{
    public interface IMainModel
    {
        string Drive { get; }

        void SelectedDriveChanged(string drive);
    }

    public class MainModel : IMainModel
    {
        public string Drive { get; private set; }

        public void SelectedDriveChanged(string drive)
        {
            Drive = drive;
        }
    }
}
