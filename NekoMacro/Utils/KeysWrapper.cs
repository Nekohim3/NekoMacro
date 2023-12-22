using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interceptor;
using ReactiveUI;

namespace NekoMacro.Utils
{
    public class KeysWrapper : ReactiveObject
    {
        private Keys _key;
        public Keys Key
        {
            get => _key;
            set => this.RaiseAndSetIfChanged(ref _key, value);
        }

        public string Text => Key.ToString();

        public KeysWrapper()
        {
            
        }

        public KeysWrapper(Keys key)
        {
            _key = key;
        }

        public static List<KeysWrapper> GetList() => Enum.GetValues(typeof(Keys)).Cast<Keys>().Select(x => new KeysWrapper(x)).ToList();
    }
}
