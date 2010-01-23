using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkypeBot.plugins.maze {
    [Serializable]
    public class MazeLink {
        MazeCell otherSide;
        bool wall;

        public MazeCell OtherSide {
            get { return otherSide; }
            set { otherSide = value; }
        }

        public Boolean Wall {
            get { return wall; }
            set { wall = value; }
        }

        public MazeLink(bool walled) {
            wall = walled;
        }
    }
}
