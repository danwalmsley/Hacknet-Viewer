﻿using System;
using System.Collections.Generic;
using System.Xml;

namespace hacknet_viewer.parser {
	public class Parser {
		public List<Computer> nodes;
		
		public Parser() {
			// Labyrinths compatibility will be fairly simple:
            // if your extension uses content from the DLC then
            // it will be properly parsed. There is no difference
            // in parsing from base or from Labyrinths!

			nodes = new List<Computer> {
				ComputerParser.ParseNode(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/IntroExtension/Nodes/ExampleComputer.xml")
			};
		}
	}		
}