using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkypeBot.plugins.maze.model {
    [Serializable]
    public class MazeLink {
        MazeCell otherSide;
        bool wall;
        Direction direction;

        public MazeCell OtherSide {
            get { return otherSide; }
            set { otherSide = value; }
        }

        public Boolean Wall {
            get { return wall; }
            set {
                wall = value;
                if (otherSide != null &&
                    otherSide[direction.Reverse].Wall != value) {
                    otherSide[direction.Reverse].Wall = value;
                }

            }
        }

        public Direction Direction {
            get { return direction; }
        }

        public MazeLink(Direction dir, bool walled) {
            direction = dir;
            wall = walled;
        }
    }
}
