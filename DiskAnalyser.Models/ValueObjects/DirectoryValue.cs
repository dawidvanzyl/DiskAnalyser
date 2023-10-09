using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace DiskAnalyser.Models.ValueObjects
{
    public struct DirectoryValue
    {
        private readonly DirectoryInfo directoryInfo;

        private DirectoryValue(DirectoryInfo directoryInfo)
        {
            this.directoryInfo = directoryInfo;
        }

        public long FileCount => throw new NotImplementedException();

        public string FullPath => directoryInfo.FullName;

        public string Name => directoryInfo.Name;

        public static DirectoryValue From(string drive)
        {
            return new DirectoryValue(new DirectoryInfo(drive));
        }

        public static DirectoryValue From(DirectoryInfo directoryInfo)
        {
            return new DirectoryValue(directoryInfo);
        }

        public ImmutableArray<DirectoryValue> GetDirectories()
        {
            return directoryInfo.GetDirectories()
                .Select(directoryInfo => From(directoryInfo))
                .ToImmutableArray();
        }

        public ImmutableArray<FileValue> GetFiles()
        {
            try
            {
                return directoryInfo.GetFiles()
                    .Select(fileInfo => FileValue.From(fileInfo))
                    .ToImmutableArray();
            }
            catch (DirectoryNotFoundException)
            {
                return new ImmutableArray<FileValue>();
            }
            catch (UnauthorizedAccessException)
            {
                return new ImmutableArray<FileValue>();
            }
        }

        public long GetSize()
        {
            var files = directoryInfo.GetFiles();
            var size = files.Sum(fileInfo => fileInfo.Length);

            return size;
        }

        public bool HasFiles()
        {
            var filesArray = GetFiles();
            return filesArray != null && filesArray.Length > 0;
        }

        public bool HasSubDirectories()
        {
            try
            {
                return directoryInfo.GetDirectories().Length > 0;
            }
            catch (DirectoryNotFoundException)
            {
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }
    }
}