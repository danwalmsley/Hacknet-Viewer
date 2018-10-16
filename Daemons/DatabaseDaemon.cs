using System;
namespace hacknet_viewer {
	public class DatabaseDaemon:Daemon {
		public enum DatabasePermissions {
			AdminOnly,
			Public
		}
		private string _permissions;
		private string _dataType;
		public string DataType { get; set; }
		public string FolderName { get; set; }
		public string Color { get; set; }
		public string AdminEmailAccount { get; set; }
		public string AdminEmailHostID { get; set; }
		public string Name { get; set; }

        public DatabaseDaemon(string name) {
			this.Name = name;
			this.DataType = "";
			this.FolderName = "";
			this.Color = "";
			this.AdminEmailAccount = "";
			this.AdminEmailHostID = "";
        }

		public override string ToString() {
			throw new NotImplementedException();
		}
	}
}