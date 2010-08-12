using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SKYPE4COMLib;

namespace SkypeBot.plugins {
    public delegate void MessageDelegate(String sender, String msg, Boolean isError);

    public abstract class Plugin {
        /// <returns>The name of the plugin, as displayed in the plugin list.</returns>
        public abstract String name();
        /// <returns>Description of the plugin, as displayed in the config window.</returns>
        public abstract String description();
        /// <returns>The help text displayed on !help. null if no trigger exists.</returns>
        public abstract String help();

        /// <summary>
        /// Is this plugin an early bird? Early birds get run first.
        /// </summary>
        public virtual bool earlyBird() { return false; }

        /// <returns>True iff openConfig opens a configuration window for this plugin.</returns>
        public virtual bool canConfig() { return false; }
        /// <summary>
        /// Open a configuration window for this plugin.
        /// Will not be called if canConfig() is false.
        /// </summary>
        public virtual void openConfig() { throw new NotImplementedException(); }

        /// <summary>
        /// Perform whatever is nescessary to load the plugin
        /// </summary>
        public abstract void load();
        /// <summary>
        /// Undo whatever load() does.
        /// </summary>
        public abstract void unload();
        
        /// <summary>
        /// Passes on Skype messages
        /// </summary>
        public abstract void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status);
    }
}
