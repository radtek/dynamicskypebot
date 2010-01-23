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
using SkypeBot.plugins.config.maze;
using SkypeBot.plugins.maze.model;
using SkypeBot.plugins.maze.control;

namespace SkypeBot.plugins {
    public class MazePlugin : Plugin {
        private MazeController control;

        public event MessageDelegate onMessage;

        public String name() { return "Maze Plugin"; }

        public String help() { return null; }

        public String description() { return "Allows for maze exploration."; }

        public bool canConfig() { return true; }
        public void openConfig() {
            MazeConfigForm mcf = new MazeConfigForm(control);
            mcf.Visible = true;
        }

        public MazePlugin() {
            control = new MazeController();
        }

        public void load() {
            logMessage("Plugin successfully loaded.", false);
        }

        public void unload() {
            logMessage("Plugin successfully unloaded.", false);
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!maze (north|south|east|west)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (output.Success) {
                String cmd = output.Groups[1].Value.ToLower();

                Direction dir = Direction.FromString(cmd);
                if (control.Walker.CanWalk(dir))
                    control.Walker.Walk(dir);
            }
        }

        private void logMessage(String msg, Boolean isError) {
            if (onMessage != null)
                onMessage(this.name(), msg, isError);
        }
    }
}   