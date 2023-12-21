using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interceptor;
using NekoMacro.MacrosBase;
using NekoMacro.Utils;
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

        public AllMacroViewModel()
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

            macros.AddCommand(new MouseCommand(MouseState.Moving, 123, 234));
            macros.AddCommand(new DelayCommand(50));
            macros.AddCommand(new MouseCommand(MouseState.LeftDown));
            macros.AddCommand(new DelayCommand(50));
            macros.AddCommand(new MouseCommand(MouseState.LeftUp));

            MacrosList.Add(macros);
        }
    }
}
