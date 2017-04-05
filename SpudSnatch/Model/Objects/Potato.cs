using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpudSnatch.Model.Serialization;

namespace SpudSnatch.Model.Objects
{
    public class Potato: Serialized
    {
        public int positionX;
        public int positionY;
        public bool retrieved = false;

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
            GameController.level.potatoes.Add(this);
        }

        public string Serialize()
        {
            string data = "po" + Convert.ToString(positionX) + "," + Convert.ToString(positionX) + "," + Convert.ToString(retrieved);
            return data;
        }

        public static void Deserialize(string[] line)
        {
            Potato potato = new Potato(Convert.ToInt32(line[1]), Convert.ToInt32(line[2]));
            potato.retrieved = Convert.ToBoolean(line[3]);
        }

    }
}
