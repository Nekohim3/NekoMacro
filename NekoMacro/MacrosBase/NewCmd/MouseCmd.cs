using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interceptor;
using NekoMacro.Utils;
using Newtonsoft.Json;
using ReactiveUI;

namespace NekoMacro.MacrosBase.NewCmd
{
    public class MouseCmd : BaseCmd
    {
        public override CmdType CmdType => CmdType.Mouse;

        public override string Text => $"{base.Text}{Action}{(Action == MouseKey.Moving ? Pos.ToString() : "")}";

        //public override ObservableCollectionWithMultiSelectedItem<BaseCmd> Childs
        //{
        //    get => null;
        //    set => _ = value;
        //}

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

        private GetMousePos.POINT                                  _pos;
        public GetMousePos.POINT Pos
        {
            get => _pos;
            set => this.RaiseAndSetIfChanged(ref _pos, value);
        }
        

        public MouseCmd(MouseKey action, int delay, int speed = 5000, bool ctrl = false, bool shift = false, bool alt = false) : base(delay, speed, ctrl, shift, alt)
        {
            _action = action;
            _pos = GetMousePos.GetNullPos();
        }

        [JsonConstructor]
        public MouseCmd(MouseKey action, GetMousePos.POINT pos, int delay, int speed = 5000, bool ctrl = false, bool shift = false, bool alt = false) : base(delay, speed, ctrl, shift, alt)
        {
            _action = action;
            _pos    = pos;
        }

        public override void Execute()
        {

        }
    }
}
