using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interceptor;
using NekoMacro.Utils;
using ReactiveUI;

namespace NekoMacro.MacrosBase.NewCmd
{
    public class KeyCmd : BaseCmd
    {
        public override CmdType CmdType => CmdType.Key;

        //public override ObservableCollectionWithMultiSelectedItem<BaseCmd> Childs
        //{
        //    get => null;
        //    set => _ = value;
        //}

        public override string Text => $"{base.Text}{Action}";

        private Keys _action;
        public Keys Action
        {
            get => _action;
            set
            {
                this.RaiseAndSetIfChanged(ref _action, value);
                this.RaisePropertyChanged(Text);
            }
        }
        
        public KeyCmd(Keys action, int delay, int clickDelay, bool ctrl = false, bool shift = false, bool alt = false) : base(delay, clickDelay, ctrl, shift, alt)
        {
            _action = action;
        }

        public override void Execute()
        {
            
        }
    }
}
