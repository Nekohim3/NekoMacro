﻿//using ReactiveUI;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Newtonsoft.Json;

//namespace NekoMacro.MacrosBase
//{
//    [JsonObject]
//    public class DelayCommand : Command
//    {
//        private int _delay;
//        public int Delay
//        {
//            get => _delay;
//            set => this.RaiseAndSetIfChanged(ref _delay, value);
//        }

//        public override CommandType Type   => CommandType.Delay;
//        public override string      TypeE  => Type.ToString();
//        public override string      KeyE   => Delay.ToString();
//        public override string      DirE => "";

//        public DelayCommand()
//        {
            
//        }

//        public DelayCommand(int delay)
//        {
//            _delay = delay;
//        }

//        public override void Execute()
//        {

//        }

//        public override string ToString()
//        {
//            return $"[D]: {Delay}";
//        }
//    }
//}
