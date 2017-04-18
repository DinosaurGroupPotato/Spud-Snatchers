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

        public int Width { get; set; }
        public int Height { get; set; }

        public Direction playerDirection;

        // Checks for collision with a character
        public bool IsCollidedChar(Character chr)
        {
            if (chr.PositionX >= PositionX && chr.PositionX <= PositionX + Width && chr.PositionY >= PositionY && chr.PositionY <= PositionY + Height)
                return true;
            if (chr.PositionX + chr.Width >= PositionX && chr.PositionX + chr.Width <= PositionX + Width && chr.PositionY >= PositionY && chr.PositionY <= PositionY + Height)
                return true;
            if (chr.PositionX >= PositionX && chr.PositionX <= PositionX + Width && chr.PositionY + chr.Height >= PositionY && chr.PositionY + chr.Height <= PositionY + Height)
                return true;
            if (chr.PositionX + chr.Width >= PositionX && chr.PositionX + chr.Width <= PositionX + Width && chr.PositionY + chr.Height >= PositionY && chr.PositionY + chr.Height <= PositionY + Height)
                return true;
            return false;
        }

        // Checks for collision with a obstacle
        public bool IsCollidedObs(Obstacle obs)
        {
            if (obs.PositionX >= PositionX && obs.PositionX <= PositionX + Width && obs.PositionY >= PositionY && obs.PositionY <= PositionY + Height)
                return true;
            if (obs.PositionX + obs.Width >= PositionX && obs.PositionX + obs.Width <= PositionX + Width && obs.PositionY >= PositionY && obs.PositionY <= PositionY + Height)
                return true;
            if (obs.PositionX >= PositionX && obs.PositionX <= PositionX + Width && obs.PositionY + obs.Height >= PositionY && obs.PositionY + obs.Height <= PositionY + Height)
                return true;
            if (obs.PositionX + obs.Width >= PositionX && obs.PositionX + obs.Width <= PositionX + Width && obs.PositionY + obs.Height >= PositionY && obs.PositionY + obs.Height <= PositionY + Height)
                return true;
            return false;
        }
        
    }
}
