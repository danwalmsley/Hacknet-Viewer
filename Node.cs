﻿using System;
using System.Collections.Generic;

namespace hacknet_viewer {
	public class Node {
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
		public int portsForHack; // Above 100 causes an inviolability error
		public int proxy; // Set to -1 to remove

		public List<int> ports; // Valid ports: 21, 22, 25, 80, 1433, 104, 6881, 443, 192, 554

		// Firewall
		public int firewallLevel; // Set to -1 to remove
		public string firewallSolution;
		public double firewallAdditionalTime;

		// Trace
		public int traceTimer; // Set to -1 to remove

		// Admin

		public Node(string id, string name, string ip, string icon, 
			int security, string type, bool allowsDefaultBootModule) {
			this.id = id;
			this.name = name;
			this.ip = ip;
			this.icon = icon;
			this.security = security;
			this.type = type;
			this.allowsDefaultBootModule = allowsDefaultBootModule;

			this.portsForHack = 4;
			this.proxy = -1;

			this.files = new List<File>();
			this.links = new List<String>();
			this.ports = new List<int> {21, 22, 25, 80, 1433, 104, 6881, 443, 192, 554};

			// Firewall
			this.firewallLevel = -1; // Cannot be shorter than the solution! (unless -1)
			this.firewallSolution = "";
			this.firewallAdditionalTime = 1.0;

			// Trace
			this.traceTimer = -1;
		}

		public override string ToString() {
			string fileString = "";

			foreach(File file in files)
				fileString += file + "\n";

			string linkString = "";
			foreach(string link in links)
				linkString += link + "\n";

			return "<" + id + " (computer)\n" +
				"name: " + name + "\n" +
				"ip: " + ip + "\n" +
				"icon: " + icon +
				"\ntype: " + type +
				"\nsecurity: " + security +
				"\nallowsDefaultBootModule: " + allowsDefaultBootModule +
				"\nfiles: " + fileString +
				"\nlinks: " + linkString +
				">";
		}
	}
}

