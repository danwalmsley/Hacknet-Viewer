using System;
using System.Collections.Generic;
using System.Xml;

using hacknet_viewer;

namespace hacknet_viewer.parser {
    public static class ComputerParser {
        public static Computer ParseNode(string filename) {
			XmlDocument doc = new XmlDocument();
			doc.Load(filename);

			XmlElement root = doc.DocumentElement;
            
			string nodeId = root.Attributes["id"].Value;
			string nodeName = root.Attributes["name"].Value;
			string nodeIp =  root.HasAttribute("ip") ? root.Attributes["ip"].Value : "";
			string nodeIcon = root.HasAttribute("icon") ? root.Attributes["icon"].Value : "chip";
			string nodeType = root.HasAttribute("type") ? root.Attributes["type"].Value : "1";
			int nodeSecurity = 0;
			bool nodeABDM = false; // Allows default boot module

			// Parsing shit, because C# sucks!
			if(root.HasAttribute("security"))
				Int32.TryParse(root.Attributes["security"].Value, out nodeSecurity);

			if(root.HasAttribute("allowsDefaultBootModule"))
				bool.TryParse(root.Attributes["allowsDefaultBootModule"].Value, out nodeABDM);

			Computer node = new Computer(nodeId, nodeName, nodeIp, nodeIcon, nodeSecurity, nodeType, nodeABDM);

			// Parse through files!
			List<File> nodeFiles = new List<File>();
			XmlNodeList files = root.SelectNodes("file");
            
			foreach(XmlNode file in files) {
				nodeFiles.Add(ParseFile(file));
			}

			// Parse custom theme files
			XmlNodeList customThemeFiles = root.SelectNodes("customthemefile");
			foreach(XmlNode file in customThemeFiles) {
				nodeFiles.Add(ParseCustomThemeFile(file));
			}

			// Parse encrypted files
			XmlNodeList encryptedFiles = root.SelectNodes("encryptedFile");
			foreach(XmlNode file in encryptedFiles) {
				nodeFiles.Add(ParseEncryptedFile(file));
			}

			node.files = nodeFiles;

			// Parse through dlinks
			List<string> nodeDLinks = new List<String>();
			XmlNodeList links = root.SelectNodes("dlink");

			foreach(XmlNode link in links) {
				nodeDLinks.Add(link.Attributes["target"].Value);
			}

			node.links = nodeDLinks;

			XmlNode ports = root.SelectSingleNode("ports");
			List<int> nodePorts = new List<int>();

			foreach(string port in ports.InnerText.Split(',')) {
				Int32.TryParse(port.Trim(), out int portNum);
				nodePorts.Add(portNum);
		    }

			if(nodePorts.Count > 0)
				node.ports = nodePorts;

			XmlNode portsForCrack = root.SelectSingleNode("portsForCrack");
			if(portsForCrack != null) {
				Int32.TryParse(portsForCrack.Attributes["val"].Value, out int nodePortsForCrack);
				node.portsForCrack = nodePortsForCrack;
			}

			XmlNode proxy = root.SelectSingleNode("proxy");
			if(proxy != null) {
				Int32.TryParse(proxy.Attributes["time"].Value, out int nodeProxyTime);
				node.proxyTime = nodeProxyTime;
			}

			XmlNode trace = root.SelectSingleNode("trace");
			if(trace != null) {
				Int32.TryParse(trace.Attributes["time"].Value, out int nodeTraceTime);
				node.traceTimer = nodeTraceTime;
			}

			XmlNode adminPass = root.SelectSingleNode("adminPass");
			if(adminPass != null) {
				node.adminPass = adminPass.Attributes["pass"].Value;
				Account tmp = new Account { name = "admin", password = node.adminPass, type = "admin" };
				node.accounts[0] = tmp;
			}

			XmlNodeList accList = root.SelectNodes("account");
			foreach(XmlNode accNode in accList) {
				string accName = accNode.Attributes["username"].Value;
				string accPass = accNode.Attributes["password"].Value;
				string accType = accNode.Attributes["type"].Value;

				node.accounts.Add(new Account { name = accName, password = accPass, type = accType });
			}

			node.passiveTracker |= root.SelectSingleNode("tracker") != null; // Monodev decided this was a better solution

			// Port remapping
			XmlNode portMaps = root.SelectSingleNode("portRemap");
			if(portMaps != null) {
				string remapString = portMaps.InnerText;
				Dictionary<int, int> nodePortRemap = new Dictionary<int, int>();
				foreach(string mapping in remapString.Split(',')) {
					string port = mapping.Split('=')[0];
					Int32.TryParse(mapping.Split('=')[1], out int remap);

					if(!Int32.TryParse(port, out int portNumber)) {
						portNumber = PortNameToInt(port);
					}

					nodePortRemap[portNumber] = remap;
				}

				node.portRemapping = nodePortRemap;
			}

			// Positioning
			XmlNode positionNear = root.SelectSingleNode("positionNear");
			if(positionNear != null) {
				Position nodePos;
				nodePos.target = positionNear.Attributes["target"].Value;
				Int32.TryParse(positionNear.Attributes["position"].Value, out nodePos.pos);
				Int32.TryParse(positionNear.Attributes["total"].Value, out nodePos.total);
				Double.TryParse(positionNear.Attributes["extraDistance"].Value, out nodePos.extraDistance);
				Boolean.TryParse(positionNear.Attributes["force"].Value, out nodePos.force);

				node.position = nodePos;
			}

			// eOS Device
			XmlNode eOSDevice = root.SelectSingleNode("eosDevice");
			if(eOSDevice != null)
				node.eOSDevice = ParseEOSDevice(eOSDevice);

			// Daemons
            /*
             * So, it turns out that you can have multiples of each daemon.
             * Why you would want to do that, I don't know. Seems like a dumb
             * idea, but if it's in the game then it should be reflected here as
             * well.
             */
			XmlNodeList mailDaemonNodes = root.SelectNodes("mailServer");
			foreach(XmlNode mailNode in mailDaemonNodes)
				node.daemons.Add(ParseMailDaemon(mailNode));

			XmlNodeList uploadDaemonNodes = root.SelectNodes("uploadServerDaemon");
			foreach(XmlNode uploadNode in uploadDaemonNodes)
				node.daemons.Add(ParseUploadDaemon(uploadNode));

			XmlNodeList webServerNodes = root.SelectNodes("addWebServer");
            // I found out that you can actually have multiple web servers on one node!
			foreach(XmlNode webServerNode in webServerNodes)
				node.daemons.Add(ParseWebServerDaemon(webServerNode));

			XmlNodeList deathRowNodes = root.SelectNodes("deathRowDatabase");
			foreach(XmlNode deathRowNode in deathRowNodes)
				node.daemons.Add(new DeathRowDatabaseDaemon());

			XmlNodeList academicNodes = root.SelectNodes("academicDatabase");
            foreach(XmlNode academicNode in academicNodes)
				node.daemons.Add(new AcademicDatabaseDaemon());

			XmlNodeList ispNodes = root.SelectNodes("ispSystem");
            foreach(XmlNode ispNode in ispNodes)
				node.daemons.Add(new IspSystemDaemon());

			XmlNodeList msgBoardNodes = root.SelectNodes("messageBoard");
			foreach(XmlNode msgBoardNode in msgBoardNodes)
				node.daemons.Add(ParseMessageBoardDaemon(msgBoardNode));

			XmlNodeList medicalDatabaseNodes = root.SelectNodes("MedicalDatabase");
			foreach(XmlNode medicalDatabaseNode in medicalDatabaseNodes)
				node.daemons.Add(new MedicalDatabaseDaemon());

			XmlNodeList heartMonitorList = root.SelectNodes("HeartMonitor");
			foreach(XmlNode heartMonitorNode in heartMonitorList)
				node.daemons.Add(new HeartMonitorDaemon(heartMonitorNode.Attributes["patient"].Value));

			XmlNodeList pointClickerNodes = root.SelectNodes("PointClicker");
			foreach(XmlNode pointClickerNode in pointClickerNodes)
				node.daemons.Add(new PointClickerDaemon());

			XmlNodeList songChangerNodes = root.SelectNodes("SongChangerDaemon");
			foreach(XmlNode songChangerNode in songChangerNodes)
				node.daemons.Add(new SongChangerDaemon());

			XmlNodeList vMLSNodes = root.SelectNodes("variableMissionListingServer");
			foreach(XmlNode daemon in vMLSNodes)
				node.daemons.Add(ParseVariableMissionListingServer(daemon));

			XmlNodeList missionHubServerNodes = root.SelectNodes("missionHubServer");
			foreach(XmlNode daemon in missionHubServerNodes)
				node.daemons.Add(ParseMissionHubServer(daemon));

			XmlNodeList creditsDaemonNodes = root.SelectNodes("CreditsDaemon");
			foreach(XmlNode daemon in creditsDaemonNodes)
				node.daemons.Add(ParseCreditsDaemon(daemon));

			XmlNodeList fastActionHostNodes = root.SelectNodes("FastActionHost");
			foreach(XmlNode daemon in fastActionHostNodes)
				node.daemons.Add(new FastActionHost());

			// Labyrinths content
			XmlNode memDumpFileNode = root.SelectSingleNode("memoryDumpFile");
			if(memDumpFileNode != null) {
				node.files.Add(ParseMemDumpFile(memDumpFileNode));
			}

			XmlNode memoryNode = root.SelectSingleNode("Memory");
			if(memoryNode != null)
				node.MemoryDump = ParseMemory(memoryNode);

			XmlNodeList customConnectDisplayNodes = root.SelectNodes("CustomConnectDisplayDaemon");
            foreach(XmlNode customConnectDisplayNode in customConnectDisplayNodes)
				node.daemons.Add(new CustomConnectDisplayDaemon());

			XmlNode logoDaemonNode = root.SelectSingleNode("LogoDaemon");
			if(logoDaemonNode != null)
				node.daemons.Add(ParseLogoDaemon(logoDaemonNode));

			XmlNode lccddNode = root.SelectSingleNode("LogoCustomConnectDisplayDaemon");
			if(lccddNode != null)
				node.daemons.Add(ParseLCCDD(lccddNode));

			XmlNode wadNode = root.SelectSingleNode("WhitelistAuthenticatorDaemon");
			if(wadNode != null)
				node.daemons.Add(ParseWhitelistAuthenticationDaemon(wadNode));

			XmlNode IRCDaemonNode = root.SelectSingleNode("IRCDaemon");
			if(IRCDaemonNode != null)
				node.daemons.Add(ParseIRCDaemon(IRCDaemonNode));

			XmlNode DHSDaemonNode = root.SelectSingleNode("DHSDaemon");
			if(DHSDaemonNode != null)
				node.daemons.Add(ParseDHSDaemon(DHSDaemonNode));

			return node;
		}

		private static int PortNameToInt(string portName) {
			Dictionary<string, int> portDict = new Dictionary<string, int> {
				["ssh"] = 22,
				["ftp"] = 21,
				["web"] = 80,
				["torrent"] = 6881,
				["medical"] = 104,
				["smtp"] = 25,
				["sql"] = 1433

			};

			return portDict[portName];
		}

		private static File ParseFile(XmlNode fileXML) {
			string filePath = fileXML.Attributes["path"].Value;
			string fileName = fileXML.Attributes["name"].Value;
			string fileContent = fileXML.InnerText;

			File file = new File(filePath, fileName) {
				content = fileContent
			};

			return file;
		}

		private static CustomThemeFile ParseCustomThemeFile(XmlNode fileXml) {
			string filePath = fileXml.Attributes["path"].Value;
			string fileName = fileXml.Attributes["name"].Value;
			string fileThemePath = fileXml.Attributes["themePath"].Value;

			return new CustomThemeFile(filePath, fileName, fileThemePath);
		}

		private static EncryptedFile ParseEncryptedFile(XmlNode fileXml) {
			string filePath = fileXml.Attributes["path"].Value;
			string fileName = fileXml.Attributes["name"].Value;
			string fileIp = fileXml.Attributes["ip"].Value;
			string fileHeader = fileXml.Attributes["header"].Value;
			string fileContent = fileXml.InnerText;

			return new EncryptedFile(filePath, fileName, fileIp, fileHeader, fileContent);
		}

		private static EOSDevice ParseEOSDevice(XmlNode node) {
			string devName = node.Attributes["name"].Value;
			string devId = node.Attributes["id"].Value;
			string devIcon = node.Attributes["icon"] != null ? node.Attributes["icon"].Value : "";
			bool devEmpty = false;
			if(node.Attributes["empty"] != null) {
				Boolean.TryParse(node.Attributes["empty"].Value, out devEmpty);
			}
			string devPassOverride = node.Attributes["passOverride"] != null ? node.Attributes["passOverride"].Value : "";

			XmlNodeList mailNodes = node.SelectNodes("mail");
			List<Mail> devMail = new List<Mail>();
			foreach(XmlNode mail in mailNodes) {
				devMail.Add(ParseMail(mail));
			}

			XmlNodeList files = node.SelectNodes("file");
			List<File> devFiles = new List<File>();
			foreach(XmlNode file in files) {
				devFiles.Add(ParseFile(file));
			}

			EOSDevice dev = new EOSDevice(devName, devId) {
				icon = devIcon,
				empty = devEmpty,
				passOverride = devPassOverride,
				mail = devMail,
				files = devFiles
			};

			return dev;
		}

		private static Mail ParseMail(XmlNode mail) {
			Mail m;
			m.username = mail.Attributes["username"].Value;
			m.pass = mail.Attributes["pass"].Value;

			return m;
		}

		private static MailServerDaemon ParseMailDaemon(XmlNode mailNode) {
			string mailName = mailNode.Attributes["name"].Value;
			string mailColor = mailNode.Attributes["color"].Value;
			Boolean.TryParse(mailNode.Attributes["generateJunk"].Value, out bool mailGenJunk);

			MailServerDaemon mailDaemon = new MailServerDaemon(mailName) {
				color = mailColor,
				generateJunk = mailGenJunk
			};

			foreach(XmlNode emailNode in mailNode.SelectNodes("email")) {
				mailDaemon.emails.Add(new Email {
					recipient = emailNode.Attributes["recipient"].Value,
					sender = emailNode.Attributes["sender"].Value,
					subject = emailNode.Attributes["subject"].Value,
					content = emailNode.InnerText
				});
			}

			return mailDaemon;
		}

		private static UploadServerDaemon ParseUploadDaemon(XmlNode uploadNode) {
			Boolean.TryParse(uploadNode.Attributes["needsAuth"].Value, out bool nodeNeedsAuth);
			UploadServerDaemon uploadDaemon = new UploadServerDaemon(
				uploadNode.Attributes["name"].Value,
				uploadNode.Attributes["folder"].Value,
				nodeNeedsAuth,
				uploadNode.Attributes["color"].Value
				      );

			return uploadDaemon;
		}

		private static WebServerDaemon ParseWebServerDaemon(XmlNode webServerNode) {
			WebServerDaemon webServerDaemon = new WebServerDaemon(
				webServerNode.Attributes["name"].Value,
				webServerNode.Attributes["url"].Value
			);

			return webServerDaemon;
		}

		private static MessageBoardDaemon ParseMessageBoardDaemon(XmlNode daemon) {
			MessageBoardDaemon msgBoardDaemon = new MessageBoardDaemon(daemon.Attributes["name"].Value);
			XmlNodeList threadList = daemon.SelectNodes("thread");
			foreach(XmlNode threadNode in threadList)
				msgBoardDaemon.Threads.Add(new Thread { content = threadNode.InnerText });

			return msgBoardDaemon;
		}

		private static VariableMissionListingServer ParseVariableMissionListingServer(XmlNode daemon) {
			string name = daemon.Attributes["name"].Value;
			string iconPath = daemon.Attributes["iconPath"].Value;
			string articleFolderPath = daemon.Attributes["articleFolderPath"].Value;
			string color = daemon.Attributes["color"].Value;
			string title = daemon.Attributes["title"].Value;

			Boolean.TryParse(daemon.Attributes["assigner"].Value, out bool assigner);
			Boolean.TryParse(daemon.Attributes["public"].Value, out bool isPublic);

			return new VariableMissionListingServer(name, iconPath, articleFolderPath, color,
													assigner, isPublic, title);
		}

		private static MissionHubServer ParseMissionHubServer(XmlNode daemon) {
			string groupName = daemon.Attributes["groupName"].Value;
			string serviceName = daemon.Attributes["serviceName"].Value;
			string missionFolderPath = daemon.Attributes["missionFolderPath"].Value;
			string themeColor = daemon.Attributes["themeColor"].Value;
			string lineColor = daemon.Attributes["lineColor"].Value;
			string backgroundColor = daemon.Attributes["backgroundColor"].Value;

			Boolean.TryParse(daemon.Attributes["allowAbandon"].Value, out bool allowAbandon);

			return new MissionHubServer(groupName, serviceName, missionFolderPath, themeColor,
										lineColor, backgroundColor, allowAbandon);
		}

		private static CreditsDaemon ParseCreditsDaemon(XmlNode daemon) {
			string title = daemon.Attributes["Title"].Value;
			string buttonText = daemon.Attributes["ButtonText"].Value;
			string conditionalActionSetToRunOnButtonPressPath = daemon.Attributes["ConditionalActionSetToRunOnButtonPressPath"].Value;

			return new CreditsDaemon(title, buttonText,
									 conditionalActionSetToRunOnButtonPressPath);
		}

		private static MemoryDumpFile ParseMemDumpFile(XmlNode file) {
			string name = file.Attributes["name"].Value;
			string path = file.Attributes["path"].Value;

			// Memory dump files can only contain
			// a single Memory dump
			XmlNode memoryNode = file.SelectSingleNode("Memory");

			return new MemoryDumpFile(name, path) {
				MemFile = ParseMemory(memoryNode)
			};
		}

		private static Memory ParseMemory(XmlNode memory) {
			List<string> commands = new List<string>();
			List<string> data = new List<string>();
			List<string> images = new List<string>();

			XmlNodeList commandNodes = memory.SelectNodes("Commands");
			foreach(XmlNode commandNode in commandNodes) {
				foreach(XmlNode node in commandNode.SelectNodes("Command")) {
					commands.Add(node.InnerText);
				}
			}

	        XmlNodeList dataNodes = memory.SelectNodes("Data");
            foreach(XmlNode dataNode in dataNodes) {
				foreach(XmlNode node in dataNode.SelectNodes("Block")) {
                    data.Add(node.InnerText);
                }
            }
            
			XmlNodeList imageNodes = memory.SelectNodes("Images");
            foreach(XmlNode dataNode in imageNodes) {
                foreach(XmlNode node in dataNode.SelectNodes("Image")) {
                    data.Add(node.InnerText);
                }
            }

			return new Memory() {
				Commands = commands,
                Data = data,
				Images = images
			};
		}

		private static LogoDaemon ParseLogoDaemon(XmlNode node) {
			string name = node.Attributes["Name"].Value;
			bool showsTitle = Boolean.Parse(node.Attributes["ShowsTitle"].Value);
			string textColor = node.Attributes["TextColor"].Value;
			string logoImagePath = node.Attributes["LogoImagePath"] != null ? node.Attributes["LogoImagePath"].Value : "";
			string message = node.InnerText;

			return new LogoDaemon(name) {
				ShowsTitle = showsTitle,
				TextColor = textColor,
				LogoImagePath = logoImagePath,
				Message = message
			};
		}

		private static LogoCustomConnectDisplayDaemon ParseLCCDD(XmlNode node) {
			string title = node.Attributes["title"].Value;
			string logo = node.Attributes["logo"].Value;
			bool overdrawLogo = Boolean.Parse(node.Attributes["overdrawLogo"].Value);
			string buttonAlignment = node.Attributes["buttonAlignment"].Value;


			return new LogoCustomConnectDisplayDaemon(title) {
				Logo = logo,
				OverdrawLogo = overdrawLogo,
				ButtonAlignment = buttonAlignment
			};
		}

		private static WhitelistAuthenticationDaemon ParseWhitelistAuthenticationDaemon(XmlNode node) {
			bool selfAuthenticating = false;
			string remote = "";
			WhitelistType type;
			if(node.Attributes["SelfAuthenticating"] != null) {
				type = WhitelistType.SELF_AUTHENTICATING;
				selfAuthenticating = Boolean.Parse(node.Attributes["SelfAuthenticating"].Value);
			} else {
				type = WhitelistType.REMOTE;
				remote = node.Attributes["Remote"].Value;
			}
			if(type == WhitelistType.REMOTE) {
				return new WhitelistAuthenticationDaemon() {
					Type = type,
					Remote = remote
				};
			} else {
				return new WhitelistAuthenticationDaemon() {
					Type = type,
					SelfAuthenticating = selfAuthenticating
				};
		    }
		}

		private static IRCDaemon ParseIRCDaemon(XmlNode node) {
			IRCDaemon daemon = new IRCDaemon();

			foreach(XmlNode userNode in node.SelectNodes("user")) {
				User user;
				user.name = userNode.Attributes["name"].Value;
				user.color = userNode.Attributes["color"].Value;
				daemon.Users.Add(user);
			}

			foreach(XmlNode postNode in node.SelectNodes("post")) {
				Post post;
				post.user = postNode.Attributes["user"].Value;
				post.message = postNode.InnerText;
				daemon.Posts.Add(post);
			}
            
			return daemon;
		}

		private static DHSDaemon ParseDHSDaemon(XmlNode node) {
			string groupName = node.Attributes["groupName"].Value;
			bool addsFactionPointOnMissionComplete = Boolean.Parse(node.Attributes["addsFactionPointOnMissionComplete"].Value);
			bool autoClearMissionsOnPlayerComplete = Boolean.Parse(node.Attributes["autoClearMissionsOnPlayerComplete"].Value);
			string themeColor = node.Attributes["themeColor"].Value;
			bool allowContractAbbandon = Boolean.Parse(node.Attributes["allowContractAbbandon"].Value);

			List<Agent> agents = new List<Agent>();
			foreach(XmlNode agentNode in node.SelectNodes("agent")) {
				Agent agent;
				agent.name = agentNode.Attributes["name"].Value;
				agent.pass = agentNode.Attributes["pass"].Value;
				agent.color = agentNode.Attributes["color"].Value;
			}

			return new DHSDaemon() {
				GroupName = groupName,
				AddsFactionPointOnMissionComplete = addsFactionPointOnMissionComplete,
				AutoClearMissionsOnPlayerComplete = autoClearMissionsOnPlayerComplete,
				ThemeColor = themeColor,
				AllowContractAbbandon = allowContractAbbandon,
				Agents = agents
		    };
		}
	}
}