using DiskAnalyser.Converters;
using DiskAnalyser.Presenters;
using DiskAnalyser.Presenters.EventArguments;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace DiskAnalyser
{
    public partial class main : Form
    {
        private readonly IMainPresenter presenter;
        private readonly BackgroundWorker driveAnalysisWorker;
        private readonly NodeSizeComparer nodeSizeComparer;

        public main(IPresenterFactory presenterFactory)
        {
            InitializeComponent();

            cmbDrives.SelectedIndexChanged += this.CmbDrives_SelectedIndexChanged;
            btnAnalyseDrive.Click += this.BtnAnalyseDrive_Clicked;

            presenter = presenterFactory.CreateMainPresenter();
            presenter.DrivesInitialized += Presenter_DrivesInitialized;
            //presenter.AnalysisCompleted += Presenter_AnalysisCompleted;
            presenter.DirectoryAnalysed += Presenter_DirectoryAnalysed;

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

        private void DriveAnalysisWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblCurrentDirectory.Text = "Analysis Completed";
        }

        private void DriveAnalysisWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var directoryAnalysedEventArgs = e.UserState as DirectoryAnalysedEventArgs;

            foreach (var node in directoryAnalysedEventArgs.RootDirectoryNode.GetNodes())
            {
                var listviewItem = lvDirectories.Items.Cast<ListViewItem>().FirstOrDefault(listViewItem => listViewItem.Text == node.Name);
                var nodeSize = SizeConverter.GetNodeSize(node.TotalSize);
                if (listviewItem != null)
                {
                    NodeSize listViewItemSize = NodeSize.From(listviewItem.SubItems[1].Text);                                        
                    if (nodeSizeComparer.Compare(nodeSize, listViewItemSize) < 1)
                    {
                        continue;
                    }

                    lvDirectories.BeginUpdate();

                    lvDirectories.Items.Remove(listviewItem);
                }
                else
                {
                    lvDirectories.BeginUpdate();
                }

                listviewItem = new ListViewItem(node.Name);
                listviewItem.SubItems.Add(nodeSize.ToString());
                lvDirectories.Items.Add(listviewItem);

                lvDirectories.EndUpdate();
            }

            lblCurrentDirectory.Text = directoryAnalysedEventArgs.Directory;
            lblTotalFiles.Text = $"Files: {directoryAnalysedEventArgs.RootDirectoryNode.TotalFileNodes}".PadRight(20, ' ');
        }

        private void DriveAnalysisWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            presenter.AnalyseDrive();
        }

        private void Presenter_DrivesInitialized(object sender, DrivesInitializedEventArgs e)
        {
            cmbDrives.ComboBox.DataSource = e.Drives.ToList();
            cmbDrives.ComboBox.DisplayMember = "Value";
            cmbDrives.ComboBox.ValueMember = "Key";
        }

        private void Presenter_DirectoryAnalysed(object sender, DirectoryAnalysedEventArgs e)
        {
            //lblCurrentDirectory.Text = e.Directory;
            //lblTotalFiles.Text = $"Files: {e.RootDirectoryNode.TotalFileNodes}".PadRight(20, ' ');

            driveAnalysisWorker.ReportProgress(0, e);
        }

        private void CmbDrives_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            presenter.ChangeSelectedDrive(cmbDrives.ComboBox.SelectedValue.ToString());
        }

        private void BtnAnalyseDrive_Clicked(object sender, System.EventArgs e)
        {
            lvDirectories.Items.Clear();
            driveAnalysisWorker.RunWorkerAsync();
        }
    }
}
