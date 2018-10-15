using System;
namespace hacknet_viewer {
	/*
	 * Mission server designed like CSEC
	 */
	public class MissionHubServer:Daemon {
		public string GroupName { get; set; }
		public string ServiceName { get; set; }
		public string MissionFolderPath { get; set; }
		public string ThemeColor { get; set; }
		public string LineColor { get; set; }
		public string BackgroundColor { get; set; }
		public bool AllowAbandon { get; set; }

        public MissionHubServer(string groupName, string serviceName, string missionFolderPath,
		                       string themeColor, string lineColor, string backgroundColor,
		                       bool allowAbandon) {
			this.GroupName = groupName;
			this.ServiceName = serviceName;
			this.MissionFolderPath = missionFolderPath;
			this.ThemeColor = themeColor;
			this.LineColor = lineColor;
			this.BackgroundColor = backgroundColor;
			this.AllowAbandon = allowAbandon;
        }

		public override string ToString() {
			return "<" + this.ServiceName + " (mission hub server)" +
				             "\ngroup name: " + this.GroupName +
							 "\nmission folder path: " + this.MissionFolderPath +
							 "\ntheme color: " + this.ThemeColor +
							 "\nline color: " + this.LineColor +
							 "\nbackground color: " + this.BackgroundColor +
							 "\nallow abandon: " + this.AllowAbandon +
							 ">";
		}
	}
}
