using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        private ObservableCollectionWithSelectedItem<EnumWrapper<Keys>> _keyList = new ObservableCollectionWithSelectedItem<EnumWrapper<Keys>>(EnumWrapper<Keys>.GetList());
        public ObservableCollectionWithSelectedItem<EnumWrapper<Keys>> KeyList
        {
            get => _keyList;
            set => this.RaiseAndSetIfChanged(ref _keyList, value);
        }

        private ObservableCollectionWithSelectedItem<EnumWrapper<KeyState>> _keyStateList = new ObservableCollectionWithSelectedItem<EnumWrapper<KeyState>>(EnumWrapper<KeyState>.GetList());
        public ObservableCollectionWithSelectedItem<EnumWrapper<KeyState>> KeyStateList
        {
            get => _keyStateList;
            set => this.RaiseAndSetIfChanged(ref _keyStateList, value);
        }

        private ObservableCollectionWithSelectedItem<EnumWrapper<MouseButton>> _mouseButtonList = new ObservableCollectionWithSelectedItem<EnumWrapper<MouseButton>>(EnumWrapper<MouseButton>.GetList());
        public ObservableCollectionWithSelectedItem<EnumWrapper<MouseButton>> MouseButtonList
        {
            get => _mouseButtonList;
            set => this.RaiseAndSetIfChanged(ref _mouseButtonList, value);
        }

        private ObservableCollectionWithSelectedItem<EnumWrapper<MouseDir>> _mouseDirList = new ObservableCollectionWithSelectedItem<EnumWrapper<MouseDir>>(EnumWrapper<MouseDir>.GetList());
        public ObservableCollectionWithSelectedItem<EnumWrapper<MouseDir>> MouseDirList
        {
            get => _mouseDirList;
            set => this.RaiseAndSetIfChanged(ref _mouseDirList, value);
        }

        private int _delay;
        public int Delay
        {
            get => _delay;
            set => this.RaiseAndSetIfChanged(ref _delay, value);
        }

        //private ObservableCollectionWithSelectedItem<KeysWrapper> _keyList = new ObservableCollectionWithSelectedItem<KeysWrapper>(KeysWrapper.GetList());
        //public ObservableCollectionWithSelectedItem<KeysWrapper> KeyList
        //{
        //    get => _keyList;
        //    set => this.RaiseAndSetIfChanged(ref _keyList, value);
        //}

        //private ObservableCollectionWithSelectedItem<MouseWrapper> _mouseList = new ObservableCollectionWithSelectedItem<MouseWrapper>(MouseWrapper.GetList());
        //public ObservableCollectionWithSelectedItem<MouseWrapper> MouseList
        //{
        //    get => _mouseList;
        //    set => this.RaiseAndSetIfChanged(ref _mouseList, value);
        //}

        //private ObservableCollectionWithSelectedItem<DirectionWrapper> _dirList = new ObservableCollectionWithSelectedItem<DirectionWrapper>(DirectionWrapper.GetList());
        //public ObservableCollectionWithSelectedItem<DirectionWrapper> DirList
        //{
        //    get => _dirList;
        //    set => this.RaiseAndSetIfChanged(ref _dirList, value);
        //}
        

        private ObservableCollectionWithSelectedItem<TypeWrapper> _typeList = new ObservableCollectionWithSelectedItem<TypeWrapper>(TypeWrapper.GetList());
        public ObservableCollectionWithSelectedItem<TypeWrapper> TypeList
        {
            get => _typeList;
            set => this.RaiseAndSetIfChanged(ref _typeList, value);
        }

        private Visibility _keyVisibility;
        public Visibility KeyVisibility
        {
            get => _keyVisibility;
            set => this.RaiseAndSetIfChanged(ref _keyVisibility, value);
        }

        private Visibility _mouseVisibility;
        public Visibility MouseVisibility
        {
            get => _mouseVisibility;
            set => this.RaiseAndSetIfChanged(ref _mouseVisibility, value);
        }

        private Visibility _delayVisibility;
        public Visibility DelayVisibility
        {
            get => _delayVisibility;
            set => this.RaiseAndSetIfChanged(ref _delayVisibility, value);
        }

        public ReactiveCommand<Unit, Unit> AddCommandCmd { get; }


        public AllMacroViewModel()
        {
            AddCommandCmd = ReactiveCommand.Create(OnAddCommand);
            MacrosList    = new ObservableCollectionWithSelectedItem<Macros>();
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

            macros.AddCommand(new MouseCommand(MouseButton.Moving, MouseDir.None, 123, 234));
            macros.AddCommand(new DelayCommand(50));
            macros.AddCommand(new MouseCommand(MouseButton.Left, MouseDir.Down));
            macros.AddCommand(new DelayCommand(50));
            macros.AddCommand(new MouseCommand(MouseButton.Left, MouseDir.Up));

            MacrosList.Add(macros);
            MacrosList.SetSelectedToFirst();

            TypeList.SelectionChanged += TypeListOnSelectionChanged;
            KeyVisibility = Visibility.Visible;
            MouseVisibility = Visibility.Collapsed;
            DelayVisibility = Visibility.Collapsed;
        }
        
        private void TypeListOnSelectionChanged(ObservableCollectionWithSelectedItem<TypeWrapper> sender, TypeWrapper newselection, TypeWrapper oldselection)
        {
            if (newselection.Type == CommandType.Key)
            {
                KeyVisibility   = Visibility.Visible;
                MouseVisibility = Visibility.Collapsed;
                DelayVisibility = Visibility.Collapsed;
            }
            else if (newselection.Type == CommandType.Mouse)
            {
                KeyVisibility   = Visibility.Collapsed;
                MouseVisibility = Visibility.Visible;
                DelayVisibility = Visibility.Collapsed;
            }
            else if (newselection.Type == CommandType.Delay)
            {
                KeyVisibility   = Visibility.Collapsed;
                MouseVisibility = Visibility.Collapsed;
                DelayVisibility = Visibility.Visible;
            }
        }

        private void OnAddCommand()
        {
            if (MacrosList?.SelectedItem?.Commands == null)
                return;
            switch (TypeList.SelectedItem.Type)
            {
                case CommandType.Key:
                {
                    var cmd = new KeyCommand(KeyList.SelectedItem.Value, KeyStateList.SelectedItem.Value);
                    if (MacrosList.SelectedItem.Commands.Position == -1)
                        MacrosList.SelectedItem?.Commands.Add(cmd);
                    else
                        MacrosList.SelectedItem?.Commands.Insert(MacrosList.SelectedItem.Commands.Position + 1, cmd);
                }
                    break;
                case CommandType.Mouse:
                {
                    var cmd = new MouseCommand(MouseButtonList.SelectedItem.Value, MouseDirList.SelectedItem.Value);
                    if (MacrosList.SelectedItem.Commands.Position == -1)
                        MacrosList.SelectedItem?.Commands.Add(cmd);
                    else
                        MacrosList.SelectedItem?.Commands.Insert(MacrosList.SelectedItem.Commands.Position + 1, cmd);
                }
                    break;
                case CommandType.Delay:
                {
                    var cmd = new DelayCommand(Delay);
                    if (MacrosList.SelectedItem.Commands.Position == -1)
                        MacrosList.SelectedItem?.Commands.Add(cmd);
                    else
                        MacrosList.SelectedItem?.Commands.Insert(MacrosList.SelectedItem.Commands.Position + 1, cmd);
                }
                    break;
            }
        }
    }
}
