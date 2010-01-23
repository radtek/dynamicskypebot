﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkypeBot.plugins.maze.generator {
    abstract class MazeGenerator {
        protected static readonly Random random = new Random();

        public abstract void Generate(Maze maze, int depth);
    }
}
