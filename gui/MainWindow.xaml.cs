using System;
using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using ReactiveUI;

using System.Collections.Generic;
using System.Diagnostics;

using hacknet_viewer.gui.ViewModels;

namespace hacknet_viewer.gui {
    public class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            DataContext = new MainWindowViewModel();
        }

		private void InitializeComponent() {
            AvaloniaXamlLoader.Load(this);
        }
    }
}