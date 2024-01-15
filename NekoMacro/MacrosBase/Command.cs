//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Interceptor;
//using NekoMacro.Utils;
//using Newtonsoft.Json;
//using ReactiveUI;

//namespace NekoMacro.MacrosBase
//{
    

//    [JsonObject]
//    public abstract class Command : ReactiveObject
//    {
//        private CommandStatus _status;
//        [JsonIgnore]
//        public CommandStatus Status
//        {
//            get => _status;
//            set => this.RaiseAndSetIfChanged(ref _status, value);
//        }

//        [JsonIgnore]
//        public abstract CommandType Type { get; }

//        [JsonIgnore]
//        public abstract string TypeE  { get; }
//        [JsonIgnore]
//        public abstract string KeyE   { get; }
//        [JsonIgnore]
//        public abstract string DirE { get; }
//        [JsonIgnore]
//        public virtual  string XE     { get; }
//        [JsonIgnore]
//        public virtual  string YE     { get; }
//        [JsonIgnore]
//        public virtual  string AbsE   { get; }

//        public abstract void Execute();

//        //private ObservableCollectionWithSelectedItem<CommandType> _typeList;
//        //public ObservableCollectionWithSelectedItem<CommandType> TypeList
//        //{
//        //    get => _typeList;
//        //    set => this.RaiseAndSetIfChanged(ref _typeList, value);
//        //}
//    }
//}
