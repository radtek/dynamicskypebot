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

namespace SkypeBot.plugins {
    public class SamplePlugin : Plugin {
        public event MessageDelegate onMessage;

        public String name() { return "Just a sample plugin"; }

        public String help() { return null; }

        public String description() { return "Does nothing."; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public SamplePlugin() {
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