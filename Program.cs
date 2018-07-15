using System;
using Gtk;

namespace hacknet_viewer
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Parser parser = new Parser();

			foreach(Node node in parser.nodes)
				Console.WriteLine(node);

			return;

			Application.Init();
			MainWindow win = new MainWindow();
			win.Show();
			Application.Run();
		}
	}
}