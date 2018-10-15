using System;

namespace hacknet_viewer {
	public class CustomThemeFile: File {
		public string ThemePath { get; set; }

		public CustomThemeFile(string path, string name, string themePath): base(path, name) {
			this.ThemePath = themePath;
        }

		public override string ToString() {
			return "<" + name + " (CustomThemeFile)" +
                "\npath: " + path +
                "\nthemePath: " + ThemePath +
                ">";
		}
	}
}