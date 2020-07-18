using System;
using System.Collections.Generic;

namespace DiskAnalyser.Presenters.EventArguments
{
    public class DrivesInitializedEventArgs : EventArgs
    {
        public DrivesInitializedEventArgs(IReadOnlyDictionary<string, string> drives)
        {
            Drives = drives;
        }

        public IReadOnlyDictionary<string, string> Drives { get; }
    }
}
