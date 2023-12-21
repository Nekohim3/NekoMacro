using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NekoMacro.UI;

namespace NekoMacro
{
    public static class g
    {
        public static Settings   Settings   { get; set; }
        public static string     CompName   => Environment.MachineName;
        public static TabManager TabManager { get; set; }

        public static LoadingControlViewModel LoadingControlVM { get; set; }
        public static NMsgViewModel           NMsgVM           { get; set; }

        public static NMsgReply MsgShow(string msg, string title, NMsgButtons buttons) => NMsgVM.Show(msg, title, buttons);
        public static NMsgReply MsgShow(string msg, string title) => NMsgVM.Show(msg, title);

        public static void Init()
        {
            Settings = Settings.Load() ?? new Settings();
            Logger.Info("Settings loaded");
            TabManager = new TabManager();
            TabManager.InitTabs();
        }

        public static void StartLongOperation(Action act, Action fin = null)
        {
            new Thread(() =>
                       {
                           LoadingControlVM.IsVisible   = true;
                           LoadingControlVM.LoadingText = "Prepare operation";
                           act.Invoke();
                           fin?.Invoke();
                           LoadingControlVM.LoadingText += " Done!";
                           Thread.Sleep(1000);
                           LoadingControlVM.IsVisible   = false;
                           LoadingControlVM.LoadingText = "";
                       }).Start();
        }
    }
}
