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
            GameController.characters.Add(this);
        }
        public string Serialize()
        {
            string data = "00" + Convert.ToString(positionX) + "," + Convert.ToString(positionY);
            return data;
        }

        public string Deserialize()
        {
            throw new NotImplementedException();
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
