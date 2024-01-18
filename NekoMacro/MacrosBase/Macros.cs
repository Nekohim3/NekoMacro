using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NekoMacro.Utils;
using NekoMacro.Utils.TreeDataGrid;
using Newtonsoft.Json;
using ReactiveUI;

namespace NekoMacro.MacrosBase
{
    [JsonObject]
    public class Macros : ReactiveObject
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        private string _desc;
        public string Desc
        {
            get => _desc;
            set => this.RaiseAndSetIfChanged(ref _desc, value);
        }

        private ObservableCollectionWithMultiSelectedItem<BaseCmd> _commands;
        public ObservableCollectionWithMultiSelectedItem<BaseCmd> Commands
        {
            get => _commands;
            set => this.RaiseAndSetIfChanged(ref _commands, value);
        }

        private bool _isChecked;
        [JsonIgnore]
        public bool IsChecked
        {
            get => _isChecked;
            set => this.RaiseAndSetIfChanged(ref _isChecked, value);
        }

        private System.Windows.Forms.Keys _hotkey;
        public System.Windows.Forms.Keys Hotkey
        {
            get => _hotkey;
            set => this.RaiseAndSetIfChanged(ref _hotkey, value);
        }

        public Macros()
        {
            Commands = new ObservableCollectionWithMultiSelectedItem<BaseCmd>() { };
        }


        public Macros(string name) : this()
        {
            _name = name;
        }

        public Macros(string name, string desc) : this()
        {
            _name = name;
            _desc = desc;
        }

        public void AddCommand(BaseCmd cmd)
        {
            Commands.Add(cmd);
        }

        

        public override string ToString()
        {
            return Name;
        }
    }
}
