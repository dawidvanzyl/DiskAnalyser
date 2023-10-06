using DiskAnalyser.Models;
using DiskAnalyser.Presenters;
using DiskAnalyser.Presenters.Models;
using DiskAnalyser.Views;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiskAnalyser
{
    public partial class analyse : Form, IAnalysisView
    {
        private readonly IAnalysePresenter _analysePresenter;
        //private readonly BackgroundWorker _driveAnalysisWorker;
        private AnalysisCancelToken _cancelToken;
        private DriveInfo _driveInfo;
        private IMainView _mainView;

        public analyse(IAnalysePresenter analysePresenter)
        {
            InitializeComponent();

            //_driveAnalysisWorker = new BackgroundWorker
            //{
            //    WorkerReportsProgress = true
            //};

            //_driveAnalysisWorker.DoWork += DriveAnalysisWorker_DoWork;
            //_driveAnalysisWorker.ProgressChanged += DriveAnalysisWorker_ProgressChanged;
            //_driveAnalysisWorker.RunWorkerCompleted += DriveAnalysisWorker_RunWorkerCompleted;

            _analysePresenter = analysePresenter;
        }

        public IProgress<(IFileSystemDescriptionModel SubDirectory, IFileSystemDescriptionModel Directory, int counter)> Progress { get; private set; }

        public void DirectoryAdded(IFileSystemDescriptionModel subDirectory, IFileSystemDescriptionModel directory)
        {
            var userState = (subDirectory, directory);
            //_driveAnalysisWorker.ReportProgress(0, userState);
        }

        public void SetParent(IMainView mainView)
        {
            _mainView = mainView;
        }

        public void SetSelectedDrive(DriveInfo driveInfo)
        {
            _driveInfo = driveInfo;
        }

        private void _progress_ProgressChanged(object sender, (IFileSystemDescriptionModel SubDirectory, IFileSystemDescriptionModel Directory, int counter) e)
        {
            if (e.counter == 0)
            {
                _mainView.DirectoryAdded(e.SubDirectory, e.Directory);
                lblCurrentDirectory.Text = e.Directory.FullName;
            }
        }

        private void analyse_Load(object sender, EventArgs e)
        {
            _analysePresenter.SetView(this);
        }

        private async void analyse_Shown(object sender, EventArgs e)
        {
            Progress = new Progress<(IFileSystemDescriptionModel SubDirectory, IFileSystemDescriptionModel Directory, int counter)>();
            ((Progress<(IFileSystemDescriptionModel SubDirectory, IFileSystemDescriptionModel Directory, int counter)>)Progress).ProgressChanged += _progress_ProgressChanged;

            _cancelToken = new AnalysisCancelToken();
            await Task.Run(() => _analysePresenter.AnalyseDriveAsync(_driveInfo, _cancelToken));

            //_driveAnalysisWorker.RunWorkerAsync(_driveInfo);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _cancelToken.Cancel();
            this.Close();
        }

        //private void DriveAnalysisWorker_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    _cancelToken = new AnalysisCancelToken();
        //    _analysePresenter.AnalyseDrive((DriveInfo)e.Argument, _cancelToken);
        //}

        //private void DriveAnalysisWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    var userState = ((IFileSystemDescriptionModel SubDirectory, IFileSystemDescriptionModel Directory))e.UserState;
        //    _mainView.DirectoryAdded(userState.SubDirectory, userState.Directory);

        //    lblCurrentDirectory.Text = userState.Directory.FullName;
        //    //lblCurrentDirectory.Update();
        //    //btnCancel.Focus();
        //}

        //private void DriveAnalysisWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    this.Close();
        //}
    }
}