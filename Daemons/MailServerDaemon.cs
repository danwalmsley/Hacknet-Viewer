using System;
using System.Collections.Generic;

namespace hacknet_viewer {
	public class MailServerDaemon:Daemon {
		public string name { get; set; }
		public string color { get; set; }
		public bool generateJunk { get; set; }

		public List<Email> emails;

        public MailServerDaemon(string name) {
			this.name = name;
			this.color = "50,237,212";
			this.generateJunk = true;

			this.emails = new List<Email>();
        }

		public override string ToString() {
			string emailString = "";
			foreach(Email email in emails) {
				emailString += "Recipient: " + email.recipient +
							    "\nsender: " + email.sender +
                                "\nsubject: " + email.subject +
                                "\ncontent: " + email.content + "\n";
			}

			return "<" + this.name + " (Mail Server Daemon)" +
				             "\ncolor: " + this.color +
				             "\ngenerate junk: " + this.generateJunk +
				             "\nemails: " + emailString.Trim() + 
				             ">";
		}
	}

	public struct Email {
		public string recipient;
		public string sender;
		public string subject;
		public string content;
	};
}
