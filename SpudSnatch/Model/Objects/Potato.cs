using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpudSnatch.Model.Serialization;

namespace SpudSnatch.Model.Objects
{
    public enum PotatoState
    {
        Small,
        Big,
        SmallPoisoned,
        BigPoisoned
        
    }

    public class Potato : Character
    {

        public PotatoState State;
        public bool Retrieved = false;

        public Potato(int x,int y)
        {
            ID = nextID;
            nextID++;
            PositionX = x;
            PositionY = y;
        }

        public void CollectPotato()
        {
            if(!Retrieved)
            {
                Retrieved = true;
                GameController.Instance.IncreaseScore(State);
            }
            
        }
        public string Serialize()
        {
            string data = "po" + Convert.ToString(PositionX) + "," + Convert.ToString(PositionX) + "," + Convert.ToString(Retrieved);
            return data;
        }

        public static void Deserialize(string[] line)
        {
            Potato potato = new Potato(Convert.ToInt32(line[1]), Convert.ToInt32(line[2]));
            potato.Retrieved = Convert.ToBoolean(line[3]);
            List<Potato> potatoes = GameController.Instance.level.GetPotatoes();
            potatoes.Add(potato);
        }

    }
}
