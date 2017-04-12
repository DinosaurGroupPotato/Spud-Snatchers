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
        int positionX;
        int positionY;

        public PlatformObstacle(int x, int y)
        {
            ID = nextID;
            nextID++;
            positionX = x;
            positionY = y;
        }
    }
}
