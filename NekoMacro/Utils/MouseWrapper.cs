using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interceptor;
using ReactiveUI;
using static System.Net.Mime.MediaTypeNames;

namespace NekoMacro.Utils
{
    //public class MouseKeyWrapper : ReactiveObject
    //{
    //    private MouseButton _button;
    //    public MouseButton Button
    //    {
    //        get => _button;
    //        set => this.RaiseAndSetIfChanged(ref _button, value);
    //    }

    //    public MouseKeyWrapper()
    //    {

    //    }

    //    public MouseKeyWrapper(MouseButton state)
    //    {
    //        _button = state;
    //    }

    //    public override string ToString()
    //    {
    //        return Button.ToString();
    //    }

    //    public static List<MouseKeyWrapper> GetList() => Enum.GetValues(typeof(MouseButton)).Cast<MouseButton>().Select(_ => new MouseKeyWrapper(_)).ToList();
    //}

    //public class MouseDirWrapper : ReactiveObject
    //{
    //    private MouseDir _dir;
    //    public MouseDir Dir
    //    {
    //        get => _dir;
    //        set => this.RaiseAndSetIfChanged(ref _dir, value);
    //    }

    //    public MouseDirWrapper()
    //    {

    //    }

    //    public MouseDirWrapper(MouseDir state)
    //    {
    //        _dir = state;
    //    }

    //    public override string ToString()
    //    {
    //        return Dir.ToString();
    //    }

    //    public static List<MouseDirWrapper> GetList() => Enum.GetValues(typeof(MouseDir)).Cast<MouseDir>().Select(_ => new MouseDirWrapper(_)).ToList();
    //}
}
