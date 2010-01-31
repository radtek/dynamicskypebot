using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkypeBot.plugins.maze.model {
    [Serializable]
    public class Direction {
        public static readonly Direction NORTH = new Direction("North");
        public static readonly Direction SOUTH = new Direction("South");
        public static readonly Direction EAST = new Direction("East");
        public static readonly Direction WEST = new Direction("West");

        private String name;

        private Direction() { }

        private Direction(String name) {
            this.name = name;
        }

        public static IEnumerable<Direction> Values {
            get {
                yield return NORTH;
                yield return WEST;
                yield return EAST;
                yield return SOUTH;
            }
        }

        public static int ToInt(Direction dir) {
            return dir == NORTH ? 0 :
                   dir ==  WEST ? 1 :
                   dir ==  EAST ? 2 :
                   dir == SOUTH ? 3 : -1;
        }

        public int AsInt {
            get { return ToInt(this); }
        }
        public Direction Reverse {
            get {
                return this == NORTH ? SOUTH :
                       this == WEST ? EAST :
                       this == EAST ? WEST :
                       this == SOUTH ? NORTH : null;
            }
        }

        public override string ToString() {
            return name;
        }

        public static Direction FromString(String name) {
            name = name.ToLower();
            return name == "north" ? NORTH :
                   name == "n" ? NORTH :
                   name == "south" ? SOUTH :
                   name == "s" ? SOUTH :
                   name == "west" ? WEST :
                   name == "w" ? WEST :
                   name == "east" ? EAST : 
                   name == "e" ? EAST : null;
        }
    }

}
