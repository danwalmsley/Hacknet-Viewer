using System;

namespace hacknet_viewer {
	public class MemoryDumpFile: File {
		public Memory MemFile { get; set; }

		public MemoryDumpFile(string path, string name): base(path, name) {
			MemFile = new Memory();
        }

		public override string ToString() {
			return "<" + name + " (Memory Dump File)" +
				"\npath: " + path +
				"\nmemory file: " + MemFile +
				">";
		}
	}
}
