using System;
using System.Collections.Generic;
using Avalonia.Controls;
using ReactiveUI;

using hacknet_viewer.parser;
using Avalonia.Threading;
using System.Collections.ObjectModel;

namespace hacknet_viewer.gui.ViewModels
{
    public class TabViewModel : ViewModelBase
    {
        public TabViewModel(string title)
        {
            _exampleProperty = "Text from example property - " + title;

            _title = title;
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { this.RaiseAndSetIfChanged(ref _title, value); }
        }


        private string _exampleProperty;

        public string ExampleProperty
        {
            get { return _exampleProperty; }
            set { this.RaiseAndSetIfChanged(ref _exampleProperty, value); }
        }

    }


    public class MainWindowViewModel : ViewModelBase
    {
        private List<string> _nodes;

        private Parser _parser;

        private string _selectedNode;
        private string _nodeData;

        public MainWindowViewModel()
        {
            _nodes = new List<string>();

            _parser = new Parser();

            _parser.Parse();

            foreach (var node in _parser.Nodes)
                _nodes.Add(node.Key);

            _selectedNode = _nodes[0];
            updateNodeDisplay();

            Items = new ObservableCollection<TabViewModel>();

            Items.Add(new TabViewModel("Tab 1"));
            Items.Add(new TabViewModel("Tab 2"));
            Items.Add(new TabViewModel("Tab 3"));
            Items.Add(new TabViewModel("Tab 4"));
        }

        private ObservableCollection<TabViewModel> _items;

        public ObservableCollection<TabViewModel> Items
        {
            get { return _items; }
            set { this.RaiseAndSetIfChanged(ref _items, value); }
        }

        private TabViewModel _selectedItem;

        public TabViewModel SelectedItem
        {
            get { return _selectedItem; }
            set { this.RaiseAndSetIfChanged(ref _selectedItem, value); }
        }


        private void updateNodeDisplay()
        {
            _nodeData = _parser.Nodes[_selectedNode].ToString();
        }

        public List<string> Nodes
        {
            get
            {
                return _nodes;
            }
        }
        public string SelectedNode
        {
            get
            {
                return _selectedNode;
            }

            set
            {
                this.RaiseAndSetIfChanged(ref _selectedNode, value);
            }
        }

        public string NodeData
        {
            get
            {
                return _nodeData;
            }
        }
    }
}