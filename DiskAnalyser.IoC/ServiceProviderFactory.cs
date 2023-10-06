using DiskAnalyser.Presenters;
using Microsoft.Extensions.DependencyInjection;

namespace DiskAnalyser.IoC
{
    public static class ServiceProviderFactory
    {
        public static IServiceCollection CreateServiceCollection()
        {
            return new ServiceCollection()
                 .AddLogging()
                 .AddSingleton<IMainPresenter, MainPresenter>()
                 .AddSingleton<IAnalysePresenter, AnalysePresenter>();
        }
    }
}