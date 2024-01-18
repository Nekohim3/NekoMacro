using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DynamicData;
using Interceptor;
using NekoMacro.MacrosBase;
using NekoMacro.MacrosBase.NewCmd;
using NekoMacro.UI;
using NekoMacro.Utils;
using NekoMacro.Utils.TreeDataGrid;
using Newtonsoft.Json;
using ReactiveUI;
using Keys = Interceptor.Keys;

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

        private ObservableCollectionWithMultiSelectedItem<BaseCmd> _commandList;
        public ObservableCollectionWithMultiSelectedItem<BaseCmd> CommandList
        {
            get => _commandList;
            set => this.RaiseAndSetIfChanged(ref _commandList, value);
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

        private string _macrosName;
        public string MacrosName
        {
            get => _macrosName;
            set => this.RaiseAndSetIfChanged(ref _macrosName, value);
        }

        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set => this.RaiseAndSetIfChanged(ref _isEdit, value);
        }

        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set => this.RaiseAndSetIfChanged(ref _isAdd, value);
        }

        private bool _isRecord;
        public bool IsRecord
        {
            get => _isRecord;
            set => this.RaiseAndSetIfChanged(ref _isRecord, value);
        }

        private bool _repeatVisible;
        public bool RepeatVisible
        {
            get => _repeatVisible;
            set => this.RaiseAndSetIfChanged(ref _repeatVisible, value);
        }

        private bool _repeatSetVisible;
        public bool RepeatSetVisible
        {
            get => _repeatSetVisible;
            set => this.RaiseAndSetIfChanged(ref _repeatSetVisible, value);
        }

        private bool _repeatEscapeVisible;
        public bool RepeatEscapeVisible
        {
            get => _repeatEscapeVisible;
            set => this.RaiseAndSetIfChanged(ref _repeatEscapeVisible, value);
        }

        private bool _repeatEditVisible;
        public bool RepeatEditVisible
        {
            get => _repeatEditVisible;
            set => this.RaiseAndSetIfChanged(ref _repeatEditVisible, value);
        }

        private int _repeatCount;
        public int RepeatCount
        {
            get => _repeatCount;
            set => this.RaiseAndSetIfChanged(ref _repeatCount, value);
        }

        private bool _insertBeforeSelected;
        public bool InsertBeforeSelected
        {
            get => _insertBeforeSelected;
            set => this.RaiseAndSetIfChanged(ref _insertBeforeSelected, value);
        }

        private bool _insertAfterSelected;
        public bool InsertAfterSelected
        {
            get => _insertAfterSelected;
            set => this.RaiseAndSetIfChanged(ref _insertAfterSelected, value);
        }

        private bool _recordCoordOnClick;
        public bool RecordCoordOnClick
        {
            get => _recordCoordOnClick;
            set => this.RaiseAndSetIfChanged(ref _recordCoordOnClick, value);
        }
        
        public ReactiveCommand<Unit, Unit> AddMacrosCmd                  { get; }
        public ReactiveCommand<Unit, Unit> EditMacrosCmd                 { get; }
        public ReactiveCommand<Unit, Unit> SaveMacrosCmd                 { get; }
        public ReactiveCommand<Unit, Unit> CancelMacrosCmd               { get; }
        public ReactiveCommand<Unit, Unit> DeleteMacrosCmd               { get; }
        public ReactiveCommand<Unit, Unit> ImportCmd                     { get; }
        public ReactiveCommand<Unit, Unit> ExportCmd                     { get; }
        public ReactiveCommand<Unit, Unit> SetClickDelayForSelectedCmd   { get; }
        public ReactiveCommand<Unit, Unit> SetBetweenDelayForSelectedCmd { get; }
        public ReactiveCommand<Unit, Unit> MaxRepeatCmd                  { get; }
        public ReactiveCommand<Unit, Unit> SetRepeatCmd                  { get; }
        public ReactiveCommand<Unit, Unit> SetRepeatForSelectedCmd       { get; }
        public ReactiveCommand<Unit, Unit> EscapeRepeatCmd                          { get; }



        public MacroEditViewModel()
        {
            AddMacrosCmd                  = ReactiveCommand.Create(OnAddMacros);
            EditMacrosCmd                 = ReactiveCommand.Create(OnEditMacros);
            CancelMacrosCmd               = ReactiveCommand.Create(OnCancelMacros);
            SaveMacrosCmd                 = ReactiveCommand.Create(OnSaveMacros);
            DeleteMacrosCmd               = ReactiveCommand.Create(OnDeleteMacros);
            ExportCmd                     = ReactiveCommand.Create(OnExport);
            ImportCmd                     = ReactiveCommand.Create(OnImport);
            SetClickDelayForSelectedCmd   = ReactiveCommand.Create(OnSetClickDelayForSelected);
            SetBetweenDelayForSelectedCmd = ReactiveCommand.Create(OnSetBetweenDelayForSelected);
            SetRepeatCmd                  = ReactiveCommand.Create(OnSetRepeat);
            MaxRepeatCmd                  = ReactiveCommand.Create(OnMaxRepeat);
            SetRepeatForSelectedCmd       = ReactiveCommand.Create(OnSetRepeatForSelected);
            EscapeRepeatCmd               = ReactiveCommand.Create(OnEscapeRepeat);

            g.GHook.KeyDown               += GHookOnKeyDown;
            g.AHook.KeyDown               += AHookOnKeyDown;

            GlobalDriver.KeyPressSubscribe(KeyPressHandler);
            GlobalDriver.MousePressSubscribe(MousePressHandler);
            Load();
        }

        private void OnEscapeRepeat()
        {
            //var parent      = MacrosList.SelectedItem.Commands.FlatModel.SelectedItems.First().Parent;
            //var childs      = MacrosList.SelectedItem.Commands.FlatModel.SelectedItems.First().Childs;
            //var sel         = MacrosList.SelectedItem.Commands.FlatModel.SelectedItems.First();
            //var indNoParent = MacrosList.SelectedItem.Commands.IndexOf(MacrosList.SelectedItem.Commands.FlatModel.SelectedItems.First());
            //var indParent   = 0;
            //if (parent != null)
            //{
            //    indParent = parent.Childs.IndexOf(sel);
            //    if (indParent < 0)
            //        indParent = 0;
            //}



            //if (parent == null)
            //{
            //    MacrosList.SelectedItem.Commands.Remove(sel);
            //}
            //else
            //{
            //    parent.Childs.Remove(sel);
            //}

            //foreach (var x in childs)
            //{
            //    x.Parent = x.Parent.Parent;
            //    x.UpdateLevel();
            //}

            //if (!sel.IsExpanded)
            //{
            //    if (parent == null)
            //    {
            //        foreach (var x in childs)
            //        {
            //            MacrosList.SelectedItem.Commands.Insert(indNoParent++, x);
            //        }
            //    }
            //    else
            //    {
            //        foreach (var x in childs)
            //        {
            //            parent.Childs.Insert(indParent++, x);
            //        }
            //    }
            //}
        }

        private void OnSetRepeatForSelected()
        {
            //if (MacrosList.SelectedItem.Commands.FlatModel.IndexOf(MacrosList.SelectedItem.Commands.FlatModel.SelectedItems.Last()) -
            //    MacrosList.SelectedItem.Commands.FlatModel.IndexOf(MacrosList.SelectedItem.Commands.FlatModel.SelectedItems.First()) + 1
            //    !=
            //    MacrosList.SelectedItem.Commands.FlatModel.SelectedItems.Count)
            //    return;

            //var parent      = MacrosList.SelectedItem.Commands.FlatModel.SelectedItems.First().Parent;
            //if (MacrosList.SelectedItem.Commands.FlatModel.SelectedItems.Any(_ => _.Parent != parent))
            //    return;

            //var indNoParent = MacrosList.SelectedItem.Commands.IndexOf(MacrosList.SelectedItem.Commands.FlatModel.SelectedItems.First());
            //var indParent = 0;
            //var sel         = MacrosList.SelectedItem.Commands.FlatModel.SelectedItems.ToList();
            //if (parent != null)
            //{
            //    indParent = parent.Childs.IndexOf(sel.First());
            //    if (indParent < 0)
            //        indParent = 0;
            //}

            //if (parent == null)
            //    foreach (var x in MacrosList.SelectedItem.Commands.FlatModel.SelectedItems.ToList())
            //        MacrosList.SelectedItem.Commands.Remove(x);
            //else
            //    foreach (var x in MacrosList.SelectedItem.Commands.FlatModel.SelectedItems.ToList())
            //        parent.Childs.Remove(x);
            
            //var cmd = new RepeatCmd(100, 1) { IsExpanded = true };
            //foreach (var x in sel)
            //    cmd.Childs.Add(x); 
            //if (parent == null)
            //    MacrosList.SelectedItem.Commands.Insert(indNoParent, cmd);
            //else
            //    parent.Childs.Insert(indParent, cmd);
            //RefreshTree();
        }

        public void RefreshTree()
        {
            //var lst = MacrosList.SelectedItem.Commands.ToList();
            //MacrosList.SelectedItem.Commands.Clear();
            //foreach (var x in lst)
            //{
            //    MacrosList.SelectedItem.Commands.Add(x);
            //}

        }

        private void OnSetRepeat()
        {

        }

        private void OnMaxRepeat()
        {

        }

        private void AHookOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Insert)
            {
                if (IsRecord)
                    OnStopRecord();
                else
                    OnStartRecord();
            }

            if (IsRecord)
                return;

            if (e.KeyCode == System.Windows.Forms.Keys.Delete)
            {
                OnDeleteCommand();
            }
        }

        private void GHookOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Insert && e.Shift)
            {
                Process.GetCurrentProcess().Kill();
            }

            if (IsRecord)
                return;

            //if (e.KeyCode == System.Windows.Forms.Keys.Home)
            //{
            //    OnStartMacro();
            //}

            //if (e.KeyCode == System.Windows.Forms.Keys.End)
            //{
            //    OnStopMacro();
            //}

        }

        private void OnStartRecord()
        {
            if (IsRecord)
                return;
            IsRecord = true;
            g.BlockWindow("Recording...");
        }

        private void OnStopRecord()
        {
            if (!IsRecord)
                return;
            IsRecord = false;
            g.UnblockWindow();
        }

        private void OnDeleteCommand()
        {

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
            if (MacrosList.SelectedItem == null)
                return;
            IsEdit     = true;
            IsAdd      = true;
            MacrosName = "";
        }

        private void OnEditMacros()
        {
            if (MacrosList.SelectedItem == null)
                return;
            IsEdit     = true;
            IsAdd      = false;
            MacrosName = MacrosList.SelectedItem.Name;
        }

        private void OnCancelMacros()
        {
            MacrosName = "";
            IsEdit     = false;
        }

        private void OnSaveMacros()
        {
            IsEdit     = false;
            if (IsAdd)
                MacrosList.Add(new Macros(MacrosName));
            else
                MacrosList.SelectedItem.Name = MacrosName;
            MacrosName = "";
            Save();
        }

        private void OnDeleteMacros()
        {
            if (MacrosList.SelectedItem == null)
                return;
            if (g.MsgShow($"Really delete macros {MacrosList.SelectedItem.Name}?", "Alert", NMsgButtons.YesNo) == NMsgReply.No)
                return;
            var selPos = MacrosList.Position;
            MacrosList.Remove(MacrosList.SelectedItem);
            MacrosList.SetSelectedToPosition(selPos);
            Save();
        }

        private void OnImport()
        {

        }

        private void OnExport()
        {
            //selected
        }

        private void OnSetBetweenDelayForSelected()
        {

        }
        
        private void OnSetClickDelayForSelected()
        {

        }

        private void Save()
        {
            File.WriteAllText("MacrosDict.cfg", JsonConvert.SerializeObject(MacrosList, Formatting.Indented, new JsonSerializerSettings()
                                                                                                             {
                                                                                                                 TypeNameHandling           = TypeNameHandling.Auto,
                                                                                                                 ReferenceLoopHandling      = ReferenceLoopHandling.Ignore,
                                                                                                                 PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                                                                                                                 NullValueHandling          = NullValueHandling.Ignore,
                                                                                                                 MaxDepth                   = 1024,

                                                                                                             }));
        }

        private void Load()
        {
            if (File.Exists("MacrosDict.cfg"))
                MacrosList = JsonConvert.DeserializeObject<ObservableCollectionWithSelectedItem<Macros>>(File.ReadAllText("MacrosDict.cfg"), new JsonSerializerSettings()
                                                                                                                                             {
                                                                                                                                                 TypeNameHandling           = TypeNameHandling.Auto,
                                                                                                                                                 ReferenceLoopHandling      = ReferenceLoopHandling.Ignore,
                                                                                                                                                 PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                                                                                                                                                 NullValueHandling          = NullValueHandling.Ignore,
                                                                                                                                                 MaxDepth                   = 1024,

                                                                                                                                             });
            else
            {
                //MacrosList = new ObservableCollectionWithSelectedItem<Macros>();
                //var macros = new Macros("Test1");
                //macros.AddCommand(new KeyCmd(Keys.Q, 100, 50));

                //macros.AddCommand(new KeyCmd(Keys.W, 100, 50, ctrl: true));
                //macros.AddCommand(new KeyCmd(Keys.E, 100, 50, alt: true));
                //macros.AddCommand(new KeyCmd(Keys.R, 100, 50, shift: true));
                //macros.AddCommand(new KeyCmd(Keys.T, 100, 50, ctrl: true, alt: true));
                //macros.AddCommand(new KeyCmd(Keys.Y, 100, 50, ctrl: true, shift: true));
                //macros.AddCommand(new KeyCmd(Keys.U, 100, 50, alt: true, shift: true));
                //macros.AddCommand(new KeyCmd(Keys.I, 100, 50, ctrl: true, alt: true, shift: true));
                //var rcmd = new RepeatCmd(100, 50);
                //rcmd.Childs.Add(new MouseCmd(MouseKey.Left, 100, 50));
                //rcmd.Childs.Add(new MouseCmd(MouseKey.Left, 100, 50));
                //rcmd.Childs.Add(new KeyCmd(Keys.W, 100, 50, ctrl: true));
                //rcmd.Childs.Add(new KeyCmd(Keys.E, 100, 50, alt: true));
                //rcmd.Childs.Add(new KeyCmd(Keys.R, 100, 50, shift: true));
                //rcmd.Childs.Add(new KeyCmd(Keys.T, 100, 50, ctrl: true, alt: true));
                //rcmd.Childs.Add(new KeyCmd(Keys.Y, 100, 50, ctrl: true, shift: true));
                //rcmd.Childs.Add(new KeyCmd(Keys.U, 100, 50, alt: true, shift: true));
                //rcmd.Childs.Add(new KeyCmd(Keys.I, 100, 50, ctrl: true, alt: true, shift: true));
                //macros.AddCommand(rcmd);
                //macros.AddCommand(new MouseCmd(MouseKey.Left, 100, 50));
                //macros.AddCommand(new MouseCmd(MouseKey.Right, 100, 50));
                //macros.AddCommand(new MouseCmd(MouseKey.Middle, 100, 50));
                //macros.AddCommand(new MouseCmd(MouseKey.X1, 100, 50));
                //macros.AddCommand(new MouseCmd(MouseKey.X2, 100, 50));
                //macros.AddCommand(new MouseCmd(MouseKey.Left, 100, 50, ctrl: true));
                //macros.AddCommand(new MouseCmd(MouseKey.Left, 100, 50, alt: true));
                //macros.AddCommand(new MouseCmd(MouseKey.Left, 100, 50, shift: true));
                //macros.AddCommand(new MouseCmd(MouseKey.Left, 100, 50, ctrl: true, alt: true));
                //macros.AddCommand(new MouseCmd(MouseKey.Left, 100, 50, ctrl: true, shift: true));
                //macros.AddCommand(new MouseCmd(MouseKey.Left, 100, 50, alt: true, shift: true));
                //macros.AddCommand(new MouseCmd(MouseKey.Left, 100, 50, ctrl: true, alt: true, shift: true));
                //macros.AddCommand(new MouseCmd(MouseKey.Moving, new GetMousePos.POINT(100, 150), 300));
                //macros.AddCommand(new MouseCmd(MouseKey.Moving, new GetMousePos.POINT(1000, 1500), 300));
                //macros.AddCommand(new MouseCmd(MouseKey.Moving, new GetMousePos.POINT(1500, 150), 300));

                //MacrosList.Add(macros);
                //Save();
            }
            MacrosList.SelectionChanged += MacrosListOnSelectionChanged;
        }

        private void MacrosListOnSelectionChanged(ObservableCollectionWithSelectedItem<Macros> sender, Macros newselection, Macros oldselection)
        {
            if (newselection == null)
                return;
            CommandList = new ObservableCollectionWithMultiSelectedItem<BaseCmd>();
            CommandList.AddRange(newselection.Commands);
        }

        private void ChangedIsExpanded(bool isExpanded, BaseCmd r)
        {
            if (CommandList.SelectedItems == null || CommandList.SelectedItems.Count != 1)
                return;
            var sel = CommandList.SelectedItems.First();
            var ind = CommandList.IndexOf(sel);
            sel.IsExpanded = isExpanded;
            if (isExpanded) // развернуть 
            {
                foreach (var item in sel.Childs)
                {
                    ind++;
                    CommandList.Insert(ind, item);
                    foreach (var x in item.Childs)
                    {
                        x.IsExpanded = false;
                    }
                }
            }
            else // свернуть 
            {
                sel.Shrink();
                var level = sel.LevelNumber;
                ind++;
                while (ind < ListRepairItem.Count && ListRepairItem[ind].LevelNumber > level)
                    ListRepairItem.RemoveAt(ind);
            }
        }
    }
}
