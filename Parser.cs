using System;
using System.Collections.Generic;
using System.Xml;

namespace hacknet_viewer {
	public class Parser {
		public List<Node> nodes;
		public Parser() {
			nodes = new List<Node>();

			nodes.Add(parseNode(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/IntroExtension/Nodes/PlayerComp.xml"));
			nodes.Add(parseNode(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/IntroExtension/Nodes/Intro/FactionEntryNode.xml"));
		}

		private Node parseNode(string filename) {
			XmlDocument doc = new XmlDocument();
			doc.Load(filename);

			XmlElement root = doc.DocumentElement;

			string nodeId = root.Attributes["id"].Value;
			string nodeName = root.Attributes["name"].Value;
			string nodeIp =  root.HasAttribute("ip") ? root.Attributes["ip"].Value : "";
			string nodeIcon = root.HasAttribute("icon") ? root.Attributes["icon"].Value : "chip";
			string nodeType = root.HasAttribute("type") ? root.Attributes["type"].Value : "1";
			int nodeSecurity = 0;
			bool nodeABDM = false; // Allows default boot module

			// Parsing shit, because C# sucks!
			if(root.HasAttribute("security"))
				Int32.TryParse(root.Attributes["security"].Value, out nodeSecurity);

			if(root.HasAttribute("allowsDefaultBootModule"))
				bool.TryParse(root.Attributes["allowsDefaultBootModule"].Value, out nodeABDM);

			Node node = new Node(nodeId, nodeName, nodeIp, nodeIcon, nodeSecurity, nodeType, nodeABDM, "", new List<Account>());

			// Parse through files!
			List<File> nodeFiles = new List<File>();
			XmlNodeList files = root.GetElementsByTagName("file");

			foreach(XmlNode file in files) {
				nodeFiles.Add(parseFile(file));
			}

			node.files = nodeFiles;

			// Parse through dlinks
			List<string> nodeDLinks = new List<String>();
			XmlNodeList links = root.GetElementsByTagName("dlink");

			foreach(XmlNode link in links) {
				nodeDLinks.Add(link.Attributes["target"].Value);
			}

			XmlNode ports = root.SelectSingleNode("ports");
			List<int> nodePorts = new List<int>();

			foreach(string port in ports.InnerText.Split(',')) {
				int portNum;
				Int32.TryParse(port.Trim(), out portNum);
				nodePorts.Add(portNum);
			 }

			node.links = nodeDLinks;

			return node;
		}

		private File parseFile(XmlNode fileXML) {
			string filePath = fileXML.Attributes["path"].Value;
			string fileName = fileXML.Attributes["name"].Value;
			string fileContent = fileXML.InnerText;

			return new File(filePath, fileName, fileContent);
		}
	}
}