// Enemy.cs
// Holds the state of an enemy object in the game.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpudSnatch.Model.Serialization;

namespace SpudSnatch.Model.Objects
{
    public class Enemy: Character
    {
        //Constructor for enemy
        //Takes initial position of 'x' and 'y'
        //Takes 'width' and 'height' for data points used in collision detection
        public Enemy(int x, int y, int width, int height)
        {
            ID = nextID;
            nextID++;
            PositionX = x;
            PositionY = y;
            Width = width;
            Height = height;
        }

        // Serialization method
        public string Serialize()
        {
            string data = "en," + Convert.ToString(PositionX) + "," + Convert.ToString(PositionY) + "," + Convert.ToString(Width) + "," + Convert.ToString(Height);
            return data;
        }

        // Deserialization method
        public static void Deserialize(string[] line)
        {
            Enemy enemy = new Enemy(Convert.ToInt32(line[1]), Convert.ToInt32(line[2]), Convert.ToInt32(line[3]), Convert.ToInt32(line[4]));
            List<Character> enemies = GameController.Instance.level.GetEnemies();
            enemies.Add(enemy);
        }

        //Jump method for future expansion
        public int[] Jump()
        {
            throw new NotImplementedException();
        }

        //Takes enemy object and number of times to call walk method
        //Randomly selects between walk left and walk right until
        //Walk has been called 'callTime' number of times
        public void Walk(Enemy Phil, int callTime)
        {
            var walker = new Random();
            int direction = walker.Next(2);
            while (callTime != 0)
            {
                if (direction == 0)
                {
                    walkLeft(Phil);
                    callTime--;
                    Walk(Phil, callTime);
                }
                else
                {
                    walkRight(Phil);
                    callTime--;
                    Walk(Phil, callTime);
                }
            }
            
        }

        //Increments enemy position to move enemy to the right
        private void walkRight(Enemy Jack)
        {
            for (int steps = 0; steps < 10; steps++)
            {
                Jack.PositionX += 1;
            }
        }

        //Decrements enemy position to move enemy to the left
        private void walkLeft(Enemy Toledo)
        {
            for (int steps = 0; steps < 10; steps++)
            {
                Toledo.PositionX -= 1;
            }
        }
    }
}
