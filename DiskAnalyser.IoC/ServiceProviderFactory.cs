using DiskAnalyser.Models;
using DiskAnalyser.Presenters;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DiskAnalyser.IoC
{
    public static class ServiceProviderFactory
    {
        public static IServiceCollection CreateServiceCollection()
        {
            return new ServiceCollection()
                 .AddLogging()
                 .AddSingleton<IPresenterFactory, PresenterFactory>();
        }
    }
}
