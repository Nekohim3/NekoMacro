using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NekoMacro.MacrosBase;
using NekoMacro.Utils;
using ReactiveUI;

namespace NekoMacro.ViewModels
{
    public class MacroEditViewModel : ReactiveObject
    {
        private ObservableCollectionWithSelectedItem<Macros> _macrosList;
        public ObservableCollectionWithSelectedItem<Macros> MacrosList
        {
            get => _macrosList;
            set => this.RaiseAndSetIfChanged(ref _macrosList, value);
        }

        private bool _noDelay;
        public bool NoDelay
        {
            get => _noDelay;
            set => this.RaiseAndSetIfChanged(ref _noDelay, value);
        }

        private bool _recordDelay;
        public bool RecordDelay
        {
            get => _recordDelay;
            set => this.RaiseAndSetIfChanged(ref _recordDelay, value);
        }

        private bool _staticDelay;
        public bool StaticDelay
        {
            get => _staticDelay;
            set => this.RaiseAndSetIfChanged(ref _staticDelay, value);
        }

        private int _delay;
        public int Delay
        {
            get => _delay;
            set => this.RaiseAndSetIfChanged(ref _delay, value);
        }

        public MacroEditViewModel()
        {
            
        }

        public ReactiveCommand<Unit, Unit> AddMacrosCmd { get; }

        AddMacrosCmd = ReactiveCommand.Create(OnAddMacros);

        private void OnAddMacros()
        {

        }

        public ReactiveCommand<Unit, Unit> DeleteMacrosCmd { get; }

        DeleteMacrosCmd = ReactiveCommand.Create(OnDeleteMacros);

        private void OnDeleteMacros()
        {

        }

        
        
    }
}
