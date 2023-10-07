using DiskAnalyser.Models.ValueObjects;
using DiskAnalyser.Views;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DiskAnalyser.Presenters
{
    public interface IMainPresenter
    {
        Task AnalyseDriveAsync();

        void InitializeDrives();

        void SetView(IMainView view);
    }

    public class MainPresenter : IMainPresenter
    {
        private IMainView _view;

        public async Task AnalyseDriveAsync()
        {
            _view.DeleteDriveSnapShot();

            _view.EnableDriveAnalysisProgressInfo();

            var analysisValue = await _view.PerformDriveAnalysisAsync();

            _view.ConfigureProgressTracking(analysisValue.Directory.TotalDirectoryCount + 1);

            await _view.CreateDriveSnapshotAsync(analysisValue);

            _view.DisableDriveAnalysisProgressInfo();
        }

        public void InitializeDrives()
        {
            var drives = DriveInfo.GetDrives()
                .Where(driveInfo => driveInfo.DriveType != DriveType.Removable && driveInfo.IsReady)
                .Select(driveInfo => DriveValue.From(driveInfo))
                .ToList();

            _view.SetDrives(drives);
        }

        public void SetView(IMainView view)
        {
            _view = view;

            InitializeDrives();
        }
    }
}