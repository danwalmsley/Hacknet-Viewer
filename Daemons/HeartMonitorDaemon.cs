using System;
namespace hacknet_viewer {
	public class HeartMonitorDaemon: Daemon {
		public string Patient { get; set; }

		public HeartMonitorDaemon(string patient) {
			this.Patient = patient;
		}

		public override string ToString() {
			return "<heart monitor daemon\npatient: " + this.Patient + ">";
		}
	}
}
