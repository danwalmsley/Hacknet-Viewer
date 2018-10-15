using System;
namespace hacknet_viewer {
	public class DatabaseDaemon:Daemon {
		private string _permissions;
		private string _dataType;

		public string Permissions {
			get {
				return this._permissions;
			}
			set {
				if(value == "public" || value == "private")
					this._permissions = value;
				else
					throw new Exception("Invalid permission string. Must be 'public' or 'private'");
			}
		}
		public string DataType { get; set; }
		public string FolderName { get; set; }
		public string Color { get; set; }
		public string AdminEmailAccount { get; set; }
		public string AdminEmailHostID { get; set; }
		public string Name { get; set; }

        public DatabaseDaemon(string name) {
			this.Name = name;
			this.Permissions = "private";
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