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
using log4net;

namespace SkypeBot.plugins {
    public class SamplePlugin : Plugin {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override String name() { return "Just a sample plugin"; }

        public override String help() { return null; }

        public override String description() { return "Does nothing."; }

        public override bool canConfig() { return false; }
        public override void openConfig() { }

        public SamplePlugin() {
        }

        public override void load() {
            log.Info("Plugin successfully loaded.");
        }

        public override void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public override void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            // Your code goes here. Yay!
        }

    }
}   