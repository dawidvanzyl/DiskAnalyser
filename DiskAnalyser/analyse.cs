using DiskAnalyser.Models.ValueObjects;
using DiskAnalyser.Presenters;
using DiskAnalyser.Views;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiskAnalyser
{
    public partial class analyse : Form, IAnalysisView
    {
        private readonly IAnalysePresenter _analysePresenter;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public analyse(IAnalysePresenter analysePresenter)
        {
            InitializeComponent();

            _cancellationTokenSource = new CancellationTokenSource();
            _analysePresenter = analysePresenter;
        }

        public IProgress<string> DirectoryAdded { get; private set; }

        public IProgress<int> DirectoryAnalysed { get; private set; }

        public async Task<AnalysisValue> AnalyseDriveAsync(DriveValue drive)
        {
            DirectoryAdded = new Progress<string>();
            DirectoryAnalysed = new Progress<int>();

            ((Progress<string>)DirectoryAdded).ProgressChanged += (s, directory) => lblCurrentDirectory.Text = directory;
            ((Progress<int>)DirectoryAnalysed).ProgressChanged += (s, totalNumberOfFiles) => lblTotalFiles.Text = $"Total File: {totalNumberOfFiles}";

            var cancellationToken = _cancellationTokenSource.Token;
            var directoryModel = await _analysePresenter.AnalyseDriveAsync(drive, cancellationToken);

            return AnalysisValue.Create(directoryModel, cancellationToken.IsCancellationRequested);
        }

        private void analyse_Load(object sender, EventArgs e)
        {
            this.CenterToParent();

            _analysePresenter.SetView(this);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource.Cancel();
        }
    }
}