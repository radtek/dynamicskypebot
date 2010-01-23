using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkypeBot.plugins.maze.model;

namespace SkypeBot.plugins.maze.control {
    [Serializable]
    public class MazeController {
        private Maze maze;
        private MazeWalker walker;
        private readonly static Random random = new Random();

        public MazeWalker Walker {
            get { return walker; }
            set { walker = value; }
        }

        public Maze Maze {
            get { return maze; }
        }

        public MazeController() {
            maze = MazeFactory.MakeMaze(30, 30, 1);
            maze.OnChange += new Maze.ChangeHandler(passalongHandler);

            walker = new MazeWalker(maze[random.Next(maze.Width), random.Next(maze.Height)]);
            walker.OnChange += new MazeWalker.ChangeHandler(passalongHandler);
        }

        private void passalongHandler(object sender) {
            if (this.OnChange != null)
                this.OnChange(this);
        }

        public delegate void ChangeHandler(object sender);
        public event ChangeHandler OnChange;
    }
}
