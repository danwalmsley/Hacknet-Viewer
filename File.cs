using System;

namespace hacknet_viewer {
	public class File {
		public string path;
		public string name;
		public string content;

		public File(string path, string name, string content) {
			this.path = path;
			this.name = name;
			this.content = content;
		}

		public override string ToString() {
			return "<" + name + "(File)" +
				"\npath: " + path +
				"\ncontent: " + content +
				">";
		}
	}
}

