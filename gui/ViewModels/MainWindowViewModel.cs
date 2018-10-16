using System;
using System.Collections.Generic;
using Avalonia.Controls;
using ReactiveUI;

using hacknet_viewer.parser;

namespace hacknet_viewer.gui.ViewModels {
	public class MainWindowViewModel : ViewModelBase {
		private List<string> _nodes;

		private Parser _parser;

		private string _selectedNode;

		public MainWindowViewModel() {
			_parser = new Parser();

			_parser.Parse();

			Console.Write(_parser.Nodes[0]);

			_nodes.Add(_parser.Nodes[0].name);

			_selectedNode = _nodes[0];
		}

		public List<string> Nodes {
			get {
				return _nodes;
			}
		}
		public string SelectedNode {
			get {
				return _selectedNode;
			}

			set {
				this.RaiseAndSetIfChanged(ref _selectedNode, value);
			}
		}
	}
}