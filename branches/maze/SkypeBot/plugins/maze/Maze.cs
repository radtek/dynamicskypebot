using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkypeBot.plugins.maze {
    [Serializable]
    class Maze {
        private MazeCell[,] cells;

        protected Maze(int width, int height) {
            cells = new MazeCell[width, height];

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    cells[x, y] = new MazeCell(true);
                }
            }

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    if (x > 0)
                        cells[x, y].West.OtherSide = cells[x - 1, y];
                    if (x < width - 1)
                        cells[x, y].East.OtherSide = cells[x + 1, y];
                    if (y > 0)
                        cells[x, y].North.OtherSide = cells[x, y - 1];
                    if (y < height - 1)
                        cells[x, y].South.OtherSide = cells[x, y + 1];
                }
            }
        }
    }
}
