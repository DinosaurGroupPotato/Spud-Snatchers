using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpudSnatch.Model.Serialization;

namespace SpudSnatch.Model.Objects
{
    public class Potato
    {
        public static int nextID = 1;
        public int ID;

        public int positionX;
        public int positionY;

        public bool retrieved = false;
        public bool big = false;
        public bool poisoned = false;

        public Potato(int x,int y)
        {
            ID = nextID;
            nextID++;
            positionX = x;
            positionY = y;
        }

        public void CollectPotato()
        {
            if(!retrieved)
            {
                retrieved = true;
                GameController.IncreaseScore(big, poisoned);
            }
            
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
            List<Potato> potatoes = GameController.level.GetPotatoes();
            potatoes.Add(potato);
        }

    }
}
