using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NekoMacro.ViewModels;

namespace NekoMacro.Views
{
    /// <summary>
    /// Interaction logic for AllMacroView.xaml
    /// </summary>
    public partial class AllMacroView : Page
    {
        public AllMacroView()
        {
            InitializeComponent();
        }

        private void AllMacroView_OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Delete)
                if(DataContext is AllMacroViewModel vm)
                    vm.OnDeleteCommand();
        }
    }
}
