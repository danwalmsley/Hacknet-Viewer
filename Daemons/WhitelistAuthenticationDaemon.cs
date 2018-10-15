using System;
namespace hacknet_viewer {
	public class WhitelistAuthenticationDaemon: Daemon {
		private bool _selfAuthenticating;
		private string _remote;

		public WhitelistType Type { get; set; }
		public bool SelfAuthenticating {
			get {
				return this._selfAuthenticating;
			}
			set {
				if(this.Type == WhitelistType.REMOTE) {
					this._selfAuthenticating = false;
				} else {
					this._selfAuthenticating = value;
				}
			}
		}

		public string Remote {
			get {
				return this._remote;
			}
			set {
				if(this.Type == WhitelistType.SELF_AUTHENTICATING)
					this._remote = "";
				else
					this._remote = value;
			}
		}

        public WhitelistAuthenticationDaemon() {
			this.SelfAuthenticating = false;
			this.Remote = "";
        }

		public override string ToString() {
			return "<whitelist authentication daemon" +
				(this.Type == WhitelistType.REMOTE ?
					"\nremote: " + this.Remote :
					"\nself authenticating: " + this.SelfAuthenticating) +
				">";
		}
	}

	public enum WhitelistType {
		REMOTE,
        SELF_AUTHENTICATING
	}
}