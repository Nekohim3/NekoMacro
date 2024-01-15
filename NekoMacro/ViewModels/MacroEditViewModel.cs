using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Interceptor;
using NekoMacro.MacrosBase;
using NekoMacro.MacrosBase.NewCmd;
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
        
        public ReactiveCommand<Unit, Unit> AddMacrosCmd                  { get; }
        public ReactiveCommand<Unit, Unit> DeleteMacrosCmd               { get; }
        public ReactiveCommand<Unit, Unit> ImportCmd                     { get; }
        public ReactiveCommand<Unit, Unit> ExportCmd                     { get; }
        public ReactiveCommand<Unit, Unit> SetClickDelayForSelectedCmd   { get; }
        public ReactiveCommand<Unit, Unit> SetBetweenDelayForSelectedCmd { get; }
        

        public MacroEditViewModel()
        {
            AddMacrosCmd                  = ReactiveCommand.Create(OnAddMacros);
            DeleteMacrosCmd               = ReactiveCommand.Create(OnDeleteMacros);
            ExportCmd                     = ReactiveCommand.Create(OnExport);
            ImportCmd                     = ReactiveCommand.Create(OnImport);
            SetClickDelayForSelectedCmd   = ReactiveCommand.Create(OnSetClickDelayForSelected);
            SetBetweenDelayForSelectedCmd = ReactiveCommand.Create(OnSetBetweenDelayForSelected);

            GlobalDriver.KeyPressSubscribe(KeyPressHandler);
            GlobalDriver.MousePressSubscribe(MousePressHandler);
            TestInitMacros();
        }

        public void TestInitMacros()
        {
            MacrosList = new ObservableCollectionWithSelectedItem<Macros>();
            var macros = new Macros("Test1");
            macros.AddCommand(new KeyCmd(Keys.Q, 100, 50));

            macros.AddCommand(new KeyCmd(Keys.W, 100, 50, ctrl: true));
            macros.AddCommand(new KeyCmd(Keys.E, 100, 50, alt: true));
            macros.AddCommand(new KeyCmd(Keys.R, 100, 50, shift: true));
            macros.AddCommand(new KeyCmd(Keys.T, 100, 50, ctrl: true, alt: true));
            macros.AddCommand(new KeyCmd(Keys.Y, 100, 50, ctrl: true, shift: true));
            macros.AddCommand(new KeyCmd(Keys.U, 100, 50, alt: true, shift: true));
            macros.AddCommand(new KeyCmd(Keys.I, 100, 50, ctrl: true, alt: true, shift: true));
            var rcmd = new RepeatCmd(100,        50);
            rcmd.AddChild(new MouseCmd(MouseKey.Left, 100, 50));
            macros.AddCommand(rcmd);
            macros.AddCommand(new MouseCmd(MouseKey.Left, 100, 50));
            macros.AddCommand(new MouseCmd(MouseKey.Right, 100, 50));
            macros.AddCommand(new MouseCmd(MouseKey.Middle, 100, 50));
            macros.AddCommand(new MouseCmd(MouseKey.X1, 100, 50));
            macros.AddCommand(new MouseCmd(MouseKey.X2, 100, 50));
            macros.AddCommand(new MouseCmd(MouseKey.Left, 100, 50, ctrl: true));
            macros.AddCommand(new MouseCmd(MouseKey.Left, 100, 50, alt: true));
            macros.AddCommand(new MouseCmd(MouseKey.Left, 100, 50, shift: true));
            macros.AddCommand(new MouseCmd(MouseKey.Left, 100, 50, ctrl: true, alt: true));
            macros.AddCommand(new MouseCmd(MouseKey.Left, 100, 50, ctrl: true, shift: true));
            macros.AddCommand(new MouseCmd(MouseKey.Left, 100, 50, alt: true, shift: true));
            macros.AddCommand(new MouseCmd(MouseKey.Left, 100, 50, ctrl: true, alt: true, shift: true));
            macros.AddCommand(new MouseCmd(MouseKey.Moving, new GetMousePos.POINT(100, 150), 300));
            macros.AddCommand(new MouseCmd(MouseKey.Moving, new GetMousePos.POINT(1000, 1500), 300));
            macros.AddCommand(new MouseCmd(MouseKey.Moving, new GetMousePos.POINT(1500, 150), 300));

            MacrosList.Add(macros);

        }

        private void MousePressHandler(object sender, MousePressedEventArgs e)
        {
            //if (e.State == MouseState.Moving)
            //    return;
            //var pos = GetMousePos.GetCursorPosition();
            //Logger.Info($"{e.State} / {pos.X} / {pos.Y}");
        }

        private void KeyPressHandler(object sender, KeyPressedEventArgs e)
        {
            
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

        private void OnSetBetweenDelayForSelected()
        {

        }
        
        private void OnSetClickDelayForSelected()
        {

        }
    }
}
