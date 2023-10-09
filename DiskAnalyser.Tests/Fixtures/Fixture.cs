using DiskAnalyser.Models;
using DiskAnalyser.Presenters;

namespace DiskAnalyser.Tests.Fixtures
{
    public class Fixture
    {
        public Fixture()
        {
            MainModel = new MainModel();
            MainPresenter = new MainPresenter(MainModel);
        }

        public MainModel MainModel { get; private set; }
        public MainPresenter MainPresenter { get; private set; }
    }
}
