using System;
using System.Collections.Generic;

namespace hacknet_viewer {
	public class DHSDaemon: Daemon {
		public string GroupName { get; set; }
		public bool AddsFactionPointOnMissionComplete { get; set; }
		public bool AutoClearMissionsOnPlayerComplete { get; set; }
		public string ThemeColor { get; set; }
		public bool AllowContractAbbandon { get; set; } // This is how it is spelled in the files

		public List<Agent> Agents { get; set; }

		public DHSDaemon() {
			this.GroupName = "";
			this.AddsFactionPointOnMissionComplete = true;
			this.AutoClearMissionsOnPlayerComplete = true;
			this.ThemeColor = "0,0,0";
			this.AllowContractAbbandon = true;
			this.Agents = new List<Agent>();
		}

		public override string ToString() {
			return "<" + this.GroupName + " (DHS daemon)" +
							 "\nadds faction point on mission complete: " + this.AddsFactionPointOnMissionComplete +
							 "\nauto clear missions on player complete: " + this.AutoClearMissionsOnPlayerComplete +
							 "\ntheme color: " + this.ThemeColor +
							 "\nallow contract abbandon: " + this.AllowContractAbbandon +
							 "\n>";
		}
	}

	public struct Agent {
		public string name;
		public string pass;
		public string color;
	}
}
