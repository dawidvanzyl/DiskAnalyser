using DiskAnalyser.Presenters.Proxies;
using Moq;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DiskAnalyser.Tests.Fixtures
{
    public class AnalyseDirectoryFixture
    {
        public IDirectoryProxy CreateDirectoryProxy(bool hasSubDirectory = false, bool hasFiles = true)
        {
            var rootDirectoryMock = new Mock<IDirectoryProxy>();
            rootDirectoryMock.Setup(mock => mock.Name).Returns("root");
            rootDirectoryMock.Setup(mock => mock.FullName).Returns("rootDirectory");
            rootDirectoryMock.Setup(mock => mock.HasSubDirectories()).Returns(hasSubDirectory);

            if (hasFiles)
            {
                var mockFile = new Mock<IFileProxy>();
                mockFile.Setup(mock => mock.Name).Returns("mockFile");
                mockFile.Setup(mock => mock.Size).Returns(512);
                rootDirectoryMock.Setup(mock => mock.GetFiles()).Returns(new List<IFileProxy> { mockFile.Object }.ToImmutableArray());
            }
            else
            {
                rootDirectoryMock.Setup(mock => mock.GetFiles()).Returns(new List<IFileProxy> {  }.ToImmutableArray());
            }

            if (hasSubDirectory)
            {
                var subDirectoryMock = new Mock<IDirectoryProxy>();
                subDirectoryMock.Setup(mock => mock.Name).Returns($"sub");
                subDirectoryMock.Setup(mock => mock.FullName).Returns($"subDirectory");
                subDirectoryMock.Setup(mock => mock.HasSubDirectories()).Returns(false);                

                if (hasFiles)
                {
                    var mockFile = new Mock<IFileProxy>();
                    mockFile.Setup(mock => mock.Name).Returns("mockSubFile");
                    mockFile.Setup(mock => mock.Size).Returns(1024);
                    subDirectoryMock.Setup(mock => mock.GetFiles()).Returns(new List<IFileProxy> { mockFile.Object }.ToImmutableArray());
                }
                else
                {
                    subDirectoryMock.Setup(mock => mock.GetFiles()).Returns(new List<IFileProxy> { }.ToImmutableArray());
                }

                rootDirectoryMock.Setup(mock => mock.GetDirectories()).Returns(new List<IDirectoryProxy> { subDirectoryMock.Object }.ToImmutableArray());
            }            

            return rootDirectoryMock.Object;
        }
    }
}
