using System;
using System.Collections.Generic;

namespace hacknet_viewer {
	public class EOSDevice {
		public string name { get; set; }
		public string id { get; set; }
		public string icon { get; set; }
		public bool empty { get; set; } // Might mean it doesn't autofill?
		public string passOverride { get; set; }

		public List<Mail> mail { get; set; }
		public List<File> files { get; set; }

        public EOSDevice(string name, string id) {
			this.name = name;
			this.id = id;
			this.icon = "ePhone3";
			this.empty = false;
			this.passOverride = "";

			this.mail = new List<Mail>();
			this.files = new List<File>();
        }

		public override string ToString() {
			string mailString = "";
			foreach(Mail m in mail) {
				mailString += "username: " + m.username + "\npassword: " + m.pass + "\n";
			}

			string fileString = "";
			foreach(File f in files) {
				fileString += f + "\n";
			}

			return "<" + this.id + " (eOSDevice)" +
							 "\nname: " + this.name +
							 "\nicon: " + this.icon +
							 "\nempty: " + this.empty +
							 "\npassword override: " + this.passOverride +
							 "\nmail accounts: " + mailString.Trim() +
			                 "\nfiles: " + fileString.Trim() +
				             ">";
		}
	}
}
