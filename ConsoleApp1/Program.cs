using DiskAnalyser.Models;
using DiskAnalyser.Presenters;
using System;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main()
        {
            var mainModel = new MainModel();
            mainModel.SelectedDriveChanged("rootDirectory");
            var mainPresenter = new MainPresenter(mainModel);

            mainPresenter.ChangeSelectedDrive(@"c:\");

            mainPresenter.DirectoryAnalysed += (_, @event) => Console.WriteLine(@event.Directory);
            mainPresenter.DirectoryNodeAdded += (_, @event) => Console.WriteLine(@event.DirectoryModel.Name);

            var rootDirectoryNode = mainPresenter.AnalyseDrive();
            Console.ReadLine();
        }
    }
}