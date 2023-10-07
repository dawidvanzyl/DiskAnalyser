using DiskAnalyser.Models.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiskAnalyser.Views
{
    public interface IMainView
    {
        void ConfigureProgressTracking(int totalDirectoryCount);

        Task CreateDriveSnapshotAsync(AnalysisValue analysis);

        void DeleteDriveSnapShot();

        void DisableDriveAnalysisProgressInfo();

        void EnableDriveAnalysisProgressInfo();

        Task<AnalysisValue> PerformDriveAnalysisAsync();

        void SetDrives(IList<DriveValue> drives);
    }
}