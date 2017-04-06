using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpudSnatch.Model.Serialization;

namespace SpudSnatch.Model.Objects
{
    public class Character
    {
        public int positionX;
        public int positionY;

        public int[] GetLocation()
        {
            int[] position = new int[2];
            position[0] = positionX;
            position[1] = positionY;
            return position;
        }
    }
}
