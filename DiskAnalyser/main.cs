using DiskAnalyser.Converters;
using DiskAnalyser.Presenters;
using DiskAnalyser.Presenters.Composites;
using DiskAnalyser.Presenters.EventArguments;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace DiskAnalyser
{
    public partial class main : Form
    {
        private readonly BackgroundWorker driveAnalysisWorker;
        private readonly NodeSizeComparer nodeSizeComparer;
        private readonly IMainPresenter presenter;

        public main(IPresenterFactory presenterFactory)
        {
            InitializeComponent();

            cmbDrives.SelectedIndexChanged += this.CmbDrives_SelectedIndexChanged;
            btnAnalyseDrive.Click += this.BtnAnalyseDrive_Clicked;

            presenter = presenterFactory.CreateMainPresenter();
            presenter.DrivesInitialized += Presenter_DrivesInitialized;
            presenter.DirectoryNodeAdded += Presenter_DirectoryNodeAdded;

            driveAnalysisWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true
            };
            driveAnalysisWorker.DoWork += DriveAnalysisWorker_DoWork;
            driveAnalysisWorker.ProgressChanged += DriveAnalysisWorker_ProgressChanged;
            driveAnalysisWorker.RunWorkerCompleted += DriveAnalysisWorker_RunWorkerCompleted;

            nodeSizeComparer = NodeSizeComparer.Create();

            presenter.InitializeDrives();
        }

        private void BtnAnalyseDrive_Clicked(object sender, System.EventArgs e)
        {
            tvTreeView.Nodes.Clear();
            driveAnalysisWorker.RunWorkerAsync();
        }

        private void CmbDrives_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var selectedValue = (KeyValuePair<string, string>)cmbDrives.ComboBox.SelectedValue;
            presenter.ChangeSelectedDrive(selectedValue.Key);
        }

        private void DriveAnalysisWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            presenter.AnalyseDrive();
        }

        private void DriveAnalysisWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            tvTreeView.BeginUpdate();
            var directoryNodeAddedEventArgs = e.UserState as DirectoryNodeAddedEventArgs;

            TreeNode treeNode = default;
            IFileSystemNode directoryNode = directoryNodeAddedEventArgs.DirectoryNode;

            while (treeNode is null)
            {
                treeNode = tvTreeView.Nodes
                    .Find(directoryNode.FullName, searchAllChildren: true)
                    .SingleOrDefault();

                if (directoryNode.ParentNode != null)
                {
                }

                if (treeNode is null)
                {
                    treeNode = new TreeNode
                    {
                        Name = directoryNodeAddedEventArgs.DirectoryNode.FullName,
                        Text = directoryNodeAddedEventArgs.DirectoryNode.Name
                    };

                    tvTreeView.Nodes.Add(treeNode);
                }
                else
                {
                    directoryNode = directoryNode.ParentNode;
                }
            }

            if (!treeNode.Nodes.ContainsKey(directoryNodeAddedEventArgs.DirectoryModel.FullName))
            {
                treeNode.Nodes.Add(
                    directoryNodeAddedEventArgs.DirectoryModel.FullName,
                    directoryNodeAddedEventArgs.DirectoryModel.Name);
            }

            tvTreeView.EndUpdate();
            tvTreeView.Refresh();

            lblCurrentDirectory.Text = directoryNodeAddedEventArgs.DirectoryModel.FullName;
            //lblTotalFiles.Text = $"Files: {directoryNodeAddedEventArgs.DirectoryNode.TotalFileNodes}".PadRight(20, ' ');
        }

        private void DriveAnalysisWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblCurrentDirectory.Text = "Analysis Completed";
        }

        private void Presenter_DirectoryNodeAdded(object sender, DirectoryNodeAddedEventArgs e)
        {
            driveAnalysisWorker.ReportProgress(0, e);

            lblCurrentDirectory.Text = e.DirectoryModel.Name;
            lblTotalFiles.Text = $"Files: {((DirectoryNode)e.DirectoryNode).TotalFileNodes}".PadRight(20, ' ');
        }

        private void Presenter_DrivesInitialized(object sender, DrivesInitializedEventArgs e)
        {
            cmbDrives.ComboBox.DataSource = e.Drives.ToList();
            cmbDrives.ComboBox.DisplayMember = "Value";
            cmbDrives.ComboBox.ValueMember = "Key";
        }

        private void TryAdd(TreeNodeCollection nodes, IFileSystemNode fileSystemNode)
        {
            var treeNode = nodes.ContainsKey(fileSystemNode.Name)
                ? nodes.Find(fileSystemNode.Name, searchAllChildren: true).Single()
                : nodes.Add(fileSystemNode.Name, fileSystemNode.Name);

            ImmutableArray<IFileSystemNode> children = fileSystemNode.GetChildren();
            if (children != null && children.Any())
            {
                foreach (var child in children)
                {
                    TryAdd(treeNode.Nodes, child);
                }
            }
        }
    }
}