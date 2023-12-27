using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
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

        private DateTime _dt = DateTime.MinValue;
        private bool        _recordDelay;
        public bool RecordDelay
        {
            get => _recordDelay;
            set
            {
                if (_isRecord)
                {
                    this.RaisePropertyChanged();
                    return;
                }
                this.RaiseAndSetIfChanged(ref _recordDelay, value);
            }
        }

        private bool _isRecord;
        public bool IsRecord
        {
            get => _isRecord;
            set => this.RaiseAndSetIfChanged(ref _isRecord, value);
        }

        public ReactiveCommand<Unit, Unit> AddCommandCmd { get; }
        public ReactiveCommand<Unit, Unit> RecordCmd   { get; }
        public ReactiveCommand<Unit, Unit> StopRecordCmd          { get; }
        
        public AllMacroViewModel()
        {
            AddCommandCmd = ReactiveCommand.Create(OnAddCommand);
            RecordCmd   = ReactiveCommand.Create(OnRecord);
            StopRecordCmd = ReactiveCommand.Create(OnStopRecord);
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
            MacrosList.SelectionChanged += MacrosListOnSelectionChanged;
            MacrosList.CollectionChanged += MacrosListOnCollectionChanged;
            MacrosList.Add(macros);
            MacrosList.SetSelectedToFirst();

            TypeList.SelectionChanged += TypeListOnSelectionChanged;
            KeyVisibility = Visibility.Visible;
            MouseVisibility = Visibility.Collapsed;
            DelayVisibility = Visibility.Collapsed;
        }

        private void MacrosListOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //Save
        }

        private void MacrosListOnSelectionChanged(ObservableCollectionWithSelectedItem<Macros> sender, Macros newselection, Macros oldselection)
        {
            if (oldselection != null)
            {
                oldselection.Commands.SelectionChanged  -= CommandsOnSelectionChanged;
                oldselection.Commands.CollectionChanged -= CommandsOnCollectionChanged;
            }

            if (newselection == null)
                return;
            newselection.Commands.SelectionChanged += CommandsOnSelectionChanged;
            newselection.Commands.CollectionChanged += CommandsOnCollectionChanged;
        }

        private void CommandsOnSelectionChanged(ObservableCollectionWithSelectedItem<Command> sender, Command newselection, Command oldselection)
        {

        }

        private void CommandsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //Save
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

        public void OnDeleteCommand()
        {
            if (MacrosList?.SelectedItem?.Commands?.SelectedItem == null)
                return;
            var ind = MacrosList.SelectedItem.Commands.Position;
            MacrosList.SelectedItem.Commands.Remove(MacrosList.SelectedItem.Commands.SelectedItem);
            MacrosList.SelectedItem.Commands.SetSelectedToPosition(ind);
        }

        private void OnRecord()
        {
            IsRecord                         =  true;
            //GlobalDriver._driver.OnKeyPressed += DriverOnOnKeyPressed;
        }

        private void DriverOnOnKeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (RecordDelay)
            {
                if(_dt == DateTime.MinValue)
                    _dt = DateTime.Now;
                else
                {
                    var cmdd = new DelayCommand((int)(DateTime.Now - _dt).TotalMilliseconds);
                    
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (MacrosList.SelectedItem.Commands.Position == -1)
                            MacrosList.SelectedItem?.Commands.Add(cmdd);
                        else
                            MacrosList.SelectedItem?.Commands.Insert(MacrosList.SelectedItem.Commands.Position + 1, cmdd);
                    });
                    _dt = DateTime.Now;
                }
            }

            var cmd = new KeyCommand(e.Key, e.State);
            Application.Current.Dispatcher.Invoke(() =>
                                                  {
                                                      if (MacrosList.SelectedItem.Commands.Position == -1)
                                                          MacrosList.SelectedItem?.Commands.Add(cmd);
                                                      else
                                                          MacrosList.SelectedItem?.Commands.Insert(MacrosList.SelectedItem.Commands.Position + 1, cmd);
                                                  });
        }

        private void OnStopRecord()
        {
            //GlobalDriver._driver.OnKeyPressed -= DriverOnOnKeyPressed;
            IsRecord                         =  false;
        }

    }
}
