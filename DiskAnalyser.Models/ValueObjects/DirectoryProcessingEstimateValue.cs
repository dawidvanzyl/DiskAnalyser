using System;

namespace DiskAnalyser.Models.ValueObjects
{
    public struct DirectoryProcessingEstimateValue
    {
        private readonly int _directoryCount;
        private readonly DateTime _startDateTime;

        private DirectoryProcessingEstimateValue(int directoryCount)
        {
            _startDateTime = DateTime.UtcNow;
            _directoryCount = directoryCount;
        }

        public static DirectoryProcessingEstimateValue Create(int directoryCount)
        {
            return new DirectoryProcessingEstimateValue(directoryCount);
        }

        public string FormattedEstimate(TimeSpan estimate)
        {
            if (estimate.Minutes == 0)
            {
                return "< minute";
            }

            return estimate.Hours switch
            {
                0 => $"~ {estimate.Minutes} minutes",
                _ => $"~ {estimate.Hours:00}:{estimate.Minutes:00}"
            };
        }

        public TimeSpan GetEstimate(int directoriesProcessed)
        {
            var avgTicks = DateTime.UtcNow.Subtract(_startDateTime).Ticks / directoriesProcessed;
            var estimatedTicks = avgTicks * (_directoryCount - directoriesProcessed);
            return TimeSpan.FromTicks(estimatedTicks);
        }
    }
}