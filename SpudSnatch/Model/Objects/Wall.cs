using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpudSnatch.Model.Serialization;

namespace SpudSnatch.Model.Objects
{
    class Wall: Obstacle
    {
        public Wall(int x, int y, int width, int height)
        {
            ID = nextID;
            nextID++;
            positionX = x;
            positionY = y;
            Width = width;
            Height = height;
        }
    }
}
