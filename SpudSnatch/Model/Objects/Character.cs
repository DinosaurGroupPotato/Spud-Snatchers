using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpudSnatch.Model.Serialization;

namespace SpudSnatch.Model.Objects
{

    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    public class Character
    {

        public static int nextID = 1;

        public int ID;

        // Property  and instance variables for the position
        private int positionX;
        public int PositionX { get { return positionX; } set { positionX = value; } }
        private int positionY;
        public int PositionY { get { return positionY; } set { positionY = value; } }
        public Direction playerDirection;
        
    }
}
