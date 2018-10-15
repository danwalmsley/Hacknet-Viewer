using System;
namespace hacknet_viewer {
	public class CreditsDaemon: Daemon {
		public string Title { get; set; }
		public string ButtonText { get; set; }
		public string ReallyLongName { get; set; } // Conditional action set path

        public CreditsDaemon(string title, string buttonText, string cASPath) {
			this.Title = title;
			this.ButtonText = buttonText;
			this.ReallyLongName = cASPath;
        }

		public override string ToString() {
			return "<credits daemon " +
			                "\ntitle: " + this.Title +
                            "\nbutton text: " + this.ButtonText +
							"\nconditional action set to run on button press path: " + this.ReallyLongName +
							">";
		}
	}
}
