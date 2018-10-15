using System;
using System.Xml;

namespace hacknet_viewer {
	public static class DatabaseParser {
		public static DatabaseDaemon ParseDatabaseNode(XmlNode node) {
			return new DatabaseDaemon("");
		}
	}
}
