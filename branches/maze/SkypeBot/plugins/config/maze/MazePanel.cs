using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SkypeBot.plugins.maze;
using System.Windows.Forms;
using System.Drawing;

namespace SkypeBot.plugins.config.maze {
    class MazePanel : Panel {
        private const int CELL_SIZE = 10;
        private Maze maze;

        public MazePanel(Maze maze) {
            this.Width = CELL_SIZE * maze.Width;
            this.Height = CELL_SIZE * maze.Height;
            this.Margin = new Padding(0);
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);


            this.maze = maze;
            // maze.OnChange += new maze.ChangeHandler(model_OnChange);
        }

        private delegate void RefreshCallback();
        private void model_OnChange(object sender) {
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
            canvas.FillRectangle(Brushes.White, ClientRectangle);
            foreach (MazeCell cell in maze) {
                int w = CELL_SIZE;
                int x = cell.X * w, y = cell.Y * w;
                Point topLeft     = new Point(x, y),
                      bottomLeft  = new Point(x, y + w - 1),
                      topRight    = new Point(x + w - 1, y),
                      bottomRight = new Point(x + w - 1, y + w - 1);



                Rectangle cellRect = new Rectangle(cell.X * CELL_SIZE, cell.Y * CELL_SIZE, CELL_SIZE, CELL_SIZE);
                //if (cell.Seen) {
                    canvas.FillRectangle(Brushes.Yellow, cellRect);

                    if (cell.West.Wall)
                        canvas.DrawLine(Pens.Blue, topLeft, bottomLeft);
                    if (cell.East.Wall)
                        canvas.DrawLine(Pens.Blue, topRight, bottomRight);
                    if (cell.North.Wall)
                        canvas.DrawLine(Pens.Blue, topLeft, topRight);
                    if (cell.South.Wall)
                        canvas.DrawLine(Pens.Blue, bottomLeft, bottomRight);
                /*} else {
                    canvas.FillRectangle(Brushes.Black, cellRect);
                }*/
            }
        }

    }
}
