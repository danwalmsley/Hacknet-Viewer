using System;
using System.Collections.Generic;

namespace hacknet_viewer {
	public class MessageBoardDaemon:Daemon {
		public string Name { get; set; }

		public List<Thread> Threads { get; set; }
        
		public MessageBoardDaemon(string name) {
			this.Name = name;

			this.Threads = new List<Thread>();
        }

		public override string ToString() {
			string threadString = "";
			foreach(Thread thread in Threads)
				threadString += "<thread\n" + thread.content + ">\n";

			return "<" + this.Name + " (message board daemon)" +
							 "\nthreads: " + threadString.Trim() +
				             "\n>";
		}
	}

	public struct Thread {
		public string content;
	};
}
