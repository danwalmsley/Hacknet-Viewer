using System;
using System.Collections.Generic;
using System.Xml;

using hacknet_viewer.parser;

namespace hacknet_viewer.parser {
	public class Parser {
		public Dictionary<string, Computer> Nodes { get; }
		
		public Parser() {
			Nodes = new Dictionary<string, Computer>();
		}

		public void Parse() {
			// Labyrinths compatibility will be fairly simple:
            // if your extension uses content from the DLC then
            // it will be properly parsed. There is no difference
            // in parsing from base or from Labyrinths!

			var node = ComputerParser.ParseNode("IntroExtension/Nodes/ExampleComputer.xml");
			Nodes.Add(node.name, node);
		}
	}		
}