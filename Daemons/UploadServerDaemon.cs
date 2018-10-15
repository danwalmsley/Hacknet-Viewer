using System;
namespace hacknet_viewer {
	public class UploadServerDaemon: Daemon {
        
		public string Name { get; set; }
		public string Folder { get; set; }
		public bool NeedsAuth { get; set; }
		public string Color { get; set; }
        
		public UploadServerDaemon(string name, string folder, bool needsAuth, string color) {
			this.Name = name;
			this.Folder = folder;
			this.NeedsAuth = needsAuth;
			this.Color = color;
        }

		public override string ToString() {
			return "<" + this.Name + " (upload server daemon)" +
				             "\nfolder: " + this.Folder +
				             "\nneeds authentication: " + this.NeedsAuth +
							 "\ncolor: " + this.Color +
							 ">";
		}
	}
}
