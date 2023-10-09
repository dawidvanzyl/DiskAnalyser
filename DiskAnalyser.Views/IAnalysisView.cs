using DiskAnalyser.Models.ValueObjects;
using System;
using System.Threading.Tasks;

namespace DiskAnalyser.Views
{
    public interface IAnalysisView
    {
        IProgress<string> DirectoryAdded { get; }

        IProgress<int> DirectoryAnalysed { get; }

        Task<AnalysisValue> AnalyseDriveAsync(DriveValue drive);
    }
}