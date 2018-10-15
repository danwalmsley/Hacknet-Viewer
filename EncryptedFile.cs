using System;
namespace hacknet_viewer {
	public class EncryptedFile: File {
		public string ip { get; set; }
		public string header { get; set; }

		public EncryptedFile(string path, string name, string ip, string header, string content): base(path, name) {
			this.ip = ip;
			this.header = header;
			this.content = content;
        }

		public override string ToString() {
			return "<" + name + " (EncryptedFile)" +
                "\npath: " + path +
				"\nip: " + ip +
				"\nheader: " + header +
                "\ncontent: " + content +
                ">";
		}
	}
}
