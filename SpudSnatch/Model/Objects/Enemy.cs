﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpudSnatch.Model.Serialization;

namespace SpudSnatch.Model.Objects
{
    public class Enemy: Character, Serialized
    {

        public Enemy(int x, int y)
        {
            positionX = x;
            positionY = y;
        }
        public void AddToObjects()
        {
            GameController.level.enemies.Add(this);
        }
        public string Serialize()
        {
            string data = "en" + Convert.ToString(positionX) + "," + Convert.ToString(positionY);
            return data;
        }

        public static void Deserialize(string[] line)
        {
            Enemy enemy = new Enemy(Convert.ToInt32(line[1]), Convert.ToInt32(line[2]));
            enemy.AddToObjects();
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
