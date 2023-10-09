namespace DiskAnalyser.Models.ValueObjects
{
    public struct AnalysisValue
    {
        public AnalysisValue(DirectoryModel directory, bool cancelled)
        {
            Directory = directory;
            Cancelled = cancelled;
        }

        public bool Cancelled { get; }

        public DirectoryModel Directory { get; }

        public static AnalysisValue Create(DirectoryModel directory, bool cancelled)
        {
            return new AnalysisValue(directory, cancelled);
        }
    }
}