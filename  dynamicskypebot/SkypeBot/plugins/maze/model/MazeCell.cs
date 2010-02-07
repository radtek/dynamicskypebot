using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkypeBot.plugins.maze.model {
    [Serializable]
    public class MazeCell {
        private MazeLink[] exits;
        private int x, y;

        private bool hasDown, seen;

        public int X {
            get { return x; }
        }

        public int Y {
            get { return y; }
        }

        public MazeLink this[Direction dir] {
            get { return exits[dir.AsInt]; }
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

            exits = new MazeLink[4];

            foreach (Direction dir in Direction.Values) {
                exits[dir.AsInt] = new MazeLink(dir, walled);
            }

            hasDown = false;
            seen = false;
        }

        public delegate void ChangeHandler(object sender);
        public event ChangeHandler OnChange;
    }
}
