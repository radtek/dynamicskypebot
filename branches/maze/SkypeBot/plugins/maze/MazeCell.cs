using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkypeBot.plugins.maze {
    [Serializable]
    public class MazeCell {
        private MazeLink north, west, south, east;
        private int x, y;

        private bool hasDown, seen;

        public int X {
            get { return x; }
        }

        public int Y {
            get { return y; }
        }

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

        public MazeCell(int x, int y, bool walled) {
            this.x = x;
            this.y = y;

            north = new MazeLink(walled);
            west = new MazeLink(walled);
            east = new MazeLink(walled);
            south = new MazeLink(walled);

            hasDown = false;
            seen = false;
        }

    }
}
