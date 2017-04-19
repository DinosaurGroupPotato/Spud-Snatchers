using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpudSnatch.Model.Serialization;

namespace SpudSnatch.Model.Objects
{
    class PlatformObstacle: Obstacle
    {

        public PlatformObstacle(int x, int y, int width, int height)
        {
            ID = nextID;
            nextID++;
            positionX = x;
            positionY = y;
            Width = width;
            Height = height;
        }

        public int CheckWidth(PlatformObstacle platform, string direction)
        {
            int extremis = 0;
            if (direction == "left")
            {
                extremis = platform.positionX + 50;
                return extremis;
            }
            else
            {
                extremis = platform.positionX - 50;
                return extremis;
            }
        }

    }
}
