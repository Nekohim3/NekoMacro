using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ReactiveUI;

namespace NekoMacro.MacrosBase
{
    public enum CmdType
    {
        Key,
        Mouse,
        Repeat
    }

    public abstract class BaseCmd : ReactiveObject
    {
        private bool _ctrl;
        public bool Ctrl
        {
            get => _ctrl;
            set => this.RaiseAndSetIfChanged(ref _ctrl, value);
        }

        private bool _shift;
        public bool Shift
        {
            get => _shift;
            set => this.RaiseAndSetIfChanged(ref _shift, value);
        }

        private bool _alt;
        public bool Alt
        {
            get => _alt;
            set => this.RaiseAndSetIfChanged(ref _alt, value);
        }

        private int _delay;
        public int Delay
        {
            get => _delay;
            set => this.RaiseAndSetIfChanged(ref _delay, value);
        }

        private int _clickDelay;
        public int ClickDelay
        {
            get => _clickDelay;
            set => this.RaiseAndSetIfChanged(ref _clickDelay, value);
        }

        private BaseCmd _parent;
        public BaseCmd Parent
        {
            get => _parent;
            set => this.RaiseAndSetIfChanged(ref _parent, value);
        }



        public virtual string Text => $"{(Ctrl ? "Ctrl+" : "")}{(Alt ? "Alt+" : "")}{(Shift ? "Shift+" : "")}";

        [JsonIgnore] public abstract CmdType CmdType { get; }

        public abstract void Execute();
    }
}
