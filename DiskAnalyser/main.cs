using DiskAnalyser.Models;
using DiskAnalyser.Presenters;
using DiskAnalyser.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DiskAnalyser
{
    public partial class main : Form, IMainView
    {
        private readonly IMainPresenter _presenter;
        private readonly IServiceProvider _serviceProvider;

        //private readonly BackgroundWorker driveAnalysisWorker;

        public main(IMainPresenter mainPresenter, IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _presenter = mainPresenter;
            _serviceProvider = serviceProvider;

            //cmbDrives.SelectedIndexChanged += this.CmbDrives_SelectedIndexChanged;
            //btnAnalyseDrive.Click += this.BtnAnalyseDrive_Clicked;

            //presenter = presenterFactory.CreateMainPresenter();
            //presenter.DrivesInitialized += Presenter_DrivesInitialized;
            //presenter.DirectoryNodeAdded += Presenter_DirectoryNodeAdded;

            //driveAnalysisWorker = new BackgroundWorker
            //{
            //    WorkerReportsProgress = true,
            //    WorkerSupportsCancellation = true
            //};
            //driveAnalysisWorker.DoWork += DriveAnalysisWorker_DoWork;
            //driveAnalysisWorker.ProgressChanged += DriveAnalysisWorker_ProgressChanged;
            //driveAnalysisWorker.RunWorkerCompleted += DriveAnalysisWorker_RunWorkerCompleted;

            //nodeSizeComparer = NodeSizeComparer.Create();

            //presenter.InitializeDrives();
        }

        public void DirectoryAdded(IFileSystemDescriptionModel subDirectory, IFileSystemDescriptionModel directory)
        {
            var treeNode = tvTreeView.Nodes
                .Find(directory.FullName, searchAllChildren: true)
                .SingleOrDefault();

            if (treeNode is null)
            {
                treeNode = new TreeNode
                {
                    Name = directory.FullName,
                    Text = directory.Name
                };

                tvTreeView.Nodes.Add(treeNode);
            }

            treeNode.Nodes.Add(
                subDirectory.FullName,
                subDirectory.Name);

            //tvTreeView.Update();
        }

        public void SetDrives(IList<DriveModel> driveModels)
        {
            cmbDrives.ComboBox.DataSource = driveModels;
            cmbDrives.ComboBox.DisplayMember = "Description";
            cmbDrives.ComboBox.ValueMember = "DriveInfo";
        }

        private void btnAnalyseDrive_Click(object sender, System.EventArgs e)
        {
            tvTreeView.Nodes.Clear();

            var analyse = _serviceProvider.GetRequiredService<analyse>();
            var analyseView = analyse as IAnalysisView;
            analyseView.SetParent(this);
            analyseView.SetSelectedDrive((DriveInfo)cmbDrives.ComboBox.SelectedValue);
            analyse.ShowDialog();
        }

        private void main_Load(object sender, System.EventArgs e)
        {
            _presenter.SetView(this);
        }

        ////private void DriveAnalysisWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        ////{
        ////    var directoryNodeAddedEventArgs = e.UserState as DirectoryNodeAddedEventArgs;

        ////    TreeNode treeNode = default;
        ////    var directoryNode = directoryNodeAddedEventArgs.DirectoryNode;

        ////    treeNode = tvTreeView.Nodes
        ////        .Find(directoryNode.FullName, searchAllChildren: true)
        ////        .SingleOrDefault();

        ////    if (treeNode is null)
        ////    {
        ////        treeNode = new TreeNode
        ////        {
        ////            Name = directoryNodeAddedEventArgs.DirectoryNode.FullName,
        ////            Text = directoryNodeAddedEventArgs.DirectoryNode.Name
        ////        };

        ////        tvTreeView.Nodes.Add(treeNode);
        ////    }

        ////    treeNode.Nodes.Add(
        ////        directoryNodeAddedEventArgs.DirectoryModel.FullName,
        ////        directoryNodeAddedEventArgs.DirectoryModel.Name);
        ////}

        //private void DriveAnalysisWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    lblCurrentDirectory.Text = $"Analysis Completed";
        //}

        //private void Presenter_DirectoryNodeAdded(object sender, DirectoryNodeAddedEventArgs e)
        //{
        //    //driveAnalysisWorker.ReportProgress(0, e);

        //    var directoryNodeAddedEventArgs = e; //.UserState as DirectoryNodeAddedEventArgs;

        //    TreeNode treeNode = default;
        //    var directoryNode = directoryNodeAddedEventArgs.DirectoryNode;

        //    treeNode = tvTreeView.Nodes
        //        .Find(directoryNode.FullName, searchAllChildren: true)
        //        .SingleOrDefault();

        //    if (treeNode is null)
        //    {
        //        treeNode = new TreeNode
        //        {
        //            Name = directoryNodeAddedEventArgs.DirectoryNode.FullName,
        //            Text = directoryNodeAddedEventArgs.DirectoryNode.Name
        //        };

        //        tvTreeView.Nodes.Add(treeNode);
        //    }

        //    treeNode.Nodes.Add(
        //        directoryNodeAddedEventArgs.DirectoryModel.FullName,
        //        directoryNodeAddedEventArgs.DirectoryModel.Name);

        //    lblCurrentDirectory.Text = e.DirectoryModel.FullName;

        //    this.Refresh();
        //}

        //private void Presenter_DrivesInitialized(object sender, DrivesInitializedEventArgs e)
        //{
        //    cmbDrives.ComboBox.DataSource = e.Drives.ToList();
        //    cmbDrives.ComboBox.DisplayMember = "Value";
        //    cmbDrives.ComboBox.ValueMember = "Key";
        //}

        //private void TryAdd(TreeNodeCollection nodes, IFileSystemNode fileSystemNode)
        //{
        //    var treeNode = nodes.ContainsKey(fileSystemNode.Name)
        //        ? nodes.Find(fileSystemNode.Name, searchAllChildren: true).Single()
        //        : nodes.Add(fileSystemNode.Name, fileSystemNode.Name);

        //    ImmutableArray<IFileSystemNode> children = fileSystemNode.GetChildren();
        //    if (children != null && children.Any())
        //    {
        //        foreach (var child in children)
        //        {
        //            TryAdd(treeNode.Nodes, child);
        //        }
        //    }
        //}
    }
}