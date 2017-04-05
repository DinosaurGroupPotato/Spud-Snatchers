using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpudSnatch.Model.Serialization;

namespace SpudSnatch.Model.Objects
{
    public class Homer: Character, Serialized
    {
        public void AddToObjects()
        {
            GameController.level.player = this;
        }
        public string Serialize()
        {
            string data = "hm" + Convert.ToString(positionX) + "," + Convert.ToString(positionY);
            return data;
        }

        public static void Deserialize(string[] line)
        {
            Homer ida = new Homer(Convert.ToInt32(line[1]), Convert.ToInt32(line[2]));
            //GameController.homer = ida;
        }

        public Homer(int x, int y)
        {
            positionX = x;
            positionY = y;
        }

        public Homer(){ }
        public void Jump()
        {
            for (int i = 0; i < 6; i++)
            {
                positionY += i;
            }
            for (int ii = 0; ii < 6; ii++)
            {
                positionY -= ii;
            }
        }

        public void Walk(string direction)
        {
            //Walking left
            if (direction == "left")
            {
                positionX += 1;
            }

            //Walking right
            else
            {
                positionX -= 1;
            }
        }
    }
}
