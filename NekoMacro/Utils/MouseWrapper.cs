using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interceptor;
using ReactiveUI;
using static System.Net.Mime.MediaTypeNames;

namespace NekoMacro.Utils
{
    public class MouseWrapper : ReactiveObject
    {
        private MouseState _state;
        public MouseState State
        {
            get => _state;
            set => this.RaiseAndSetIfChanged(ref _state, value);
        }

        public MouseWrapper()
        {
            
        }

        public MouseWrapper(MouseState state)
        {
            _state = state;
        }

        public override string ToString()
        {
            return State.ToString();
        }

        public static List<MouseWrapper> GetList() => Enum.GetValues(typeof(MouseState)).Cast<MouseState>().Select(_ => new MouseWrapper(_)).ToList();
    }
}
