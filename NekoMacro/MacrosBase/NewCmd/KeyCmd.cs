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

        public override string Text => $"{(Ctrl ? "Ctrl+" : "")}{(Alt ? "Alt+" : "")}{(Shift ? "Shift+" : "")}{Key}";

        private Keys _key;
        public Keys Key
        {
            get => _key;
            set
            {
                this.RaiseAndSetIfChanged(ref _key, value);
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
