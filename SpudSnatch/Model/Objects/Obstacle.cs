using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpudSnatch.Model.Objects
{
    public class Obstacle
    {

        public static int nextID = 1;
        public int ID;

        public int positionX;
        public int positionY;

        public int Width { get; set; }
        public int Height { get; set; }

        public int PositionX { get { return positionX; } set { positionX = value; } }
        public int PositionY { get { return positionY; } set { positionY = value; } }

    }
}