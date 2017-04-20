﻿using System;
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

        public Potato(int x,int y, int width, int height)
        {
            ID = nextID;
            nextID++;
            PositionX = x;
            PositionY = y;
            Width = width;
            Height = height;
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
            string data = "po," + Convert.ToString(PositionX) + "," + Convert.ToString(PositionX) + "," + Convert.ToString(Width) + "," + Convert.ToString(Height) + "," + Convert.ToString(Retrieved);
            return data;
        }

        public static void Deserialize(string[] line)
        {
            Potato potato = new Potato(Convert.ToInt32(line[1]), Convert.ToInt32(line[2]), Convert.ToInt32(line[3]), Convert.ToInt32(line[4]));
            potato.Retrieved = Convert.ToBoolean(line[5]);
            List<Potato> potatoes = GameController.Instance.level.GetPotatoes();
            potatoes.Add(potato);
        }

    }
}
