using DiskAnalyser.Converters;
using DiskAnalyser.Models;
using DiskAnalyser.Models.ValueObjects;
using DiskAnalyser.Presenters;
using DiskAnalyser.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiskAnalyser
{
    public partial class main : Form, IMainView
    {
        private readonly IMainPresenter _presenter;
        private readonly IServiceProvider _serviceProvider;

        private IProgress<int> _directoriesProcessed;

        public main(IMainPresenter mainPresenter, IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _presenter = mainPresenter;
            _serviceProvider = serviceProvider;

            toolStripSnapshot.Visible = false;
            toolStripProcessing.Visible = false;
            lblEstimatedTimeLeft.Visible = false;
        }

        public void ConfigureProgressTracking(int totalDirectoryCount)
        {
            lblEstimatedTimeLeft.Visible = true;
            lblProcessingDirectories.Text = "Creating Snapshot";
            pbProcessingDirectories.Value = 0;
            pbProcessingDirectories.Maximum = totalDirectoryCount + 1;
            pbProcessingDirectories.ProgressBar.Style = ProgressBarStyle.Blocks;
        }

        public async Task CreateDriveSnapshotAsync(AnalysisValue analysis)
        {
            var drive = analysis.Directory;
            var directoryProcessingEstimateValue = DirectoryProcessingEstimateValue.Create(drive.TotalDirectoryCount + 1);
            _directoriesProcessed = new Progress<int>();
            (_directoriesProcessed as Progress<int>).ProgressChanged += (object sender, int progress) =>
            {
                pbProcessingDirectories.Value++;
                var estimatedTimeSpan = directoryProcessingEstimateValue.GetEstimate(pbProcessingDirectories.Value);
                lblEstimatedTimeLeft.Text = $"Time left {directoryProcessingEstimateValue.FormattedEstimate(estimatedTimeSpan)}";
            };

            var driveNode = await CreateDriveNodeAsync(drive);

            tvTreeView.Nodes.Add(driveNode);
            gbxSnapshot.Text = $"{drive.Name} snapshot, {DateTime.Now:yyyy, dd MMM HH:mm}";

            if (analysis.Cancelled)
            {
                gbxSnapshot.Text = $"{gbxSnapshot.Text}. Incomplete.";
            }

            toolStripSnapshot.Visible = true;
        }

        public void DeleteDriveSnapShot()
        {
            tvTreeView.Nodes.Clear();
        }

        public void DisableDriveAnalysisProgressInfo()
        {
            toolStripProcessing.Visible = false;
            lblEstimatedTimeLeft.Visible = false;
        }

        public void EnableDriveAnalysisProgressInfo()
        {
            toolStripProcessing.Visible = true;
            pbProcessingDirectories.ProgressBar.Style = ProgressBarStyle.Marquee;
            lblProcessingDirectories.Text = "Analysing...";
        }

        public async Task<AnalysisValue> PerformDriveAnalysisAsync()
        {
            var analyse = _serviceProvider.GetRequiredService<analyse>();
            analyse.Show(this);

            var analyseView = analyse as IAnalysisView;
            var analysisValue = await analyseView.AnalyseDriveAsync((DriveValue)cmbDrives.ComboBox.SelectedValue);

            analyse.Close();

            return analysisValue;
        }

        public void SetDrives(IList<DriveValue> drives)
        {
            cmbDrives.ComboBox.DataSource = drives;
            cmbDrives.ComboBox.DisplayMember = "Description";
        }

        private async void btnAnalyseDrive_Click(object sender, EventArgs e)
        {
            await _presenter.AnalyseDriveAsync();
        }

        private async Task<TreeNode> CreateDirectoryNodeAsync(DirectoryModel directory, TreeNode parentNode)
        {
            return await Task.Run(async () =>
            {
                _directoriesProcessed.Report(0);

                var directorySizeNode = SizeConverter.GetNodeSize(directory.Size);
                var directoryTotalSizeNode = SizeConverter.GetNodeSize(directory.TotalSize);
                var directoryNode = new TreeNode
                {
                    Name = directory.FullPath,
                    Text = $"{directory.Name} [{directorySizeNode}, Total Size: {directoryTotalSizeNode}]",
                    Tag = directory
                };

                parentNode.Nodes.Add(directoryNode);

                if (directory.HasSubDirectories())
                {
                    foreach (var subDirectory in directory.GetDirectories())
                    {
                        await CreateDirectoryNodeAsync(subDirectory, directoryNode);
                    }
                }

                if (directory.HasFiles())
                {
                    foreach (var file in directory.GetFiles())
                    {
                        var fileSizeNode = SizeConverter.GetNodeSize(file.Size);
                        directoryNode.Nodes.Add(new TreeNode
                        {
                            Name = file.FullPath,
                            Text = $"{file.Name} [{fileSizeNode}]",
                            Tag = file
                        });
                    }
                }

                return directoryNode;
            });
        }

        private async Task<TreeNode> CreateDriveNodeAsync(DirectoryModel drive)
        {
            return await CreateDirectoryNodeAsync(drive, new TreeNode());
        }

        private void main_Load(object sender, System.EventArgs e)
        {
            _presenter.SetView(this);
        }
    }
}