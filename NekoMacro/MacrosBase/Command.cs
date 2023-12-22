using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interceptor;
using NekoMacro.Utils;
using Newtonsoft.Json;
using ReactiveUI;

namespace NekoMacro.MacrosBase
{
    public enum CommandStatus
    {
        Queue,
        Execute,
        Executed
    }

    public enum CommandType
    {
        None,
        Key,
        Mouse,
        Delay
    }

    [JsonObject]
    public abstract class Command : ReactiveObject
    {
        private CommandStatus _status;
        public CommandStatus Status
        {
            get => _status;
            set => this.RaiseAndSetIfChanged(ref _status, value);
        }

        public abstract CommandType Type { get; }

        public abstract string TypeE  { get; }
        public abstract string KeyE   { get; }
        public abstract string StateE { get; }
        public virtual  string XE     { get; }
        public virtual  string YE     { get; }
        public virtual  string AbsE   { get; }

        public abstract void Execute();

        private ObservableCollectionWithSelectedItem<CommandType> _typeList;
        public ObservableCollectionWithSelectedItem<CommandType> TypeList
        {
            get => _typeList;
            set => this.RaiseAndSetIfChanged(ref _typeList, value);
        }
    }
}
