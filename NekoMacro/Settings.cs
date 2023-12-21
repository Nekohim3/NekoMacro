using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NekoMacro
{
    public enum Skin { DarkRed, DarkBlue, LightRed, LightBlue }
    public class Settings
    {
        private Skin _theme;

        public Skin Theme
        {
            get => _theme;
            set
            {
                _theme = value;
                (App.Current as App).ChangeSkin(_theme);
            }
        }

        public string DirForDbData { get; set; }

        public Settings()
        {

        }

        public void SetDir(string path)
        {
            DirForDbData = path;
            Save();
        }

        public static Settings Load()
        {
            return !File.Exists("config.cfg") ? null : JsonConvert.DeserializeObject<Settings>(File.ReadAllText("config.cfg"));
        }

        public void Save()
        {
            File.WriteAllText("config.cfg", JsonConvert.SerializeObject(this));
        }
    }
}
