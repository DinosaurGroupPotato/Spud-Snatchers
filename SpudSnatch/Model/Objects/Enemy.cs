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

        public Enemy(int x, int y)
        {
            ID = nextID;
            nextID++;
            PositionX = x;
            PositionY = y;
        }
        public string Serialize()
        {
            string data = "en" + Convert.ToString(PositionX) + "," + Convert.ToString(PositionY);
            return data;
        }

        public static void Deserialize(string[] line)
        {
            Enemy enemy = new Enemy(Convert.ToInt32(line[1]), Convert.ToInt32(line[2]));
            List<Character> enemies = GameController.Instance.level.GetEnemies();
            enemies.Add(enemy);
        }
        public int[] Jump()
        {
            throw new NotImplementedException();
        }

        public void Walk(Enemy Phil)
        {
            var walker = new Random();
            int direction = walker.Next(2);

            if (direction == 0)
            {
                walkLeft(Phil);
                Walk(Phil);
            }
            else
            {
                walkRight(Phil);
                Walk(Phil);
            }
            
        }

        private void walkRight(Enemy Jack)
        {
            for (int steps = 0; steps < 10; steps++)
            {
                Jack.PositionX += 1;
            }
        }

        private void walkLeft(Enemy Toledo)
        {
            for (int steps = 0; steps < 10; steps++)
            {
                Toledo.PositionX -= 1;
            }
        }
    }
}
