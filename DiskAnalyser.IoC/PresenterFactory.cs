using DiskAnalyser.Models;

namespace DiskAnalyser.Presenters
{
    public interface IPresenterFactory
    {
        IMainPresenter CreateMainPresenter();
    }

    public class PresenterFactory : IPresenterFactory
    {
        public IMainPresenter CreateMainPresenter()
        {
            return new MainPresenter(new MainModel());
        }
    }
}
