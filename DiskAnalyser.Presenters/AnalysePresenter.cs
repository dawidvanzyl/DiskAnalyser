using DiskAnalyser.Presenters.Composites;
using DiskAnalyser.Presenters.Models;
using DiskAnalyser.Views;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DiskAnalyser.Presenters
{
    public interface IAnalysePresenter
    {
        Task AnalyseDriveAsync(DriveInfo driveInfo, AnalysisCancelToken analysisCancelToken);

        void SetView(IAnalysisView view);
    }

    public class AnalysePresenter : IAnalysePresenter
    {
        private IAnalysisView _view;
        private int i = 0;

        public async Task AnalyseDriveAsync(DriveInfo driveInfo, AnalysisCancelToken analysisCancelToken)
        {
            await Task.Run(() =>
            {
                var driveNode = DriveNode.From(driveInfo.Name, driveInfo.Name);
                var directoryModel = DirectoryModel.From(driveInfo.Name);
                AnalyseDirectory(directoryModel, driveNode, driveNode, driveNode, analysisCancelToken);
            });
        }

        public void SetView(IAnalysisView view)
        {
            _view = view;
        }

        private void AnalyseDirectory(IDirectoryModel directory, DirectoryNode directoryNode, DirectoryNode rootDirectoryNode, DriveNode driveNode, AnalysisCancelToken analysisCancelToken)
        {
            if (analysisCancelToken.Cancelled)
            {
                return;
            }

            if (directory.HasSubDirectories())
            {
                var subDirectories = directory.GetDirectories();
                foreach (var subDirectory in subDirectories)
                {
                    var subDirectoryNode = DirectoryNode.From(subDirectory.Name, subDirectory.FullName, directoryNode, driveNode);
                    directoryNode.AddDirectoryNode(subDirectoryNode);

                    i++;
                    if (i == 1000)
                    {
                        i = 0;
                    }

                    _view.Progress.Report((subDirectory, directoryNode, i));

                    AnalyseDirectory(subDirectory, subDirectoryNode, rootDirectoryNode, driveNode, analysisCancelToken);
                }
            }

            if (directory.HasFiles())
            {
                var fileLeaves = directory
                    .GetFiles()
                    .Select(fileModel => FileNode.From(fileModel, directoryNode));

                directoryNode.AddFileNodes(fileLeaves);
            }

            //DirectoryAnalysed?.Invoke(this, new DirectoryAnalysedEventArgs(directory.FullName, rootDirectoryNode));
        }
    }
}