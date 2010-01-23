using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkypeBot.plugins.maze.model.generator;
using SkypeBot.plugins.maze.control;

namespace SkypeBot.plugins.maze.model {
    public class MazeFactory {
        internal static Random random;

        private MazeFactory() { }

        public static Maze MakeMaze(int width, int height, int depth) {
            Maze maze = new Maze(width, height);

            DFSMazeGenerator.Instance.Generate(maze, depth);

            maze[random.Next(width), random.Next(height)].HasDown = true;

            return maze;
        }
    }
}
