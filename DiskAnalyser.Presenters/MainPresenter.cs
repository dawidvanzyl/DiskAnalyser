using DiskAnalyser.Models;
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
            var driveModels = DriveInfo.GetDrives()
                .Where(driveInfo => driveInfo.DriveType != DriveType.Removable && driveInfo.IsReady)
                .Select(driveInfo => new DriveModel(driveInfo))
                .ToList();

            _view.SetDrives(driveModels);
        }

        public void SetView(IMainView view)
        {
            _view = view;

            InitializeDrives();
        }
    }
}