using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkypeBot.plugins.maze {
    public class MazeFactory {
        private static readonly Random random = new Random();

        private MazeFactory() { }

        public static Maze MakeMaze(int width, int height, int depth) {
            Maze maze = new Maze(width, height);

            return maze;
        }
    }
}
