//using Interceptor;
//using Newtonsoft.Json;
//using ReactiveUI;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace NekoMacro.MacrosBase
//{
//    //public static class MouseStateExtension
//    //{
//    //    public static MouseButton GetButtonFromState(this MouseState state)
//    //    {
//    //        if (state == MouseState.LeftDown || state == MouseState.LeftUp)
//    //            return MouseButton.Left;
//    //        if (state == MouseState.RightDown || state == MouseState.RightUp)
//    //            return MouseButton.Right;
//    //        if (state == MouseState.MiddleDown || state == MouseState.MiddleUp)
//    //            return MouseButton.Middle;
//    //        if (state == MouseState.LeftExtraDown || state == MouseState.LeftExtraUp)
//    //            return MouseButton.LeftExtra;
//    //        if (state == MouseState.RightExtraDown || state == MouseState.RightExtraUp)
//    //            return MouseButton.RightExtra;
//    //        if (state == MouseState.ScrollDown || state == MouseState.ScrollUp || state == MouseState.ScrollVertical ||
//    //            state == MouseState.ScrollLeft || state == MouseState.ScrollRight ||
//    //            state == MouseState.ScrollHorizontal)
//    //            return MouseButton.Scroll;
//    //        if (state == MouseState.Moving)
//    //            return MouseButton.Moving;
//    //        return MouseButton.Left;
//    //    }

//    //    public static MouseDir GetDirectionFromState(this MouseState state)
//    //    {
//    //        if (state.ToString().ToLower().Contains("down"))
//    //            return MouseDir.Down;

//    //        if (state.ToString().ToLower().Contains("up"))
//    //            return MouseDir.Up;
//    //        return MouseDir.None;
//    //    }
//    //}
//    //[JsonObject]
//    //public class MouseCommand : Command
//    //{
//    //    private MouseButton _button;
//    //    public MouseButton Button
//    //    {
//    //        get => _button;
//    //        set => this.RaiseAndSetIfChanged(ref _button, value);
//    //    }

//    //    private MouseDir _dir;
//    //    public MouseDir Dir
//    //    {
//    //        get => _dir;
//    //        set => this.RaiseAndSetIfChanged(ref _dir, value);
//    //    }

//    //    private int _x;
//    //    public int X
//    //    {
//    //        get => _x;
//    //        set => this.RaiseAndSetIfChanged(ref _x, value);
//    //    }

//    //    private int _y;
//    //    public int Y
//    //    {
//    //        get => _y;
//    //        set => this.RaiseAndSetIfChanged(ref _y, value);
//    //    }

//    //    private bool _abs;
//    //    public bool Abs
//    //    {
//    //        get => _abs;
//    //        set => this.RaiseAndSetIfChanged(ref _abs, value);
//    //    }

//    //    public override CommandType Type => CommandType.Mouse;

//    //    public override string TypeE => Type.ToString();
//    //    public override string KeyE  => Button == MouseButton.Moving ? $"{Button} ({X}:{Y})" : Button.ToString();
//    //    public override string DirE  => Dir == MouseDir.None ? "" : Dir.ToString();
//    //    public override string XE    => X == int.MinValue ? "" : X.ToString();
//    //    public override string YE    => Y == int.MinValue ? "" : Y.ToString();
//    //    public override string AbsE  => Abs.ToString();
//    //    public MouseCommand()
//    //    {
            
//    //    }

//    //    public MouseCommand(MouseButton button, MouseDir dir, int x = int.MinValue, int y = int.MinValue, bool abs = true)
//    //    {
//    //        _button = button;
//    //        _dir    = dir;
//    //        _x      = x;
//    //        _y      = y;
//    //        _abs    = abs;
//    //    }

//    //    public override void Execute()
//    //    {

//    //    }

//    //    public override string ToString()
//    //    {
//    //        return $"[M]: {Button}{Dir} | {X}:{Y} | {Abs}";
//    //    }
//    //}
//}
