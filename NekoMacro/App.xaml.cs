using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using NekoMacro.UI;

namespace NekoMacro
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Skin Skin { get; set; } = Skin.DarkRed;
        protected override void OnStartup(StartupEventArgs e)
        {
            Logger.Init();
            AppDomain.CurrentDomain.UnhandledException       += CurrentDomain_UnhandledException;
            Current.DispatcherUnhandledException += CurrentOnDispatcherUnhandledException;
            GlobalDriver.Load();
            g.Init();
            base.OnStartup(e);

//#if DEBUG

//#else
//#endif


            var f  = new MainWindow();
            var vm = new MainWindowViewModel();
            f.DataContext = vm;
            f.ShowDialog();

        }

        public void ChangeSkin(Skin newSkin)
        {
            Skin = newSkin;

            foreach (ResourceDictionary dict in Resources.MergedDictionaries)
            {

                if (dict is SkinResourceDictionary skinDict)
                    skinDict.UpdateSource();
                else
                    dict.Source = dict.Source;
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            GlobalDriver.Unload();
            Logger.ErrorQ(e.ExceptionObject as Exception, "CD");
            //Process.GetCurrentProcess().Kill();
        }

        private void CurrentOnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            GlobalDriver.Unload();
            Logger.ErrorQ(e.Exception, "UE");
            //Process.GetCurrentProcess().Kill();
        }
    }
}
