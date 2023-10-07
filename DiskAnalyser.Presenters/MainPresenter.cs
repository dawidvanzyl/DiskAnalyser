using DiskAnalyser.Models.ValueObjects;
using DiskAnalyser.Views;
using System.IO;
using System.Linq;

namespace DiskAnalyser.Presenters
{
    public interface IMainPresenter
    {
        void InitializeDrives();

        void SetView(IMainView view);
    }

    public class MainPresenter : IMainPresenter
    {
        private IMainView _view;

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