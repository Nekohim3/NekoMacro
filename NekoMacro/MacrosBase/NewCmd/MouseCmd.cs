using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interceptor;
using ReactiveUI;

namespace NekoMacro.MacrosBase.NewCmd
{
    public class MouseCmd : BaseCmd
    {
        public override CmdType CmdType => CmdType.Mouse;

        public override string Text => $"{base.Text}{Action}";

        private MouseKey _action;
        public MouseKey Action
        {
            get => _action;
            set
            {
                this.RaiseAndSetIfChanged(ref _action, value);
                this.RaisePropertyChanged(Text);
            }
        }

        public MouseCmd()
        {

        }

        public override void Execute()
        {

        }
    }
}
