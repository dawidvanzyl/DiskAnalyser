using DiskAnalyser.Models;
using DiskAnalyser.Presenters;
using DiskAnalyser.Presenters.Composites;
using DiskAnalyser.Presenters.Proxies;
using DiskAnalyser.Tests.Fixtures;
using System;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Xunit;

namespace DiskAnalyser.Tests
{
    [Trait("Collection", "MainPresenter")]    
    public class AnalyseDirectoryAsyncTests : IClassFixture<AnalyseDirectoryFixture>
    {
        private readonly AnalyseDirectoryFixture fixture;
        private readonly MainPresenter mainPresenter;

        public AnalyseDirectoryAsyncTests(AnalyseDirectoryFixture fixture)
        {
            this.fixture = fixture;

            var mainModel = new MainModel();
            mainModel.SelectedDriveChanged("rootDirectory");
            mainPresenter = new MainPresenter(mainModel);
        }

        [Fact]
        public void Should_Calulate_Directory_Size_From_Files()
        {
            //Arrange
            var mockDirectory = fixture.CreateDirectoryProxy();

            //Action
            mainPresenter.DirectoryAnalysed += (_, @event) =>
            {
                Assert.Equal("rootDirectory", @event.Directory);
                Assert.Equal(512, @event.RootDirectoryNode.TotalSize);
            };

            var rootDirectoryNode = mainPresenter.AnalyseDrive();

            Assert.NotNull(rootDirectoryNode);
            Assert.Equal("root", rootDirectoryNode.Name);
            Assert.Equal(512, rootDirectoryNode.TotalSize);
        }

        [Fact]
        public void Should_Have_Directory_Size_Of_Zero()
        {
            //Arrange
            var mockDirectory = fixture.CreateDirectoryProxy(hasFiles: false);

            //Action
            mainPresenter.DirectoryAnalysed += (_, @event) =>
            {
                Assert.Equal("rootDirectory", @event.Directory);
                Assert.Equal(0, @event.RootDirectoryNode.TotalSize);
            };

            var rootDirectoryNode = mainPresenter.AnalyseDrive();

            Assert.NotNull(rootDirectoryNode);
            Assert.Equal("root", rootDirectoryNode.Name);
            Assert.Equal(0, rootDirectoryNode.TotalSize);
        }

        [Fact]
        public void Should_Analyse_Sub_Directories()
        {
            //Arrange
            var mockDirectory = fixture.CreateDirectoryProxy(hasSubDirectory: true);

            //Action
            int directoryAnalysedEvents = 0;
            mainPresenter.DirectoryAnalysed += (_, @event) =>
            {
                if (directoryAnalysedEvents == 0)
                {
                    Assert.Equal("subDirectory", @event.Directory);
                    Assert.Equal(1024, @event.RootDirectoryNode.TotalSize);
                }

                if (directoryAnalysedEvents == 1)
                {
                    Assert.Equal("rootDirectory", @event.Directory);
                    Assert.Equal(1536, @event.RootDirectoryNode.TotalSize);
                }

                directoryAnalysedEvents++;
            };

            var rootDirectoryNode = mainPresenter.AnalyseDrive();

            //Assert
            Assert.Equal(2, directoryAnalysedEvents);

            Assert.NotNull(rootDirectoryNode);
            Assert.Equal("root", rootDirectoryNode.Name);
            Assert.Equal(1536, rootDirectoryNode.TotalSize);

            ImmutableArray<IFileSystemNode> rootCollection = rootDirectoryNode.GetNodes();
            Assert.NotEmpty(rootCollection);
            Assert.True(rootCollection.Length == 2);
            Assert.Equal("sub", rootCollection[0].Name);
            Assert.Equal(1024, rootCollection[0].TotalSize);
            Assert.Equal("mockFile", rootCollection[1].Name);
            Assert.Equal(512, rootCollection[1].TotalSize);

            ImmutableArray<IFileSystemNode> subCollection = rootDirectoryNode.GetNodes()[0].GetNodes();
            Assert.NotEmpty(subCollection);
            Assert.True(subCollection.Length == 1);
            Assert.Equal("mockSubFile", subCollection[0].Name);
            Assert.Equal(1024, subCollection[0].TotalSize);
        }

        [Fact]
        public void Integration_Test()
        {
            //Arrange
            mainPresenter.ChangeSelectedDrive(@"c:\");

            //Action
            mainPresenter.DirectoryAnalysed += (_, @event) => { Console.WriteLine(@event.Directory); };
            var rootDirectoryNode = mainPresenter.AnalyseDrive();

            ////Assert
            //Assert.Equal(2, directoryAnalysedEvents);

            //Assert.NotNull(rootDirectoryNode);
            //Assert.Equal("root", rootDirectoryNode.Name);
            //Assert.Equal(1536, rootDirectoryNode.TotalSize);

            //ImmutableArray<IFileSystemNode> rootCollection = rootDirectoryNode.GetNodes();
            //Assert.NotEmpty(rootCollection);
            //Assert.True(rootCollection.Length == 2);
            //Assert.Equal("sub", rootCollection[0].Name);
            //Assert.Equal(1024, rootCollection[0].TotalSize);
            //Assert.Equal("mockFile", rootCollection[1].Name);
            //Assert.Equal(512, rootCollection[1].TotalSize);

            //ImmutableArray<IFileSystemNode> subCollection = rootDirectoryNode.GetNodes()[0].GetNodes();
            //Assert.NotEmpty(subCollection);
            //Assert.True(subCollection.Length == 1);
            //Assert.Equal("mockSubFile", subCollection[0].Name);
            //Assert.Equal(1024, subCollection[0].TotalSize);
        }
    }
}
