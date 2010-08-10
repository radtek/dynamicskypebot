using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkypeBot.plugins.maze.model {
    [Serializable]
    public class MazeWalker {
        public class YouHitAWallException : Exception { }

        private MazeCell position;

        public MazeCell Position {
            get { return position; }
        }

        public MazeWalker(MazeCell startCell) {
            position = startCell;
            position.Seen = true;
        }

        public bool CanWalk(Direction dir) {
            return !position[dir].Wall;
        }

        public bool Walk(Direction dir) {
            if (!CanWalk(dir))
                return false;

            position = position[dir].OtherSide;
            position.Seen = true;

            if (OnChange != null)
                OnChange(this);

            return true;
        }

        public delegate void ChangeHandler(object sender);
        public event ChangeHandler OnChange;
    }
}
