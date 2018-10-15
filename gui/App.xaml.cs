using Avalonia;
using Avalonia.Markup.Xaml;

namespace hacknet_viewer.gui {
    public class App : Application {
		public override void Initialize() {
            AvaloniaXamlLoader.Load(this);
        }
   }
}