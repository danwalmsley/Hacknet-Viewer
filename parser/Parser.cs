using System;
using System.Collections.Generic;
using System.Xml;

using hacknet_viewer.parser;

namespace hacknet_viewer.parser {
	public class Parser {
		public List<Computer> Nodes { get; }
		
		public Parser() {
			Nodes = new List<Computer>();
		}

		public void Parse() {
			// Labyrinths compatibility will be fairly simple:
            // if your extension uses content from the DLC then
            // it will be properly parsed. There is no difference
            // in parsing from base or from Labyrinths!

			Nodes.Add(ComputerParser.ParseNode("IntroExtension/Nodes/ExampleComputer.xml"));
		}

		public Computer GetNode(string nodeName) {
			foreach(var node in Nodes) {
				if(node.name == nodeName) return node;
			}

			return null;
		}
	}		
}