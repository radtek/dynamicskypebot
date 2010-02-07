using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkypeBot.plugins.maze.control;
using SkypeBot.plugins.maze.model;

namespace SkypeBot.plugins.maze.view {
    public class MazeReporter {
        private MazeController control;

        public MazeReporter(MazeController control) {
            this.control = control;
        }

        public String ReportWalk(Direction dir) {
            return String.Format(
                "You walk to the {0}.\n{1}",
                dir, ReportLook
            );
        }

        public String ReportCannotWalk {
            get {
                return "You bang your head against the wall. Well done.";
            }
        }

        public String ReportLook {
            get {
                return ReportStaircase
                     + ReportExits;
            }
        }

        public String ReportStaircase {
            get {
                if (control.Walker.Position.HasDown)
                    return "There happens to be a huge gaping hole in the floor here.\n";
                else
                    return "";
            }
        }

        public String ReportExits {
            get {
                MazeCell room = control.Walker.Position;
                var exits = from dir in Direction.Values
                            where !room[dir].Wall
                            select dir;

                String output = "Exits: ";
                bool first = true;

                foreach (Direction dir in exits) {
                    if (!first) {
                        output += ", ";
                    }
                    first = false;
                    output += dir;
                }
                
                if (room.HasDown) {
                    output += ", Down";
                }

                output += ".";

                return output;
            }
        }

        public String ReportCannotDescend {
            get {
                return "You can't seem to go down here.\n" +
                       "Maybe it has something to do with the solid concrete floor below you.\n" +
                       "We may never know.";
            }
        }

        public String ReportDescend {
            get {
                return "You jump into the gaping hole, landing on your face.\n" +
                    ReportLook;
            }
        }
    }
}
