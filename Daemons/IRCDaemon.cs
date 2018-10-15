using System;
using System.Collections.Generic;

namespace hacknet_viewer {
	public class IRCDaemon: Daemon {
		public List<User> Users { get; set; }
		public List<Post> Posts { get; set; }

        public IRCDaemon() {
			this.Users = new List<User>();
			this.Posts = new List<Post>();
        }

		public override string ToString() {
			string usersString = "";
			foreach(User user in Users) {
				usersString += "name: " + user.name +
   							   "\ncolor: " + user.color + "\n";
			}
            
			string postsString = "";
			foreach(Post post in Posts) {
				postsString += "user: " + post.user +
							  "\nmessage: " + post.message + "\n";
			}

			return "<IRC daemon" +
				"\nusers: " + usersString.TrimEnd('\n') +
                 "\nposts: " + postsString.TrimEnd('\n') +
				 "\n>";
		}
	}

	public struct User {
		public string name;
		public string color;
	}

	public struct Post {
		public string user;
		public string message;
	}
}
