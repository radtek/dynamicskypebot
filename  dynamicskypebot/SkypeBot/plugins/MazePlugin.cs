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
using SkypeBot.plugins.maze.view;
using System.Globalization;
using log4net;

namespace SkypeBot.plugins {
    public class MazePlugin : Plugin {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private MazeController control;
        private MazeReporter reporter;

        private enum Command { North, N, East, E, West, W, South, S, Down, Look, Unknown }
        private Command CommandFromString(String str) {
            try {
                return (Command)Enum.Parse(typeof(Command), CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str));
            } catch (ArgumentException) {
                return Command.Unknown;
            }
        }

        public override String name() { return "Maze Plugin (beta)"; }

        public override String help() { return "!maze <north/south/east/west/down/look> (BETA)"; }

        public override String description() { return "Allows for maze exploration."; }

        public override bool canConfig() { return true; }
        public override void openConfig() {
            MazeConfigForm mcf = new MazeConfigForm(control);
            mcf.Visible = true;
        }

        public MazePlugin() {
            control = new MazeController();
            reporter = new MazeReporter(control);
        }

        public override void load() {
            log.Info("Plugin successfully loaded.");
        }

        public override void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public override void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match input = Regex.Match(message.Body, @"^!maze (.+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (input.Success) {
                Command cmd = CommandFromString(input.Groups[1].Value.ToLower());

                log.Debug("Got the command '" + cmd + "'");

                String output = "";

                switch (cmd) {
                    case Command.North:
                    case Command.South:
                    case Command.East:
                    case Command.West:
                    case Command.N:
                    case Command.E:
                    case Command.S:
                    case Command.W:
                        Direction dir = Direction.FromString(cmd.ToString());
                        if (control.Walker.CanWalk(dir)) {
                            control.Walker.Walk(dir);
                            output += reporter.ReportWalk(dir);
                        } else {
                            output += reporter.ReportCannotWalk;
                        }
                        break;
                    case Command.Down:
                        if (control.Walker.Position.HasDown) {
                            control.Descend();
                            output += reporter.ReportDescend;
                        } else {
                            output += reporter.ReportCannotDescend;
                        }
                        break;
                    case Command.Look:
                        output += reporter.ReportLook;
                        break;
                    default:
                        output += "Invalid command!";
                        break;
                }

                message.Chat.SendMessage(output);
            }
        }
    }
}   