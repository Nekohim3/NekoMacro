using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using NekoMacro.ViewModels;
using NekoMacro.Views;
//using NekoMacro.ViewModels;
//using NekoMacro.Views;
using ReactiveUI;

namespace NekoMacro.UI
{
    public enum TabType
    {
        MacroCombo  = 0,
        MacroEditor = 1,
        Settings   = 99
    }

    public class Tab : ReactiveObject
    {
        private string _tabName;

        public string TabName
        {
            get => _tabName;
            set => this.RaiseAndSetIfChanged(ref _tabName, value);
        }

        private bool _isClosable;

        public bool IsClosable
        {
            get => _isClosable;
            set => this.RaiseAndSetIfChanged(ref _isClosable, value);
        }

        private Page _page;

        public Page Page
        {
            get => _page;
            set => this.RaiseAndSetIfChanged(ref _page, value);
        }

        public ReactiveCommand<Unit, Unit> CloseCmd { get; }

        public Tab(string tabName, TabType type, bool isClosable = false)
        {
            TabName    = tabName;
            IsClosable = isClosable;
            //if (type == TabType.DbManager)
            //{
            //    Page             = new DBManagerView();
            //    Page.DataContext = new DbManagerViewModel();
            //}
            //if (type == TabType.DbDataCopy)
            //{
            //    Page             = new DBCopyManagerView();
            //    Page.DataContext = new DBCopyManagerViewModel();
            //}
            //if (type == TabType.AllMacro)
            //{
            //    Page             = new AllMacroView();
            //    Page.DataContext = new AllMacroViewModel();
            //}
            if (type == TabType.MacroEditor)
            {
                Page             = new MacroEditor();
                Page.DataContext = new MacroEditViewModel();
            }
            if (type == TabType.Settings)
            {
                Page = new SettingsView();
                Page.DataContext = new SettingsViewModel();
            }
            CloseCmd = ReactiveCommand.Create(OnClose);
        }

        private void OnClose()
        {
            //g.TabManager.CloseTab(this);
        }
    }
}
