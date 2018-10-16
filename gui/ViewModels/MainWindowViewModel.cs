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
		private string _nodeData;

		public MainWindowViewModel() {
			_nodes = new List<string>();

			_parser = new Parser();

			_parser.Parse();

			foreach(var node in _parser.Nodes)
				_nodes.Add(node.name);

			_selectedNode = _nodes[0];
			updateNodeDisplay();
		}

		private void updateNodeDisplay() {
			_nodeData = _parser.GetNode(_selectedNode).ToString();
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

		public string NodeData {
			get {
				return _nodeData;
			}
		}
	}
}