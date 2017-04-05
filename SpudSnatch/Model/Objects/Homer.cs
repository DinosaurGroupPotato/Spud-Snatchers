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
            GameController.homer = this;
        }
        public string Serialize()
        {
            string data = "hm" + Convert.ToString(positionX) + "," + Convert.ToString(positionY);
            return data;
        }

        public static void Deserialize(string[] line)
        {
            Homer ida = new Homer(Convert.ToInt32(line[1]), Convert.ToInt32(line[2]));
            GameController.homer = ida;
        }

        public Homer(int x, int y)
        {
            positionX = x;
            positionY = y;
        }

        public int[] Jump()
        {
            throw new NotImplementedException();
        }

        public int[] Walk()
        {
            throw new NotImplementedException();
        }
    }
}
