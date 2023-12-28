using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace NekoMacro.Utils
{
    public class EnumWrapper<T> : ReactiveObject where T : Enum
    {
        private T _value;
        public T Value
        {
            get => _value;
            set => this.RaiseAndSetIfChanged(ref _value, value);
        }

        public string Text => Value.ToString();

        public EnumWrapper()
        {
            
        }

        public EnumWrapper(T t)
        {
            _value = t;
        }

        public override string ToString()
        {
            return Text;
        }

        public static List<EnumWrapper<T>> GetList() => Enum.GetValues(typeof(T)).Cast<T>().Select(_ => new EnumWrapper<T>(_)).ToList();
        public static List<T> GetListBase() => Enum.GetValues(typeof(T)).Cast<T>().ToList();
    }
}
