using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NekoMacro.Utils;
using ReactiveUI;

namespace NekoMacro.MacrosBase
{
    public class ExecList : ReactiveObject
    {

        private ObservableCollectionWithSelectedItem<ObservableCollectionWithSelectedItem<Macros>> _macrosGrid;
        public ObservableCollectionWithSelectedItem<ObservableCollectionWithSelectedItem<Macros>> MacrosGrid
        {
            get => _macrosGrid;
            set => this.RaiseAndSetIfChanged(ref _macrosGrid, value);
        }

        public ExecList()
        {

        }
    }
}
