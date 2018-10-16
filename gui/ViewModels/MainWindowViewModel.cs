using System;
using System.Collections.Generic;
using Avalonia.Controls;
using ReactiveUI;

namespace hacknet_viewer.gui.ViewModels {
	public class MainWindowViewModel : ViewModelBase {
		private List<string> _nodes = new List<string> {
			"Item 0"
		};

		private string _selectedNode;

		public DropDownViewModel() {
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