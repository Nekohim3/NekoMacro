using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NekoMacro.Utils;
using ReactiveUI;

namespace NekoMacro.MacrosBase
{
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

        private bool _hideDelay;
        public bool HideDelay
        {
            get => _hideDelay;
            set
            {
                this.RaiseAndSetIfChanged(ref _hideDelay, value);
                Commands.Filter = value ? (Func<Command, bool>)(c => !(c is DelayCommand)) : null;
            }
        }

        private ObservableCollectionWithSelectedItem<Command> _commands;
        public ObservableCollectionWithSelectedItem<Command> Commands
        {
            get => _commands;
            set => this.RaiseAndSetIfChanged(ref _commands, value);
        }

        public Macros()
        {
            Commands = new ObservableCollectionWithSelectedItem<Command>() { };
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

        public void AddCommand(Command cmd)
        {
            Commands.Add(cmd);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
