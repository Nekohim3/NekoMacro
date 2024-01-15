using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NekoMacro.Utils;
using ReactiveUI;

namespace NekoMacro.MacrosBase.NewCmd
{
    public class RepeatCmd : BaseCmd
    {
        public override CmdType CmdType => CmdType.Repeat;

        public override string Text => $"{ClickDelay} times";
        
        public RepeatCmd(int delay, int count) : base(delay, count)
        {
            
        }

        public override void Execute()
        {
            
        }
    }
}
