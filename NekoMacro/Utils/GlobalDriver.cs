﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interceptor;

namespace NekoMacro
{
    public static class GlobalDriver
    {
        public static Input _driver;

        public static bool Shift;
        public static bool Ctrl;
        public static bool Alt;

        public static void Load(KeyboardFilterMode                  keyFilterMode     = KeyboardFilterMode.All,
                                MouseFilterMode                     mouseFilterMode   = MouseFilterMode.All,
                                int                                 keyPressDelay     = 0,
                                int                                 clickDelay        = 0,
                                int                                 scrollDelay       = 50,
                                EventHandler<KeyPressedEventArgs>   keyPressHandler   = null,
                                EventHandler<MousePressedEventArgs> mousePressHandler = null)
        {
            //return;
            if (_driver != null && _driver.IsLoaded)
                return;
            _driver = new Input
            {
                KeyboardFilterMode = keyFilterMode,
                MouseFilterMode = mouseFilterMode,
                KeyPressDelay = keyPressDelay,
                ClickDelay = clickDelay,
                ScrollDelay = scrollDelay
            };


            if (keyPressHandler != null)
                KeyPressSubscribe(keyPressHandler);
            if (mousePressHandler != null)
                MousePressSubscribe(mousePressHandler);

            _driver.Load();
        }
        
        public static void Unload()
        {
            if (_driver == null || !_driver.IsLoaded)
                return;
            _driver.Unload();
        }

        public static void KeyPressSubscribe(EventHandler<KeyPressedEventArgs> keyPressHandler)
        {
            if (_driver == null)
                return;
            _driver.OnKeyPressed += keyPressHandler;
        }

        public static void MousePressSubscribe(EventHandler<MousePressedEventArgs> mousePressHandler)
        {
            if (_driver == null)
                return;
            _driver.OnMousePressed += mousePressHandler;
        }
    }
}
