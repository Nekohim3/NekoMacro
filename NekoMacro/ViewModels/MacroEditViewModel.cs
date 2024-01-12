using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
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

        private int _clickDelay;
        public int ClickDelay
        {
            get => _clickDelay;
            set => this.RaiseAndSetIfChanged(ref _clickDelay, value);
        }

        private int _betweenDelay;
        public int BetweenDelay
        {
            get => _betweenDelay;
            set => this.RaiseAndSetIfChanged(ref _betweenDelay, value);
        }


        public ReactiveCommand<Unit, Unit> AddMacrosCmd    { get; }
        public ReactiveCommand<Unit, Unit> DeleteMacrosCmd { get; }
        public ReactiveCommand<Unit, Unit> ImportCmd       { get; }
        public ReactiveCommand<Unit, Unit> ExportCmd       { get; }
        


        public MacroEditViewModel()
        {

            AddMacrosCmd    = ReactiveCommand.Create(OnAddMacros);
            DeleteMacrosCmd = ReactiveCommand.Create(OnDeleteMacros);
            ExportCmd       = ReactiveCommand.Create(OnExport);
            ImportCmd       = ReactiveCommand.Create(OnImport);
        }

        

        private void OnAddMacros()
        {

        }
        
        private void OnDeleteMacros()
        {

        }

        private void OnImport()
        {

        }

        private void OnExport()
        {

        }

    }
}
