using System;
using Avalonia;
using Avalonia.Logging.Serilog;

using hacknet_viewer.gui;

namespace hacknet_viewer {
    class Program {
        static void Main(string[] args) {
            AppBuilder.Configure<App>().UsePlatformDetect().Start<MainWindow>();
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToDebug();
    }
}
