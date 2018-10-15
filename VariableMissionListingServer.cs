using System;
namespace hacknet_viewer {
	/*
	 * Mission server designed like Entropy and SlashBot
	 */
	public class VariableMissionListingServer: Daemon {
		public string Name { get; set; }
		public string IconPath { get; set; }
		public string ArticleFolderPath { get; set; }
		public string Color { get; set; }
		public bool Assigner { get; set; }
		public bool Public { get; set; } // Yeah, I know, not a good name
		public string Title { get; set; }

        public VariableMissionListingServer(string name, string iconPath, string articleFolderPath,
		                                   string color, bool assigner, bool isPublic, string title) {
			this.Name = name;
			this.IconPath = iconPath;
			this.ArticleFolderPath = articleFolderPath;
			this.Color = color;
			this.Assigner = assigner;
			this.Public = isPublic;
			this.Title = title;
        }

		public override string ToString() {
			return "<" + this.Name + " (variable mission listing server)" +
							  "\nicon path: " + this.IconPath +
							  "\narticle folder path: " + this.ArticleFolderPath +
							  "\ncolor: " + this.Color +
							  "\nassigner: " + this.Assigner +
							  "\npublic: " + this.Public +
							  "\ntitle = " + this.Title +
				              ">";
		}
	}
}