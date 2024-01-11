using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interceptor;
using ReactiveUI;

namespace NekoMacro.MacrosBase.NewCmd
{
    public class KeyCmd : BaseCmd
    {
        public override CmdType CmdType => CmdType.Key;

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

        public KeyCmd()
        {
            
        }

        public override void Execute()
        {
            
        }
    }
}
