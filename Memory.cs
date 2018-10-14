using System;
using System.Collections.Generic;

namespace hacknet_viewer {
    /**
     * This is the information that is turned into a memory dump
     * when using MemDumpGenerator.exe
     */
    public class Memory {
        public List<string> Commands { get; set; }
        public List<string> Data { get; set; }
        public List<string> Images { get; set; }

        public Memory() {
            this.Commands = new List<string>();
            this.Data = new List<string>();
            this.Images = new List<string>();
        }

        public override string ToString() {
            string commandsString = "";
            string dataString = "";
            string imagesString = "";

            foreach(string str in Commands) {
                commandsString += str + "\n";
            }

            foreach(string str in Data) {
                dataString += str + "\n";
            }

            foreach(string str in Images) {
                imagesString += str + "\n";
            }

            return "<Memory " +
                "\ncommands: " + commandsString.TrimEnd('\n') +
               "\ndata: " + dataString.TrimEnd('\n') +
               "\nimages: " + imagesString.TrimEnd('\n') +
                ">";
        }
    }
}