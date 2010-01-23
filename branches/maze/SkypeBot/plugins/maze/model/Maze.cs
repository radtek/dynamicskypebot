using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkypeBot.plugins.maze.model {
    [Serializable]
    public class Maze : IEnumerable<MazeCell> {
        private MazeCell[,] cells;
        private int width, height;

        public int Width {
            get { return width; }
        }

        public int Height {
            get { return height; }
        }

        public MazeCell this[int x, int y] {
            get { return cells[x, y]; }
        }

        internal Maze(int width, int height) {
            this.width = width;
            this.height = height;

            cells = new MazeCell[width, height];

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    cells[x, y] = new MazeCell(x, y, true);
                    cells[x, y].OnChange += (obj) => {
                        if (this.OnChange != null)
                            this.OnChange(this);
                    };
                }
            }

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    if (x > 0)
                        cells[x, y][Direction.WEST].OtherSide = cells[x - 1, y];
                    if (x < width - 1)
                        cells[x, y][Direction.EAST].OtherSide = cells[x + 1, y];
                    if (y > 0)
                        cells[x, y][Direction.NORTH].OtherSide = cells[x, y - 1];
                    if (y < height - 1)
                        cells[x, y][Direction.SOUTH].OtherSide = cells[x, y + 1];
                }
            }
        }

        #region IEnumerable<MazeCell> Members

        public IEnumerator<MazeCell> GetEnumerator() {
            return cells.Cast<MazeCell>().GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return cells.GetEnumerator();
        }

        #endregion

        public delegate void ChangeHandler(object sender);
        public event ChangeHandler OnChange;
    }
}
