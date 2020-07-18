namespace DiskAnalyser.Presenters.EventArguments
{
    public class DriveAnalysisProgressUpdatedEventArgs
    {
        public DriveAnalysisProgressUpdatedEventArgs(int progressPercentage)
        {
            ProgressPercentage = progressPercentage;
        }

        public int ProgressPercentage { get; }
    }
}
