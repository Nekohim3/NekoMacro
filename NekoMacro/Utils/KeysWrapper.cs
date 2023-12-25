using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interceptor;
using ReactiveUI;

namespace NekoMacro.Utils
{
    //public class KeysWrapper : ReactiveObject
    //{
    //    private Keys _key;
    //    public Keys Key
    //    {
    //        get => _key;
    //        set => this.RaiseAndSetIfChanged(ref _key, value);
    //    }

    //    public string Text => Key.ToString();

    //    public KeysWrapper()
    //    {

    //    }

    //    public KeysWrapper(Keys key)
    //    {
    //        _key = key;
    //    }

    //    public override string ToString()
    //    {
    //        return Text;
    //    }

    //    public static List<KeysWrapper> GetList() => Enum.GetValues(typeof(Keys)).Cast<Keys>().Select(x => new KeysWrapper(x)).ToList();
    //}

    //public class KeyDirWrapper : ReactiveObject
    //{
    //    private KeyState _dir;
    //    public KeyState Dir
    //    {
    //        get => _dir;
    //        set => this.RaiseAndSetIfChanged(ref _dir, value);
    //    }

    //    public string Text => Dir.ToString();

    //    public KeyDirWrapper()
    //    {

    //    }

    //    public KeyDirWrapper(KeyState dir)
    //    {
    //        _dir = dir;
    //    }

    //    public override string ToString()
    //    {
    //        return Text;
    //    }

    //    public static List<KeyDirWrapper> GetList() => Enum.GetValues(typeof(KeyState)).Cast<KeyState>().Select(x => new KeyDirWrapper(x)).ToList();
    //}
}
