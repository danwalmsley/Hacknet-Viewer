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

namespace hacknet_viewer.gui {
    public class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

		private void InitializeComponent() {
            AvaloniaXamlLoader.Load(this);
        }
    }
}