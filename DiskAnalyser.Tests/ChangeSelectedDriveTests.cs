using DiskAnalyser.Tests.Fixtures;
using Xunit;

namespace DiskAnalyser.Tests
{
    [Trait("Collection", "MainPresenter")]
    public class ChangeSelectedDriveTests : IClassFixture<Fixture>
    {
        private readonly Fixture fixture;

        public ChangeSelectedDriveTests(Fixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void Should_Change_Selected_Drive()
        {
            //Arrange
            var expectedDrive = @"c:\";

            //Action
            fixture.MainPresenter.ChangeSelectedDrive(expectedDrive);

            //Assert
            Assert.Equal(expectedDrive, fixture.MainModel.Drive);
        }
    }
}
