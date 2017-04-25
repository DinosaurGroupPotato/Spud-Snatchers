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
            int x1, x2, y1, y2, w1, w2, h1, h2;
            x1 = chr.PositionX;
            y1 = chr.PositionY;
            w1 = chr.Width;
            h1 = chr.Height;
            x2 = PositionX;
            y2 = PositionY;
            w2 = Width;
            h2 = Height;
            if (x1 >= x2 && x1 <= x2 - w2 && y1 >= y2 && y1 <= y2 - h2 / 2)
                return true;
            if (x1 + w1 >= x2 && x1 - w1 <= x2 - w2 && y1 >= y2 && y1 <= y2 - h2 / 2)
                return true;
            if (x1 >= x2 && x1 <= x2 - w2 && y1 + h1 / 2 >= y2 && y1 + h1 / 2 <= y2 - h2 / 2)
                return true;
            if (x1 + w1 >= x2 && x1 - w1 <= x2 - w2 && y1 + h1 / 2 >= y2 && y1 - h1 / 2 <= y2 - h2 / 2)
                return true;
            if (x2 >= x1 && x2 <= x1 - w1 && y2 >= y1 && y2 <= y1 - h1 / 2)
                return true;
            if (x2 + w2 >= x1 && x2 - w2 <= x1 - w1 && y2 >= y1 && y2 <= y1 - h1 / 2)
                return true;
            if (x2 >= x1 && x2 <= x1 - w1 && y2 + h2 / 2 >= y1 && y2 - h2 / 2 <= y1 - h1 / 2)
                return true;
            if (x2 + w2 >= x1 && x2 - w2 <= x1 - w1 && y2 + h2 / 2 >= y1 && y2 - h2 / 2 <= y1 - h1 / 2)
                return true;
            return false;
        }

        // Checks for collision with a obstacle
        public bool IsCollidedObs(Obstacle obs)
        {
            int x1, x2, y1, y2, w1, w2, h1, h2;
            x1 = obs.PositionX;
            y1 = obs.PositionY;
            w1 = obs.Width;
            h1 = obs.Height;
            x2 = PositionX;
            y2 = PositionY;
            w2 = Width;
            h2 = Height;
            if (x1 >= x2 && x1 <= x2 - w2 && y1 >= y2 && y1 <= y2 - h2 / 2)
                return true;
            if (x1 + w1 >= x2 && x1 - w1 <= x2 - w2 && y1 >= y2 && y1 <= y2 - h2 / 2)
                return true;
            if (x1 >= x2 && x1 <= x2 - w2 && y1 + h1 / 2 >= y2 && y1 + h1 / 2 <= y2 - h2 / 2)
                return true;
            if (x1 + w1 >= x2 && x1 - w1 <= x2 - w2 && y1 + h1 / 2 >= y2 && y1 - h1 / 2 <= y2 - h2 / 2)
                return true;
            if (x2 >= x1 && x2 <= x1 - w1 && y2 >= y1 && y2 <= y1 - h1 / 2)
                return true;
            if (x2 + w2 >= x1 && x2 - w2 <= x1 - w1 && y2 >= y1 && y2 <= y1 - h1 / 2)
                return true;
            if (x2 >= x1 && x2 <= x1 - w1 && y2 + h2 / 2 >= y1 && y2 - h2 / 2 <= y1 - h1 / 2)
                return true;
            if (x2 + w2 >= x1 && x2 - w2 <= x1 - w1 && y2 + h2 / 2 >= y1 && y2 - h2 / 2 <= y1 - h1 / 2)
                return true;
            return false;
        }
        
    }
}
