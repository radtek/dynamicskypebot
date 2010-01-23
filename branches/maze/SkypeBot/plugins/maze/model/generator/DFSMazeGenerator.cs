using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkypeBot.plugins.maze.model.generator {
    class DFSMazeGenerator : MazeGenerator {
        public static readonly MazeGenerator Instance = new DFSMazeGenerator();

        private DFSMazeGenerator() { }

        public override void Generate(Maze maze, int depth) {
            bool[,] visited = new bool[maze.Width, maze.Height];

            Visit(maze[random.Next(maze.Width), random.Next(maze.Height)], visited);
        }

        private void Visit(MazeCell cell, bool[,] visited) {
            visited[cell.X, cell.Y] = true;

            while (true) {
                List<MazeLink> potentials = new List<MazeLink>(
                     from dir in Direction.Values
                     where cell[dir].OtherSide != null && !visited[cell[dir].OtherSide.X, cell[dir].OtherSide.Y]
                     orderby Guid.NewGuid()
                     select cell[dir]
                );

                if (potentials.Count == 0)
                    return;

                MazeLink link = potentials[0];
                link.Wall = false;
                Visit(link.OtherSide, visited);                
            }
        }
    }
}
