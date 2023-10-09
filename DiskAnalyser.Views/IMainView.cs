using DiskAnalyser.Models.ValueObjects;
using System.Collections.Generic;

namespace DiskAnalyser.Views
{
    public interface IMainView
    {
        void SetDrives(IList<DriveValue> drives);
    }
}