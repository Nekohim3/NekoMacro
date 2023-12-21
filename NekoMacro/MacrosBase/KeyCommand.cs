using Interceptor;
using Newtonsoft.Json;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NekoMacro.MacrosBase
{
    [JsonObject]
    public class KeyCommand : Command
    {
        private Keys _key;
        public Keys Key
        {
            get => _key;
            set => this.RaiseAndSetIfChanged(ref _key, value);
        }

        private KeyState _state;
        public KeyState State
        {
            get => _state;
            set => this.RaiseAndSetIfChanged(ref _state, value);
        }

        public override string TypeE  => "Key";
        public override string KeyE   => Key.ToString();
        public override string StateE => State.ToString().Substring(0, 1);

        public KeyCommand()
        {
            
        }

        public KeyCommand(Keys key, KeyState state)
        {
            _key = key;
            _state = state;
        }

        public override void Execute()
        {
            if (Key == Keys.None || State == KeyState.None)
                return;
        }

        public override string ToString()
        {
            return $"[K]: {Key} | {State}";

        }
    }
}
