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
        private IProgress<Snapshot> _displaySnapshot;
        private bool _preventSecondLoad;
        private Snapshot _snapShot;

        public main(IMainPresenter mainPresenter, IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _presenter = mainPresenter;
            _serviceProvider = serviceProvider;

            toolStripProcessing.Visible = false;
            lblEstimatedTimeLeft.Visible = false;
            _preventSecondLoad = false;
        }

        public void ConfigureProgressTracking(int totalDirectoryCount)
        {
            lblEstimatedTimeLeft.Visible = true;
            lblProcessingDirectories.Text = "Creating Snapshot";
            pbProcessingDirectories.Value = 0;
            pbProcessingDirectories.Maximum = totalDirectoryCount + 1;
            pbProcessingDirectories.ProgressBar.Style = ProgressBarStyle.Blocks;
        }

        public async Task<Snapshot> CreateSnapshotAsync(AnalysisValue analysis)
        {
            ConfigureProgressTracking(analysis.Directory.TotalDirectoryCount + 1);

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

            _snapShot = Snapshot.Create(
                driveNode,
                drive,
                analysis.Cancelled);

            return _snapShot;
        }

        public void DeleteSnapShot()
        {
            DriveAnalysisProgressInfo("Delete Snapshot...");

            Snapshot.Delete((DriveValue)cmbDrives.ComboBox.SelectedValue);
            tvTreeView.Nodes.Clear();
        }

        public void DisableDriveAnalysisProgressInfo()
        {
            topToolStrip.Enabled = true;
            toolStripProcessing.Visible = false;
            lblEstimatedTimeLeft.Visible = false;
        }

        public void DriveAnalysisProgressInfo(string infoText)
        {
            topToolStrip.Enabled = false;
            toolStripProcessing.Visible = true;
            pbProcessingDirectories.ProgressBar.Style = ProgressBarStyle.Marquee;
            lblProcessingDirectories.Text = infoText;
            toolStripProcessing.Refresh();
        }

        public async Task<AnalysisValue> PerformDriveAnalysisAsync()
        {
            DriveAnalysisProgressInfo("Analysing...");

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
            DeleteSnapShot();
            var analysisValue = await PerformDriveAnalysisAsync();
            var snapshot = await CreateSnapshotAsync(analysisValue);
            await SaveSnapshotAsync(snapshot);
            DisableDriveAnalysisProgressInfo();
        }

        private async void cmbDrives_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_preventSecondLoad)
            {
                return;
            }

            _preventSecondLoad = true;
            var drive = (DriveValue)cmbDrives.ComboBox.SelectedValue;
            if (!Snapshot.Exists(drive))
            {
                return;
            }

            DriveAnalysisProgressInfo("Loading Snapshot...");

            _displaySnapshot = new Progress<Snapshot>();
            (_displaySnapshot as Progress<Snapshot>).ProgressChanged += (object sender, Snapshot snapshot) => LoadSnapshot(snapshot);

            await Task.Run(async () =>
            {
                var snapShot = await Snapshot.LoadAsync(drive);
                _displaySnapshot.Report(snapShot);
            });

            DisableDriveAnalysisProgressInfo();
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

        private void LoadSnapshot(Snapshot snapshot)
        {
            tvTreeView.Nodes.Clear();
            tvTreeView.Nodes.AddRange(snapshot.Nodes);
            gbxSnapshot.Text = snapshot.Description;
        }

        private void main_Load(object sender, System.EventArgs e)
        {
            _presenter.SetView(this);
        }

        private async Task SaveSnapshotAsync(Snapshot snapshot)
        {
            DriveAnalysisProgressInfo("Saving Snapshot...");

            _displaySnapshot = new Progress<Snapshot>();
            (_displaySnapshot as Progress<Snapshot>).ProgressChanged += (object sender, Snapshot snapshot) => LoadSnapshot(snapshot);

            await Task.Run(() =>
            {
                snapshot.Save();
                _displaySnapshot.Report(snapshot);
            });
        }
    }
}