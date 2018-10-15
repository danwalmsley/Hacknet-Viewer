using System;
namespace hacknet_viewer {
	public class LogoCustomConnectDisplayDaemon: Daemon {
		private string _buttonAlignment;

		public string Logo { get; set; }
		public string Title { get; set; }
		public bool OverdrawLogo { get; set; }
		public string ButtonAlignment { // Value must be left, middle, or right
			get {
				return this._buttonAlignment;
			}
			set {
				if(value == "left" || value == "middle" || value == "right")
					this._buttonAlignment = value;
				else
					throw new Exception("Invalid button alignment value: " + value + "!\nValue must be " +
					                    "'left','middle', or 'right'.");
			}
		}

        public LogoCustomConnectDisplayDaemon(string title) {
			this.Title = title;
			this.Logo = "";
			this.OverdrawLogo = true;
			this.ButtonAlignment = "left";
        }

		public override string ToString() {
			return "<" + this.Title + " (logo custom connect display daemon)" +
				             "\nlogo: " + this.Logo +
				             "\noverdraw logo: " + this.OverdrawLogo +
				             "\nbutton alignment: " + this.ButtonAlignment +
		                     "\n>";
		}
	}
}
