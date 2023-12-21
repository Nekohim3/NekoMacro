using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Interceptor;
using ReactiveUI;

namespace NekoMacro
{
    public class MainWindowViewModel : ReactiveObject
    {
        public ReactiveCommand<Unit, Unit> TestCmd         { get; }
        public ReactiveCommand<Unit, Unit> LoadDriverCmd   { get; }
        public ReactiveCommand<Unit, Unit> UnloadDriverCmd { get; }

        private string _text;
        public string Text
        {
            get => _text;
            set => this.RaiseAndSetIfChanged(ref _text, value);
        }

        public MainWindowViewModel()
        {
            TestCmd         = ReactiveCommand.Create(OnTest);
            LoadDriverCmd   = ReactiveCommand.Create(OnLoadDriver);
            UnloadDriverCmd = ReactiveCommand.Create(OnUnloadDriver);
        }

        private void OnLoadDriver()
        {
            GlobalDriver.Load(keyPressHandler: DriverOnKeyPressed, mousePressHandler: DriverOnMousePressed);
        }

        private void DriverOnKeyPressed(object sender, KeyPressedEventArgs e)
        {
            Text += $"{e.Key} / {e.State}\n";
        }

        private void DriverOnMousePressed(object sender, MousePressedEventArgs e)
        {
            Text += $"{e.X} / {e.Y} / {e.State}\n";
        }

        private void OnTest()
        {
            new Thread(() =>
            {
                //GlobalDriver._driver.SendText("qweasdzxcrtyfghvbnuiojklm,.");
                //Thread.Sleep(500);
                //GlobalDriver._driver.MoveMouseTo(100, 100, true);
                Thread.Sleep(500);
                //GlobalDriver._driver.SendKey();
                GlobalDriver._driver.SendMouseEvent(MouseState.LeftDown);
                Thread.Sleep(250);
                GlobalDriver._driver.SendMouseEvent(MouseState.LeftUp);
                Thread.Sleep(500);
                GlobalDriver._driver.SendRightClick();
                Thread.Sleep(500);
            }).Start();
        }

        private void OnUnloadDriver()
        {
            GlobalDriver.Unload();
        }
    }
}
