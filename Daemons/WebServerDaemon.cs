using System;
namespace hacknet_viewer {
	public class WebServerDaemon: Daemon {
		public string Name { get; set; }
		public string Url { get; set; }

        public WebServerDaemon(string name, string url) {
			this.Name = name;
			this.Url = url;
        }

		public override string ToString() {
			return "<" + this.Name + " (web server daemon)" +
							 "\nurl: " + this.Url + ">";
		}
	}
}
