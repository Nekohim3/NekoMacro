using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interceptor;
using ReactiveUI;

namespace NekoMacro.MacrosBase
{
    public class Command : ReactiveObject
    {
        private bool _type;
        public bool Type
        {
            get => _type;
            set => this.RaiseAndSetIfChanged(ref _type, value);
        }

        private ushort _code;
        public ushort Code
        {
            get => _code;
            set => this.RaiseAndSetIfChanged(ref _code, value);
        }

        private int _x;
        public int X
        {
            get => _x;
            set => this.RaiseAndSetIfChanged(ref _x, value);
        }

        private int _y;
        public int Y
        {
            get => _y;
            set => this.RaiseAndSetIfChanged(ref _y, value);
        }

        private bool _abs;
        public bool Abs
        {
            get => _abs;
            set => this.RaiseAndSetIfChanged(ref _abs, value);
        }

        public bool IsKeyCmd => Type;
        public Keys Key      => IsKeyCmd ? (Keys)Code : Keys.None;
        public MouseState => 
    }
}
