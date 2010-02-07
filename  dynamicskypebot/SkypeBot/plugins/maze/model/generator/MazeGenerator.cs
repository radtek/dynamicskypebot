using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkypeBot.plugins.maze.model.generator {
    abstract class MazeGenerator {
        internal static Random random;

        public abstract void Generate(Maze maze, int depth);
    }
}
