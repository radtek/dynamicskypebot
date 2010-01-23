using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SkypeBot.plugins.maze;
using SkypeBot.plugins.maze.control;

namespace SkypeBot.plugins.config.maze {
    public class MazeConfigForm : Form {
        public MazeConfigForm(MazeController control) {
            MazePanel panel = new MazePanel(control);

            this.Padding = new Padding(0);
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.AutoSize = true;

            this.Controls.Add(panel);           
        }
    }
}
