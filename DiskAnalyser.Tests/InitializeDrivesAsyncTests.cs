using DiskAnalyser.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DiskAnalyser.Tests
{
    [Trait("Collection", "MainPresenter")]
    public class InitializeDrivesAsyncTests : IClassFixture<Fixture>
    {
        private readonly Fixture fixture;

        public InitializeDrivesAsyncTests(Fixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async void Should_Initialize_Drives()
        {
            //Arrange
            var expectedDrives = new Dictionary<string, string>
                {
                    { @"c:\", @"alpha (c:\)" },
                    { @"d:\", @"gamma (d:\)" }
                };

            //Action
            fixture.MainPresenter.DrivesInitialized += (_, @event) =>
            {
                Assert.All(
                    @event.Drives,
                    (drive) =>
                    {
                        Assert.True(expectedDrives.ContainsKey(drive.Key.ToLower()));
                        Assert.True(expectedDrives.ContainsValue(drive.Value.ToLower()));
                    });
            };

            fixture.MainPresenter.InitializeDrives();
        }
    }
}
