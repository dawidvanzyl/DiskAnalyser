using DiskAnalyser.Models;
using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace DiskAnalyser.Presenters.Models
{
    public interface IDirectoryModel : IFileSystemDescriptionModel
    {
        long FileCount { get; }

        ImmutableArray<IDirectoryModel> GetDirectories();

        ImmutableArray<IFileModel> GetFiles();

        long GetSize();

        bool HasFiles();

        bool HasSubDirectories();
    }

    public sealed class DirectoryModel : IDirectoryModel
    {
        private readonly DirectoryInfo directoryInfo;

        private DirectoryModel(DirectoryInfo directoryInfo)
        {
            this.directoryInfo = directoryInfo;
        }

        public long FileCount => throw new NotImplementedException();
        public string FullName => directoryInfo.FullName;

        public string Name => directoryInfo.Name;

        public static IDirectoryModel From(string drive)
        {
            return new DirectoryModel(new DirectoryInfo(drive));
        }

        public ImmutableArray<IDirectoryModel> GetDirectories()
        {
            return directoryInfo.GetDirectories()
                .Select(directoryInfo => From(directoryInfo))
                .ToImmutableArray();
        }

        public ImmutableArray<IFileModel> GetFiles()
        {
            try
            {
                return directoryInfo.GetFiles()
                    .Select(fileInfo => FileModel.From(fileInfo))
                    .ToImmutableArray();
            }
            catch (DirectoryNotFoundException)
            {
                return new ImmutableArray<IFileModel>();
            }
            catch (UnauthorizedAccessException)
            {
                return new ImmutableArray<IFileModel>();
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

        internal static IDirectoryModel From(DirectoryInfo directoryInfo)
        {
            return new DirectoryModel(directoryInfo);
        }
    }
}