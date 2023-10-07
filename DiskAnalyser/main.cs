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
        private Snapshot _snapshot;

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

            _snapshot = new Snapshot(driveNode, analysis.Cancelled, DateTime.Now);

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

        private async Task<DirectoryNode> CreateDirectoryNodeAsync(DirectoryModel directory, DirectoryNode parentNode)
        {
            return await Task.Run(async () =>
            {
                _directoriesProcessed.Report(0);

                var directoryNode = new DirectoryNode
                {
                    Name = directory.FullPath,
                    Text = directory.Name,
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

                return directoryNode;
            });
        }

        private async Task<DirectoryNode> CreateDriveNodeAsync(DirectoryModel drive)
        {
            return await CreateDirectoryNodeAsync(drive, new DirectoryNode());
        }

        private void main_Load(object sender, System.EventArgs e)
        {
            _presenter.SetView(this);
        }
    }
}