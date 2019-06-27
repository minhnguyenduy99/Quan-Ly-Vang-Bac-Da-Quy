using UIProject.ViewModels;
using UIProject.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using ModelProject;
using System.Data.SQLite;
using Dapper;
using System.Threading;

namespace UIProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var cultureInfo = new System.Globalization.CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            base.OnStartup(e);
        }
    }
}
