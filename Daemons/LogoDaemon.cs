using System;
namespace hacknet_viewer {
	public class LogoDaemon:Daemon {
		public string Name { get; set; }
		public bool ShowsTitle { get; set; }
		public string LogoImagePath { get; set; } // Display fancy loading spinner if undefined
		public string TextColor { get; set; }
		public string Message { get; set; }

        public LogoDaemon(string name) {
			this.Name = name;
			this.ShowsTitle = true;
			this.TextColor = "0, 222, 220, 200";
			this.Message = "";
        }

		public override string ToString() {
			return "<" + this.Name + " (logo daemon)" +
							 "\nshows title: " + this.ShowsTitle +
				             "\nlogo image path: " + (this.LogoImagePath != "" ? this.LogoImagePath : "none ( fancy loading spinne)") +
							 "\ntext color: " + this.TextColor +
				             "\nmessage: " + this.Message +
				             "\n>";
		}
	}
}
