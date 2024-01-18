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
    public enum CmdType
    {
        Key,
        Mouse,
        Repeat
    }

    [JsonObject]
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

        [JsonIgnore] public int  Level     => Parent?.Level + 1 ?? 0;
        [JsonIgnore] public bool HasChilds => Childs?.Count > 0;
        private             bool    _isExpanded;
        [JsonIgnore]public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (this.RaiseAndSetIfChanged(ref _isExpanded, value))
                {

                }

            }
        }
        private void ChangedIsExpanded(bool isExpanded)
        {
            OnChangedIsExpanded?.Invoke(isExpanded, this);
        }
        public Action<bool, BaseCmd> OnChangedIsExpanded { get; set; }
        public void Shrink()
        {
            IsExpanded = false;
            foreach (var item in Childs)
                item.Shrink();
        }
        private             ObservableCollectionWithMultiSelectedItem<BaseCmd> _childs;
        public virtual ObservableCollectionWithMultiSelectedItem<BaseCmd> Childs
        {
            get => _childs;
            set => this.RaiseAndSetIfChanged(ref _childs, value);
        }

        protected BaseCmd(int delay, int clickDelay)
        {
            _delay      = delay;
            _clickDelay = clickDelay;
        }

        protected BaseCmd(int delay, int clickDelay, bool ctrl = false, bool shift = false, bool alt = false)
        {
            _ctrl       = ctrl;
            _shift      = shift;
            _alt        = alt;
            _delay      = delay;
            _clickDelay = clickDelay;
        }

        public void AddChild(BaseCmd child)
        {
            if (Childs == null)
                Childs = new ObservableCollectionWithMultiSelectedItem<BaseCmd>();
            Childs.Add(child);
            child.Parent = this;
        }

        [JsonIgnore] public virtual string Text => $"{(Ctrl ? "C" : "")}{(Alt ? "A" : "")}{(Shift ? "S" : "")}{(Ctrl || Alt || Shift ? "+" : "")}";

        [JsonIgnore] public abstract CmdType CmdType { get; }

        public abstract void Execute();
    }
}
