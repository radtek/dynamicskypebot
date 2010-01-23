using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SKYPE4COMLib;

namespace SkypeBot.plugins {
    public delegate void MessageDelegate(String sender, String msg, Boolean isError);

    public interface Plugin {
        /// <returns>The name of the plugin, as displayed in the plugin list.</returns>
        String name();
        /// <returns>Description of the plugin, as displayed in the config window.</returns>
        String description();
        /// <returns>The help text displayed on !help. null if no trigger exists.</returns>
        String help();

        /// <returns>True iff openConfig opens a configuration window for this plugin.</returns>
        bool canConfig();
        /// <summary>
        /// Open a configuration window for this plugin.
        /// Will not be called if canConfig() is false.
        /// </summary>
        void openConfig();

        /// <summary>
        /// Perform whatever is nescessary to load the plugin
        /// </summary>
        void load();
        /// <summary>
        /// Undo whatever load() does.
        /// </summary>
        void unload();
        
        /// <summary>
        /// Passes on Skype messages
        /// </summary>
        void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status);

        /// <summary>
        /// Output-handler for the configuration display.
        /// </summary>
        event MessageDelegate onMessage;
    }
}
