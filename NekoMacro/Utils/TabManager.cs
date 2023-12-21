using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NekoMacro.UI
{
    public class TabManager : ReactiveObject
    {
        private ObservableCollection<Tab> _tabList;

        public ObservableCollection<Tab> TabList
        {
            get => _tabList;
            set => this.RaiseAndSetIfChanged(ref _tabList, value);
        }

        private Tab _selectedTab;

        public Tab SelectedTab
        {
            get => _selectedTab;
            set => this.RaiseAndSetIfChanged(ref _selectedTab, value);
        }

        //public bool AnyRunning => TabList.Any(x => x.IsAccount && x.Account.Running.HasValue);
        //public bool AllStopped => TabList.Where(x => x.IsAccount).All(x => !x.Account.Running.HasValue);

        public TabManager()
        {
            TabList = new ObservableCollection<Tab>();
        }

        public void InitTabs()
        {
            //TabList.Add(new Tab("Db manager", TabType.DbManager));
            //TabList.Add(new Tab("Db data copy", TabType.DbDataCopy));
            TabList.Add(new Tab("All macros", TabType.AllMacro));
            TabList.Add(new Tab("Settings", TabType.Settings));
            //TabList.Add(new Tab("Settings",     TabType.Settings));
            //TabList.Add(new Tab("Settings",     TabType.Settings));
            //TabList.Add(new Tab("Settings",     TabType.Settings));
            //TabList.Add(new Tab("Settings",     TabType.Settings));
            //TabList.Add(new Tab("Settings",     TabType.Settings));
            //TabList.Add(new Tab("Settings",     TabType.Settings));
            //TabList.Add(new Tab("Settings",     TabType.Settings));
            //TabList.Add(new Tab("Settings",     TabType.Settings));
            //TabList.Add(new Tab("Settings",     TabType.Settings));
            //TabList.Add(new Tab("Settings",     TabType.Settings));
            //TabList.Add(new Tab("Settings",     TabType.Settings));
            //TabList.Add(new Tab("Settings",     TabType.Settings));
            //TabList.Add(new Tab("Settings",     TabType.Settings));
            //TabList.Add(new Tab("Settings",     TabType.Settings));
            //TabList.Add(new Tab("Settings",     TabType.Settings));
            //TabList.Add(new Tab("Settings",     TabType.Settings));
            //TabList.Add(new Tab("Settings",     TabType.Settings));
            //TabList.Add(new Tab("Settings",     TabType.Settings));
            SelectedTab = TabList.FirstOrDefault();

            Logger.Info($"Tabs init succ");
        }

        //public void OpenSettingsTab()
        //{
        //    TabList.Add(new Tab(null, "+"));
        //    SelectedTab = TabList.FirstOrDefault();
        //}

        //public void OpenTab(Account acc, bool sw = false, bool auto = false)
        //{
        //    var ids = TabList.Where(x => x.IsAccount).Select(x => x.Account.Id).ToList();
        //    for (var i = 0; i <= ids.Count; i++)
        //    {
        //        if (i < ids.Count)
        //        {
        //            if (acc.Id < ids[i])
        //            {
        //                TabList.Insert(i, new Tab(acc));
        //                break;
        //            }
        //        }
        //        else
        //        {
        //            TabList.Insert(TabList.Count - 1, new Tab(acc));
        //        }
        //    }
        //    if (sw)
        //        SelectedTab = TabList.FirstOrDefault(x => x.Account == acc);
        //    //acc.Start(auto);
        //    RaisePropertyChanged(() => AnyRunning);
        //    RaisePropertyChanged(() => AllStopped);
        //}

        //public void CloseTab(Tab tab)
        //{
        //    if (!tab.IsAccount) return;
        //    if (tab.Account.Closing) return;
        //    var ind = TabList.IndexOf(tab);
        //    tab.Account.Stop();
        //    TabList.Remove(tab);
        //    SelectedTab = ind == 0 ? TabList[0] : TabList[ind - 1];
        //    RaisePropertyChanged(() => AnyRunning);
        //    RaisePropertyChanged(() => AllStopped);
        //}
    }
}
