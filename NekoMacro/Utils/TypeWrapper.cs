using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NekoMacro.MacrosBase;
using ReactiveUI;

namespace NekoMacro.Utils
{
    public class TypeWrapper : ReactiveObject
    {
        private CommandType _type;
        public CommandType Type
        {
            get => _type;
            set => this.RaiseAndSetIfChanged(ref _type, value);
        }
        
        public string Text => Type.ToString();

        public TypeWrapper()
        {

        }

        public TypeWrapper(CommandType type)
        {
            _type = type;
        }

        public override string ToString()
        {
            return Text;
        }

        public static List<TypeWrapper> GetList() => Enum.GetValues(typeof(CommandType)).Cast<CommandType>().Select(x => new TypeWrapper(x)).ToList();
    }
}
