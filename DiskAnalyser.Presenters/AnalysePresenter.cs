using DiskAnalyser.Models;
using DiskAnalyser.Models.ValueObjects;
using DiskAnalyser.Views;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiskAnalyser.Presenters
{
    public interface IAnalysePresenter
    {
        Task<DirectoryModel> AnalyseDriveAsync(DriveValue driveValue, CancellationToken cancellationToken);

        void SetView(IAnalysisView view);
    }

    public class AnalysePresenter : IAnalysePresenter
    {
        private IAnalysisView _view;

        public async Task<DirectoryModel> AnalyseDriveAsync(DriveValue driveValue, CancellationToken cancellationToken)
        {
            var drive = DriveModel.From(driveValue.Name, driveValue.Description);
            var directoryValue = DirectoryValue.From(driveValue.Name);
            return await AnalyseDirectoryAsync(directoryValue, drive, drive, cancellationToken);
        }

        public void SetView(IAnalysisView view)
        {
            _view = view;
        }

        private async Task<DirectoryModel> AnalyseDirectoryAsync(DirectoryValue directoryValue, DirectoryModel directory, DirectoryModel rootDirectory, CancellationToken cancellationToken)
        {
            return await Task.Run(async () =>
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return rootDirectory;
                }

                if (directoryValue.HasSubDirectories())
                {
                    var subDirectoryValues = directoryValue.GetDirectories();
                    foreach (var subDirectoryValue in subDirectoryValues)
                    {
                        var subDirectory = DirectoryModel.From(subDirectoryValue.Name, subDirectoryValue.FullPath, directory);
                        directory.AddDirectory(subDirectory);
                        _view.DirectoryAdded.Report(directory.FullPath);

                        await AnalyseDirectoryAsync(subDirectoryValue, subDirectory, rootDirectory, cancellationToken);
                    }
                }

                if (directoryValue.HasFiles())
                {
                    var files = directoryValue
                        .GetFiles()
                        .Select(fileValue => FileModel.From(fileValue.Name, fileValue.FullPath, fileValue.Size, directory));

                    directory.AddFiles(files);
                }

                _view.DirectoryAnalysed.Report(directory.TotalFileCount);

                return rootDirectory;
            });
        }
    }
}