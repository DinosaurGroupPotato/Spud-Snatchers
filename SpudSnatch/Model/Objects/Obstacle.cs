using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpudSnatch.Model.Objects
{
    public class Obstacle
    {
        public int positionX;
        public int positionY;
        public int[] GetLocation()
        {
            int[] position = new int[2];
            position[1] = positionX;
            position[2] = positionY;
            return position;
        }

        public int ReturnObstaclePosition(string partial, Obstacle obs)
        {
            int partialcoordinate;
            int[] fullcoordinates;

            fullcoordinates = obs.GetLocation();

            if (partial == "x")
            {
                partialcoordinate = fullcoordinates[0];
                return partialcoordinate;
            }

            else
            {
                partialcoordinate = fullcoordinates[1];
                return partialcoordinate;
            }
        }
    }
}
