using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReactiveUI;

namespace NekoMacro.ViewModels
{
    public class SettingsViewModel : ReactiveObject
    {
        #region Properties
        

        private bool _darkRedThemeChecked;

        public bool DarkRedThemeChecked
        {
            get => _darkRedThemeChecked;
            set
            {
                if (!value && !_darkBlueThemeChecked && !_lightRedThemeChecked && !_lightBlueThemeChecked) return;
                this.RaiseAndSetIfChanged(ref _darkRedThemeChecked, value);
                if (_darkRedThemeChecked)
                {
                    DarkBlueThemeChecked = false;
                    LightBlueThemeChecked = false;
                    LightRedThemeChecked = false;
                    (App.Current as App).ChangeSkin(Skin.DarkRed);
                    g.Settings.Theme = Skin.DarkRed;
                    g.Settings.Save();
                }
            }
        }

        private bool _darkBlueThemeChecked;

        public bool DarkBlueThemeChecked
        {
            get => _darkBlueThemeChecked;
            set
            {
                if (!value && !_darkRedThemeChecked && !_lightRedThemeChecked && !_lightBlueThemeChecked) return;
                this.RaiseAndSetIfChanged(ref _darkBlueThemeChecked, value);
                if (_darkBlueThemeChecked)
                {
                    DarkRedThemeChecked = false;
                    LightBlueThemeChecked = false;
                    LightRedThemeChecked = false;
                    (App.Current as App).ChangeSkin(Skin.DarkBlue);
                    g.Settings.Theme = Skin.DarkBlue;
                    g.Settings.Save();
                }
            }
        }

        private bool _lightRedThemeChecked;

        public bool LightRedThemeChecked
        {
            get => _lightRedThemeChecked;
            set
            {
                if (!value && !_darkBlueThemeChecked && !_darkRedThemeChecked && !_lightBlueThemeChecked) return;
                this.RaiseAndSetIfChanged(ref _lightRedThemeChecked, value);
                if (_lightRedThemeChecked)
                {
                    DarkBlueThemeChecked = false;
                    LightBlueThemeChecked = false;
                    DarkRedThemeChecked = false;
                    (App.Current as App).ChangeSkin(Skin.LightRed);
                    g.Settings.Theme = Skin.LightRed;
                    g.Settings.Save();
                }
            }
        }

        private bool _lightBlueThemeChecked;

        public bool LightBlueThemeChecked
        {
            get => _lightBlueThemeChecked;
            set
            {
                if (!value && !_darkBlueThemeChecked && !_lightRedThemeChecked && !_darkRedThemeChecked) return;
                this.RaiseAndSetIfChanged(ref _lightBlueThemeChecked, value);
                if (_lightBlueThemeChecked)
                {
                    DarkBlueThemeChecked = false;
                    DarkRedThemeChecked = false;
                    LightRedThemeChecked = false;
                    (App.Current as App).ChangeSkin(Skin.LightBlue);
                    g.Settings.Theme = Skin.LightBlue;
                    g.Settings.Save();
                }
            }
        }

        #endregion

        #region Commands
        

        #endregion

        #region Ctor

        public SettingsViewModel()
        {
            
            
            if (App.Skin == Skin.DarkRed)
                _darkRedThemeChecked = true;
            else if (App.Skin == Skin.DarkBlue)
                _darkBlueThemeChecked = true;
            else if (App.Skin == Skin.LightRed)
                _lightRedThemeChecked = true;
            else
                _lightBlueThemeChecked = true;
            this.RaisePropertyChanged("DarkRedThemeChecked");
            this.RaisePropertyChanged("DarkBlueThemeChecked");
            this.RaisePropertyChanged("LightRedThemeChecked");
            this.RaisePropertyChanged("LightBlueThemeChecked");
        }

        #endregion

        #region CmdExec
        

        #endregion

        #region Funcs



        #endregion

    }
}
