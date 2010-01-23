using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkypeBot.plugins.maze {
    [Serializable]
    class MazeCell {
        private MazeLink north, west, south, east;

        private bool hasDown, seen;

        public MazeLink North {
            get { return north; }
            set { north = value; }
        }

        public MazeLink West {
            get { return west; }
            set { west = value; }
        }

        public MazeLink South {
            get { return south; }
            set { south = value; }
        }

        public MazeLink East {
            get { return east; }
            set { east = value; }
        }

        public Boolean HasDown {
            get { return hasDown; }
            set { hasDown = value; }
        }

        public Boolean Seen {
            get { return seen; }
            set { seen = value; }
        }

        public MazeCell(bool walled) {
            north = new MazeLink(walled);
            west = new MazeLink(walled);
            east = new MazeLink(walled);
            south = new MazeLink(walled);

            hasDown = false;
            seen = false;
        }

    }
}
