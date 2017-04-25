// PlatformObstacle.cs
// A platform for the player to jump on and run around in the game

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
        //Constructor for PlatformObstacles
        //Takes initial positions 'x' and 'y'
        //Takes 'width' and 'height' parameters used for collision detection
        public PlatformObstacle(int x, int y, int width, int height)
        {
            ID = nextID;
            nextID++;
            positionX = x;
            positionY = y;
            Width = width;
            Height = height;
        }

        //Checks horizonatl extremes of 'platform', end checked based on 'direction'
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
