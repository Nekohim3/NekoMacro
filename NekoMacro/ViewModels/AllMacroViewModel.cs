using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interceptor;
using NekoMacro.MacrosBase;
using NekoMacro.Utils;
using ReactiveUI;

namespace NekoMacro.ViewModels
{

    public class AllMacroViewModel : ReactiveObject
    {
        private ObservableCollectionWithSelectedItem<Macros> _macrosList;
        public ObservableCollectionWithSelectedItem<Macros> MacrosList
        {
            get => _macrosList;
            set => this.RaiseAndSetIfChanged(ref _macrosList, value);
        }

        private Command _editedCommand;
        public Command EditedCommand
        {
            get => _editedCommand;
            set => this.RaiseAndSetIfChanged(ref _editedCommand, value);
        }

        private ObservableCollectionWithSelectedItem<KeysWrapper> _keyList = new ObservableCollectionWithSelectedItem<KeysWrapper>(KeysWrapper.GetList());
        public ObservableCollectionWithSelectedItem<KeysWrapper> KeyList
        {
            get => _keyList;
            set => this.RaiseAndSetIfChanged(ref _keyList, value);
        }

        private ObservableCollectionWithSelectedItem<MouseWrapper> _mouseList = new ObservableCollectionWithSelectedItem<MouseWrapper>(MouseWrapper.GetList());
        public ObservableCollectionWithSelectedItem<MouseWrapper> MouseList
        {
            get => _mouseList;
            set => this.RaiseAndSetIfChanged(ref _mouseList, value);
        }

        private ObservableCollectionWithSelectedItem<DirectionWrapper> _dirList = new ObservableCollectionWithSelectedItem<DirectionWrapper>(DirectionWrapper.GetList());
        public ObservableCollectionWithSelectedItem<DirectionWrapper> DirList
        {
            get => _dirList;
            set => this.RaiseAndSetIfChanged(ref _dirList, value);
        }

        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set => this.RaiseAndSetIfChanged(ref _isAdd, value);
        }

        private ObservableCollectionWithSelectedItem<TypeWrapper> _typeList = new ObservableCollectionWithSelectedItem<TypeWrapper>(TypeWrapper.GetList());
        public ObservableCollectionWithSelectedItem<TypeWrapper> TypeList
        {
            get => _typeList;
            set => this.RaiseAndSetIfChanged(ref _typeList, value);
        }

        public AllMacroViewModel()
        {
            MacrosList = new ObservableCollectionWithSelectedItem<Macros>();
            var macros = new Macros("Test macros");
            macros.AddCommand(new KeyCommand(Keys.A, KeyState.Down));
            macros.AddCommand(new DelayCommand(50));
            macros.AddCommand(new KeyCommand(Keys.A, KeyState.Up));
            macros.AddCommand(new DelayCommand(150));
            macros.AddCommand(new KeyCommand(Keys.B, KeyState.Down));
            macros.AddCommand(new DelayCommand(50));
            macros.AddCommand(new KeyCommand(Keys.B, KeyState.Up));
            macros.AddCommand(new DelayCommand(150));
            macros.AddCommand(new KeyCommand(Keys.Q, KeyState.Down));
            macros.AddCommand(new DelayCommand(50));
            macros.AddCommand(new KeyCommand(Keys.Q, KeyState.Up));
            macros.AddCommand(new DelayCommand(150));

            macros.AddCommand(new MouseCommand(MouseState.Moving, 123, 234));
            macros.AddCommand(new DelayCommand(50));
            macros.AddCommand(new MouseCommand(MouseState.LeftDown));
            macros.AddCommand(new DelayCommand(50));
            macros.AddCommand(new MouseCommand(MouseState.LeftUp));

            MacrosList.Add(macros);
            MacrosList.SelectionChanged += MacrosListOnSelectionChanged;
        }

        private void MacrosListOnSelectionChanged(ObservableCollectionWithSelectedItem<Macros> sender, Macros newselection, Macros oldselection)
        {
            if (oldselection != null)
                oldselection.Commands.SelectionChanged -= CommandsOnSelectionChanged;
            if (newselection == null)
                return;
            newselection.Commands.SelectionChanged += CommandsOnSelectionChanged;
            if(newselection.Commands.SelectedItem != null)
                EditedCommand = newselection.Commands.SelectedItem;
        }

        private void CommandsOnSelectionChanged(ObservableCollectionWithSelectedItem<Command> sender, Command newselection, Command oldselection)
        {
            if (newselection == null)
            {
                EditedCommand = null;
                return;
            }
            EditedCommand         = newselection;
            TypeList.SelectedItem = TypeList.First(_ => _.Type == newselection.Type);
        }
    }
}
