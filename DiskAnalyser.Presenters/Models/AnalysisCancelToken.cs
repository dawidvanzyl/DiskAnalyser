namespace DiskAnalyser.Presenters.Models
{
    public sealed class AnalysisCancelToken
    {
        public AnalysisCancelToken()
        {
            Cancelled = false;
        }

        internal bool Cancelled { get; private set; }

        public void Cancel()
        {
            Cancelled = true;
        }
    }
}