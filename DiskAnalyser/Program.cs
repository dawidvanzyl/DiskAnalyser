using DiskAnalyser.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace DiskAnalyser
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var serviceProvider = ServiceProviderFactory.CreateServiceCollection()
                .AddSingleton<main>()
                .BuildServiceProvider();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var main = serviceProvider.GetService(typeof(main)) as main;
            Application.Run(main);
        }
    }
}
