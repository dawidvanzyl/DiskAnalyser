using DiskAnalyser.Presenters.Models;
using Moq;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DiskAnalyser.Tests.Fixtures
{
    public class AnalyseDirectoryFixture
    {
        public IDirectoryModel CreateDirectoryProxy(bool hasSubDirectory = false, bool hasFiles = true)
        {
            var rootDirectoryMock = new Mock<IDirectoryModel>();
            rootDirectoryMock.Setup(mock => mock.Name).Returns("root");
            rootDirectoryMock.Setup(mock => mock.FullName).Returns("rootDirectory");
            rootDirectoryMock.Setup(mock => mock.HasSubDirectories()).Returns(hasSubDirectory);

            if (hasFiles)
            {
                var mockFile = new Mock<IFileModel>();
                mockFile.Setup(mock => mock.Name).Returns("mockFile");
                mockFile.Setup(mock => mock.Size).Returns(512);
                rootDirectoryMock.Setup(mock => mock.GetFiles()).Returns(new List<IFileModel> { mockFile.Object }.ToImmutableArray());
            }
            else
            {
                rootDirectoryMock.Setup(mock => mock.GetFiles()).Returns(new List<IFileModel> {  }.ToImmutableArray());
            }

            if (hasSubDirectory)
            {
                var subDirectoryMock = new Mock<IDirectoryModel>();
                subDirectoryMock.Setup(mock => mock.Name).Returns($"sub");
                subDirectoryMock.Setup(mock => mock.FullName).Returns($"subDirectory");
                subDirectoryMock.Setup(mock => mock.HasSubDirectories()).Returns(false);                

                if (hasFiles)
                {
                    var mockFile = new Mock<IFileModel>();
                    mockFile.Setup(mock => mock.Name).Returns("mockSubFile");
                    mockFile.Setup(mock => mock.Size).Returns(1024);
                    subDirectoryMock.Setup(mock => mock.GetFiles()).Returns(new List<IFileModel> { mockFile.Object }.ToImmutableArray());
                }
                else
                {
                    subDirectoryMock.Setup(mock => mock.GetFiles()).Returns(new List<IFileModel> { }.ToImmutableArray());
                }

                rootDirectoryMock.Setup(mock => mock.GetDirectories()).Returns(new List<IDirectoryModel> { subDirectoryMock.Object }.ToImmutableArray());
            }            

            return rootDirectoryMock.Object;
        }
    }
}
