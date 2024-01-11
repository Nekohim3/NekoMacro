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

        public override string Text => $"{Count} times";

        private int _count;
        public int Count
        {
            get => _count;
            set
            {
                this.RaiseAndSetIfChanged(ref _count, value);
                this.RaisePropertyChanged(Text);
            }
        }

        private ObservableCollectionWithMultiSelectedItem<BaseCmd> _commandList;
        public ObservableCollectionWithMultiSelectedItem<BaseCmd> CommandList
        {
            get => _commandList;
            set => this.RaiseAndSetIfChanged(ref _commandList, value);
        }

        public RepeatCmd()
        {
            
        }

        public override void Execute()
        {
            
        }
    }
}
