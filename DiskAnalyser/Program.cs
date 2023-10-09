using DiskAnalyser.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace DiskAnalyser
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var serviceProvider = ServiceProviderFactory.CreateServiceCollection()
                .AddTransient<main>()
                .AddTransient<analyse>()
                .BuildServiceProvider();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var main = serviceProvider.GetRequiredService<main>();
            Application.Run(main);
        }
    }
}