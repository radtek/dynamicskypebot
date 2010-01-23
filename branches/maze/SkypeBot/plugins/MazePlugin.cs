using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Windows.Forms;
using SKYPE4COMLib;
using SkypeBot.plugins.maze;
using SkypeBot.plugins.config.maze;

namespace SkypeBot.plugins {
    public class MazePlugin : Plugin {
        private Maze maze;

        public event MessageDelegate onMessage;

        public String name() { return "Maze Plugin"; }

        public String help() { return null; }

        public String description() { return "Allows for maze exploration."; }

        public bool canConfig() { return true; }
        public void openConfig() {
            MazeConfigForm mcf = new MazeConfigForm(maze);
            mcf.Visible = true;
        }

        public MazePlugin() {
            maze = MazeFactory.MakeMaze(30, 30, 1);
        }

        public void load() {
            logMessage("Plugin successfully loaded.", false);
        }

        public void unload() {
            logMessage("Plugin successfully unloaded.", false);
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            // Your code goes here. Yay!
        }

        private void logMessage(String msg, Boolean isError) {
            if (onMessage != null)
                onMessage(this.name(), msg, isError);
        }
    }
}   