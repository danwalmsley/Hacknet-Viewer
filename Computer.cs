using System;
using System.Collections.Generic;

namespace hacknet_viewer {
	public class Computer {
		public string id;
		public string name;
		public string ip;
		public string icon;
		public string type;
		public int security;
		public bool allowsDefaultBootModule;

		public List<File> files;
		public List<String> links;

		// Security
		public int portsForCrack; // Above 100 causes an inviolability error
		public int proxyTime; // Set to -1 to remove

		public List<int> ports; // Valid ports: 21, 22, 25, 80, 1433, 104, 6881, 443, 192, 554

		// Firewall
		public int firewallLevel; // Set to -1 to remove
		public string firewallSolution;
		public double firewallAdditionalTime;

		// Trace
		public int traceTimer; // Set to -1 to remove]
		public bool passiveTracker;

		// Admin
		public string adminPass;
		public string adminType;
		public bool adminResetPass;
		public bool adminIsSuper;

		// Users
		public List<Account> accounts;

		// Port remapping
		public Dictionary<int, int> portRemapping;

		// Positioning
		public Position position;

		// eOSDevice
		public EOSDevice eOSDevice;

		// Daemons
		/*
         * Booleans that set daemons for the node.
         * These are simply set by having the daemon tag
         * in the XML file for the node.
         */
		public List<Daemon> daemons;

		public Memory MemoryDump { get; set; }

		public Computer(string id, string name, string ip, string icon,
			int security, string type, bool allowsDefaultBootModule) {
			this.id = id;
			this.name = name;
			this.ip = ip;
			this.icon = icon;
			this.security = security;
			this.type = type;
			this.allowsDefaultBootModule = allowsDefaultBootModule;

			this.portsForCrack = 4;
			this.proxyTime = -1;

			this.files = new List<File>();
			this.links = new List<String>();
			this.ports = new List<int> { 21, 22, 25, 80, 1433, 104, 6881, 443, 192, 554 };

			// Firewall
			this.firewallLevel = -1; // Cannot be shorter than the solution! (unless -1)
			this.firewallSolution = "";
			this.firewallAdditionalTime = 1.0;

			// Trace
			this.traceTimer = -1;
			this.passiveTracker = false;

			// Admin
			this.accounts = new List<Account> {
				new Account { name = "admin", password = "", type = "admin" }
			};

			this.adminType = "progress";
			this.adminResetPass = false;
			this.adminIsSuper = false;

			// Port remapping
			this.portRemapping = new Dictionary<int, int>();

			// Position
			this.position.target = "";
			this.position.pos = 0;
			this.position.total = 0;
			this.position.extraDistance = 0.0;
			this.position.force = false;

			// eOSDevice
			this.eOSDevice = null;

			// Daemons
			this.daemons = new List<Daemon>();

			this.MemoryDump = new Memory();
		}

		public override string ToString() {
			string fileString = "";

			foreach(File file in files)
				fileString += file + "\n";

			string linkString = "";
			foreach(string link in links)
				linkString += link + "\n";

			string portString = "";
			foreach(int port in this.ports)
				portString += port + ", ";

			string accountString = "";
			foreach(Account acc in accounts) {
				if(acc.name != "admin")
				    accountString += acc.name + ": " + acc.password + " type: " + acc.type + "\n";
			}

			string portRemappingString = "";
			foreach(KeyValuePair<int, int> entry in portRemapping) {
				portRemappingString += entry.Key + ": " + entry.Value + ", ";
			}

			string positionString = "target: " + position.target + "\npos: " + position.pos +
                                 "\ntotal: " + position.total +
                                 "\nextra distance: " + position.extraDistance +
                                 "\nforce: " + position.force;

			string daemonString = "";
			foreach(Daemon daemon in daemons) {
				daemonString += daemon + "\n";
			}

			return "<" + id + " (computer)\n" +
				"name: " + name + "\n" +
				"ip: " + ip + "\n" +
				"icon: " + icon +
				"\ntype: " + type +
				"\nsecurity: " + security +
				"\nallowsDefaultBootModule: " + allowsDefaultBootModule +
				"\nfiles: " + fileString.TrimEnd('\n') +
				"\nlinks: " + linkString.TrimEnd('\n') +
				"\nports: " + portString.Trim().TrimEnd(',') +
				"\nports for crack: " + portsForCrack + (portsForCrack > 100 ? " (INVIOLABILITY)" : "") +
				"\nport remap: " + portRemappingString.Trim().TrimEnd(',') +
				"\nproxy time: " + proxyTime + (proxyTime == -1 ? " (disabled)" : "") +
				"\nfirewall level: " + firewallLevel + (firewallLevel == -1 ? " (disabled)" : "") +
				"\nfirewall solution: " + firewallSolution +
				"\nfirewall additional time: " + firewallAdditionalTime +
				"\ntrace timer: " + traceTimer + (traceTimer == -1 ? " (disabled)" : "") +
				"\npassive trace: " + passiveTracker +
				"\nadmin password: " + adminPass +
				"\nadmin type: " + adminType +
				"\nadmin reset password: " + adminResetPass +
				"\nadmin is super: " + adminIsSuper +
				"\naccounts: " + accountString.TrimEnd('\n') +
				"\nposition: " + positionString +
				"\neOS Device: " + (eOSDevice == null ? "none" : eOSDevice.ToString()) +
				"\ndaemons: " + daemonString.Trim() +
                "\nmemory: " + MemoryDump +
				">";
		}
	}

	public struct Account {
		public string name;
		public string password;
		public string type;
	}

	public struct Position {
		public string target;
		public int pos;
		public int total;
		public double extraDistance;
		public bool force;
	}

	public struct Mail {
        public string username;
        public string pass;
    }
}