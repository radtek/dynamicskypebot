using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkypeBot.plugins.maze.model;
using SkypeBot.plugins.maze.model.generator;

namespace SkypeBot.plugins.maze.control {
    [Serializable]
    public class MazeController {
        private Maze maze;
        private MazeWalker walker;
        private Random random;
        private int depth;

        public MazeWalker Walker {
            get { return walker; }
            set { walker = value; }
        }

        public Maze Maze {
            get { return maze; }
        }

        public MazeController() {
            random = new Random();
            MazeFactory.random = random;
            MazeGenerator.random = random;

            depth = 0;

            Descend();
        }

        public void Descend() {
            depth += 1;

            if (maze != null)
                maze.OnChange -= new Maze.ChangeHandler(passalongHandler);
            maze = MazeFactory.MakeMaze(30, 30, depth);
            maze.OnChange += new Maze.ChangeHandler(passalongHandler);

            if (walker != null)
                walker.OnChange -= new MazeWalker.ChangeHandler(passalongHandler);
            walker = new MazeWalker(maze[random.Next(maze.Width), random.Next(maze.Height)]);
            walker.OnChange += new MazeWalker.ChangeHandler(passalongHandler);

            if (this.OnChange != null)
                this.OnChange(this); 
        }

        private void passalongHandler(object sender) {
            if (this.OnChange != null)
                this.OnChange(this);
        }

        public delegate void ChangeHandler(object sender);
        public event ChangeHandler OnChange;
    }
}
