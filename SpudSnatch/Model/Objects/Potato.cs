using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpudSnatch.Model.Serialization;

namespace SpudSnatch.Model.Objects
{
    class Potato: Serialized
    {
        int positionX;
        int positionY;
        bool retrieved = false;

        public Potato(int x,int y)
        {
            positionX = x;
            positionY = y;
        }

        public void CollectPotato()
        {
            if(!retrieved)
            {
                retrieved = true;
                GameController.IncreaseScore();
            }
            
        }

        public void AddToObjects()
        {
            GameController.potatoes.Add(this);
        }

        public string Serialize()
        {
            string data = "88" + Convert.ToString(positionX) + "," + Convert.ToString(positionX) + "," + Convert.ToString(retrieved);
            return data;
        }

        public static void Deserialize(string[] line)
        {
            Potato potato = new Potato(Convert.ToInt32(line[1]), Convert.ToInt32(line[2]));
            potato.retrieved = Convert.ToBoolean(line[3]);
        }

    }
}
