using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace NekoMacro.Utils
{
    public class DirectionWrapper : ReactiveObject
    {
        private string _dir;
        public string Dir
        {
            get => _dir;
            set => this.RaiseAndSetIfChanged(ref _dir, value);
        }

        public DirectionWrapper()
        {
            
        }

        public DirectionWrapper(string dir)
        {
            _dir = dir;
        }

        public static List<DirectionWrapper> GetList() => new List<DirectionWrapper>() { new DirectionWrapper("Down"), new DirectionWrapper("Up"), new DirectionWrapper("Both") };
    }
}
