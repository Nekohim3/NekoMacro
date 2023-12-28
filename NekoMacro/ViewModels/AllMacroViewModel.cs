using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Interceptor;
using NekoMacro.MacrosBase;
using NekoMacro.Utils;
using Newtonsoft.Json;
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

        private ObservableCollection<Keys> _keyList = new ObservableCollection<Keys>(EnumWrapper<Keys>.GetListBase());
        public ObservableCollection<Keys> KeyList
        {
            get => _keyList;
            set => this.RaiseAndSetIfChanged(ref _keyList, value);
        }

        private Keys? _selectedKey;
        public Keys? SelectedKey
        {
            get => _selectedKey;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedKey, value);
                if (value == null)
                    return;
                ((KeyCommand)CurrentCommand).Key = value.Value;
            }
        }

        private ObservableCollection<KeyState> _keyStateList = new ObservableCollection<KeyState>(EnumWrapper<KeyState>.GetListBase());
        public ObservableCollection<KeyState> KeyStateList
        {
            get => _keyStateList;
            set => this.RaiseAndSetIfChanged(ref _keyStateList, value);
        }

        private KeyState? _selectedKeyState;
        public KeyState? SelectedKeyState
        {
            get => _selectedKeyState;
            set { this.RaiseAndSetIfChanged(ref _selectedKeyState, value);
                if (value == null)
                    return;
                ((KeyCommand)CurrentCommand).State = value.Value;
            }
        }

        private ObservableCollection<MouseButton> _mouseButtonList = new ObservableCollection<MouseButton>(EnumWrapper<MouseButton>.GetListBase());
        public ObservableCollection<MouseButton> MouseButtonList
        {
            get => _mouseButtonList;
            set => this.RaiseAndSetIfChanged(ref _mouseButtonList, value);
        }

        private MouseButton? _selectedMouseButton;
        public MouseButton? SelectedMouseButton
        {
            get => _selectedMouseButton;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedMouseButton, value);

                if (value == null)
                    return;
                ((MouseCommand)CurrentCommand).Button = value.Value;
            }
        }

        private ObservableCollection<MouseDir> _mouseDirList = new ObservableCollection<MouseDir>(EnumWrapper<MouseDir>.GetListBase());
        public ObservableCollection<MouseDir> MouseDirList
        {
            get => _mouseDirList;
            set => this.RaiseAndSetIfChanged(ref _mouseDirList, value);
        }

        private MouseDir? _selectedMouseDir;
        public MouseDir? SelectedMouseDir
        {
            get => _selectedMouseDir;
            set { this.RaiseAndSetIfChanged(ref _selectedMouseDir, value);

                if (value == null)
                    return;
                ((MouseCommand)CurrentCommand).Dir = value.Value;
            }
        }

        private int _delay;
        public int Delay
        {
            get => _delay;
            set
            {
                this.RaiseAndSetIfChanged(ref _delay, value);
                ((DelayCommand)CurrentCommand).Delay = value;
            }
        }

        private ObservableCollection<CommandType> _typeList = new ObservableCollection<CommandType>(EnumWrapper<CommandType>.GetListBase());
        public ObservableCollection<CommandType> TypeList
        {
            get => _typeList;
            set => this.RaiseAndSetIfChanged(ref _typeList, value);
        }

        private CommandType? _selectedType;
        public CommandType? SelectedType
        {
            get => _selectedType;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedType, value);
                switch (_selectedType)
                {
                    case null:
                        return;
                    case CommandType.Key:
                        KeyVisibility   = Visibility.Visible;
                        MouseVisibility = Visibility.Collapsed;
                        DelayVisibility = Visibility.Collapsed;
                        CurrentCommand  = new KeyCommand(SelectedKey.GetValueOrDefault(), SelectedKeyState.GetValueOrDefault());
                        break;
                    case CommandType.Mouse:
                        KeyVisibility   = Visibility.Collapsed;
                        MouseVisibility = Visibility.Visible;
                        DelayVisibility = Visibility.Collapsed;
                        CurrentCommand  = new MouseCommand(SelectedMouseButton.GetValueOrDefault(), SelectedMouseDir.GetValueOrDefault());
                        break;
                    case CommandType.Delay:
                        KeyVisibility   = Visibility.Collapsed;
                        MouseVisibility = Visibility.Collapsed;
                        DelayVisibility = Visibility.Visible;
                        CurrentCommand  = new DelayCommand(Delay);
                        break;
                }
            }
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

        private Command _currentCommand;
        public Command CurrentCommand
        {
            get => _currentCommand;
            set => this.RaiseAndSetIfChanged(ref _currentCommand, value);
        }

        private bool _isRecord;
        public bool IsRecord
        {
            get => _isRecord;
            set => this.RaiseAndSetIfChanged(ref _isRecord, value);
        }

        public ReactiveCommand<Unit, Unit> AddCommandCmd  { get; }
        public ReactiveCommand<Unit, Unit> RecordCmd      { get; }
        public ReactiveCommand<Unit, Unit> StopRecordCmd  { get; }
        public ReactiveCommand<Unit, Unit> SaveCommandCmd { get; }
        public ReactiveCommand<Unit, Unit> UpCmd          { get; }
        public ReactiveCommand<Unit, Unit> DownCmd           { get; }




        public AllMacroViewModel()
        {
            AddCommandCmd  = ReactiveCommand.Create(OnAddCommand);
            RecordCmd      = ReactiveCommand.Create(OnRecord);
            StopRecordCmd  = ReactiveCommand.Create(OnStopRecord);
            SaveCommandCmd = ReactiveCommand.Create(OnSaveCommand);
            UpCmd          = ReactiveCommand.Create(OnUp);
            DownCmd        = ReactiveCommand.Create(OnDown);
            //MacrosList     = new ObservableCollectionWithSelectedItem<Macros>();
            //var macros = new Macros("Test macros");
            //macros.AddCommand(new KeyCommand(Keys.A, KeyState.Down));
            //macros.AddCommand(new DelayCommand(50));
            //macros.AddCommand(new KeyCommand(Keys.A, KeyState.Up));
            //macros.AddCommand(new DelayCommand(150));
            //macros.AddCommand(new KeyCommand(Keys.B, KeyState.Down));
            //macros.AddCommand(new DelayCommand(50));
            //macros.AddCommand(new KeyCommand(Keys.B, KeyState.Up));
            //macros.AddCommand(new DelayCommand(150));
            //macros.AddCommand(new KeyCommand(Keys.Q, KeyState.Down));
            //macros.AddCommand(new DelayCommand(50));
            //macros.AddCommand(new KeyCommand(Keys.Q, KeyState.Up));
            //macros.AddCommand(new DelayCommand(150));

            //macros.AddCommand(new MouseCommand(MouseButton.Moving, MouseDir.None, 123, 234));
            //macros.AddCommand(new DelayCommand(50));
            //macros.AddCommand(new MouseCommand(MouseButton.Left, MouseDir.Down));
            //macros.AddCommand(new DelayCommand(50));
            //macros.AddCommand(new MouseCommand(MouseButton.Left, MouseDir.Up));
            Load();
            //MacrosList.Add(macros);

            //TypeList.SelectionChanged += TypeListOnSelectionChanged;
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
            if (newselection == null)
                return;
            if (newselection is KeyCommand kcmd)
            {
                CurrentCommand = new KeyCommand(kcmd.Key, kcmd.State);
                SelectedType   = CommandType.Key;
                SelectedKey = kcmd.Key;
                SelectedKeyState = kcmd.State;
            }
            else if (newselection is MouseCommand mcmd)
            {
                CurrentCommand = new MouseCommand(mcmd.Button, mcmd.Dir, mcmd.X, mcmd.Y, mcmd.Abs);
                SelectedType   = CommandType.Mouse;
                SelectedMouseButton = mcmd.Button;
                SelectedMouseDir = mcmd.Dir;
            }
            else if (newselection is DelayCommand dcmd)
            {
                CurrentCommand = new DelayCommand(dcmd.Delay);
                SelectedType   = CommandType.Delay;
                Delay = dcmd.Delay;
            }
        }

        private void CommandsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //Save
        }

        private void OnAddCommand()
        {
            if (MacrosList?.SelectedItem?.Commands == null)
                return;
            if(MacrosList.SelectedItem.Commands.Position >= 0)
                MacrosList.SelectedItem.Commands.Insert(MacrosList.SelectedItem.Commands.Position + 1, CurrentCommand);
            else
            {
                MacrosList.SelectedItem.Commands.Add(CurrentCommand);
                MacrosList.SelectedItem.Commands.SetSelectedToFirst();
            }
            Save();
        }

        private void OnSaveCommand()
        {
            if (MacrosList?.SelectedItem?.Commands?.SelectedItem == null)
                return;
            
            MacrosList.SelectedItem.Commands[MacrosList.SelectedItem.Commands.Position] = CurrentCommand;
            MacrosList.SelectedItem.Commands.SetSelectedTo(CurrentCommand);
            Save();
        }

        public void OnDeleteCommand()
        {
            if (MacrosList?.SelectedItem?.Commands?.SelectedItem == null)
                return;
            var ind = MacrosList.SelectedItem.Commands.Position;
            MacrosList.SelectedItem.Commands.Remove(MacrosList.SelectedItem.Commands.SelectedItem);
            MacrosList.SelectedItem.Commands.SetSelectedToPosition(ind);
            Save();
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

        private void OnDown()
        {
            if (MacrosList?.SelectedItem?.Commands?.SelectedItem == null)
                return;
            if (MacrosList.SelectedItem.Commands.Position == MacrosList.SelectedItem.Commands.Count - 1)
                return;
            MacrosList.SelectedItem.Commands.Move(MacrosList.SelectedItem.Commands.Position, MacrosList.SelectedItem.Commands.Position + 1);
            Save();
        }

        private void OnUp()
        {
            if (MacrosList?.SelectedItem?.Commands?.SelectedItem == null)
                return;
            if (MacrosList.SelectedItem.Commands.Position == 0)
                return;
            MacrosList.SelectedItem.Commands.Move(MacrosList.SelectedItem.Commands.Position, MacrosList.SelectedItem.Commands.Position - 1);
            Save();
        }

        private void OnStopRecord()
        {
            //GlobalDriver._driver.OnKeyPressed -= DriverOnOnKeyPressed;
            IsRecord                         =  false;
            Save();
        }

        private void Save()
        {
            File.WriteAllText("MacrosDict.cfg", JsonConvert.SerializeObject(MacrosList, Formatting.Indented, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto }));
        }

        private void Load()
        {
            if (File.Exists("MacrosDict.cfg"))
                MacrosList = JsonConvert.DeserializeObject<ObservableCollectionWithSelectedItem<Macros>>(File.ReadAllText("MacrosDict.cfg"), new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto });
            else
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

                macros.AddCommand(new MouseCommand(MouseButton.Moving, MouseDir.None, 123, 234));
                macros.AddCommand(new DelayCommand(50));
                macros.AddCommand(new MouseCommand(MouseButton.Left, MouseDir.Down));
                macros.AddCommand(new DelayCommand(50));
                macros.AddCommand(new MouseCommand(MouseButton.Left, MouseDir.Up));
                MacrosList.Add(macros);
            }

            MacrosList.SelectionChanged  += MacrosListOnSelectionChanged;
            MacrosList.CollectionChanged += MacrosListOnCollectionChanged;
            MacrosList.SetSelectedToFirst();
            MacrosList.SelectedItem?.Commands?.SetSelectedToFirst();
        }

    }
}
