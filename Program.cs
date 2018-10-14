using System;
using Avalonia;
using Avalonia.Logging.Serilog;

using hacknet_viewer.gui;

namespace hacknet_viewer {
    class Program {
        static void Main(string[] args) {
            BuildAvaloniaApp().Start<MainWindow>();

			Parser parser = new Parser();

            AppBuilder.Configure<App>().UsePlatformDetect().Start<MainWindow>();
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToDebug();
    }
}
