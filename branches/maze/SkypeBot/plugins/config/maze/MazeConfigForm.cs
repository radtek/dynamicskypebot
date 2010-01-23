using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SkypeBot.plugins.maze;

namespace SkypeBot.plugins.config.maze {
    public class MazeConfigForm : Form {
        public MazeConfigForm(Maze maze) {
            MazePanel panel = new MazePanel(maze);

            this.Padding = new Padding(0);
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.AutoSize = true;

            this.Controls.Add(panel);           
        }
    }
}
