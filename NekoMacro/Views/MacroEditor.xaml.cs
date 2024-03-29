﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NekoMacro.MacrosBase;
using NekoMacro.MacrosBase.NewCmd;
using NekoMacro.Utils.TreeDataGrid;
using NekoMacro.ViewModels;

namespace NekoMacro.Views
{
    /// <summary>
    /// Interaction logic for MacroEditor.xaml
    /// </summary>
    public partial class MacroEditor : Page
    {
        public MacroEditor()
        {
            InitializeComponent();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = DataContext as MacroEditViewModel;
            vm.CommandList.RemoveSelected(e.RemovedItems.Cast<BaseCmd>());
            vm.CommandList.AddSelected(e.AddedItems.Cast<BaseCmd>());
            if (vm.CommandList.SelectedItems.Count == 0)
            {
                vm.RepeatVisible       = false;
                vm.RepeatEditVisible   = false;
                vm.RepeatSetVisible    = false;
                vm.RepeatEscapeVisible = false;
            }
            if (vm.CommandList.SelectedItems.Count == 1)
            {
                if (vm.CommandList.SelectedItems.First() is RepeatCmd)
                {
                    vm.RepeatVisible       = true;
                    vm.RepeatEditVisible   = true;
                    vm.RepeatEscapeVisible = true;
                    vm.RepeatSetVisible    = false;
                }
                else
                {
                    vm.RepeatVisible       = true;
                    vm.RepeatEditVisible   = false;
                    vm.RepeatEscapeVisible = false;
                    vm.RepeatSetVisible    = true;
                }
            }
            else if (vm.CommandList.SelectedItems.Count > 0)
            {
                var lvl = vm.CommandList.SelectedItems.First().Level;
                if (vm.CommandList.SelectedItems.Any(_ => _.Level != lvl))
                {
                    vm.RepeatVisible       = false;
                    vm.RepeatEditVisible   = false;
                    vm.RepeatSetVisible    = false;
                    vm.RepeatEscapeVisible = false;
                }
                else
                {
                    vm.RepeatVisible       = true;
                    vm.RepeatEditVisible   = false;
                    vm.RepeatEscapeVisible = false;
                    vm.RepeatSetVisible    = true;
                }
            }

        }
    }
}
