using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SkypeBot.plugins.maze;
using System.Windows.Forms;
using System.Drawing;
using SkypeBot.plugins.maze.control;
using SkypeBot.plugins.maze.model;

namespace SkypeBot.plugins.config.maze {
    class MazePanel : Panel {
        private const int CELL_SIZE = 14;
        private MazeController control;

        private static class MazeColors {
            public static readonly Brush SEEN   = Brushes.White,
                                         UNSEEN = Brushes.Black,
                                         STAIRS = Brushes.PeachPuff;

            public static readonly Pen WALL = Pens.DodgerBlue;
        }

        public MazePanel(MazeController control) {
            this.Width = CELL_SIZE * control.Maze.Width;
            this.Height = CELL_SIZE * control.Maze.Height;
            this.Margin = new Padding(0);
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);


            this.control = control;
            control.OnChange += new MazeController.ChangeHandler(maze_OnChange);
        }

        private delegate void RefreshCallback();

        private void maze_OnChange(object sender) {
            if (this.InvokeRequired) {
                RefreshCallback c = new RefreshCallback(this.Refresh);
                this.Invoke(c, new object[] { });
            } else
                this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs pe) {
            redrawCells(pe.Graphics);
        }


        private void redrawCells(Graphics canvas) {
            Maze maze = control.Maze;

            canvas.FillRectangle(MazeColors.UNSEEN, ClientRectangle);

            foreach (MazeCell cell in maze) {
                int w = CELL_SIZE;
                int x = cell.X * w, y = cell.Y * w;

                Rectangle cellRect = new Rectangle(x, y, w, w);

                if (cell.Seen) {
                    canvas.FillRectangle(cell.HasDown ? MazeColors.STAIRS : MazeColors.SEEN, cellRect);
                }
            }

            foreach (MazeCell cell in maze) {
                int w = CELL_SIZE;
                int x = cell.X * w, y = cell.Y * w;
                Point topLeft = new Point(x, y),
                      bottomLeft = new Point(x, y + w - 1),
                      topRight = new Point(x + w - 1, y),
                      bottomRight = new Point(x + w - 1, y + w - 1);

                if (cell[Direction.WEST].Wall && (cell.Seen || (cell[Direction.WEST].OtherSide != null && cell[Direction.WEST].OtherSide.Seen)))
                    canvas.DrawLine(MazeColors.WALL, topLeft, bottomLeft);

                if (cell[Direction.EAST].Wall && (cell.Seen || (cell[Direction.EAST].OtherSide != null && cell[Direction.EAST].OtherSide.Seen)))
                    canvas.DrawLine(MazeColors.WALL, topRight, bottomRight);

                if (cell[Direction.NORTH].Wall && (cell.Seen || (cell[Direction.NORTH].OtherSide != null && cell[Direction.NORTH].OtherSide.Seen)))
                    canvas.DrawLine(MazeColors.WALL, topLeft, topRight);

                if (cell[Direction.SOUTH].Wall && (cell.Seen || (cell[Direction.SOUTH].OtherSide != null && cell[Direction.SOUTH].OtherSide.Seen)))
                    canvas.DrawLine(MazeColors.WALL, bottomLeft, bottomRight);

                if (cell.Seen && cell.HasDown) {
                    canvas.DrawImage(MazeResources.arrow_fat_down, topLeft.X + 2, topLeft.Y + 2);
                }
            }

            MazeCell walkerCell = control.Walker.Position;
            canvas.DrawImage(MazeResources.person, walkerCell.X * CELL_SIZE + 2, walkerCell.Y * CELL_SIZE + 2);
        }

    }
}
