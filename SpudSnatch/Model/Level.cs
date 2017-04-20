using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpudSnatch.Model.Objects;

namespace SpudSnatch.Model
{
    public class Level
    {
        public Homer Player = new Homer(0, 0);
        public List<Potato> potatoes = new List<Potato>();
        public List<Potato> collectedPotatoes = new List<Potato>();
        public List<Character> enemies = new List<Character>();
        public List<Obstacle> obstacles = new List<Obstacle>();
        private int floor = 375;

        public Level()
        {
            //initializes the game
            //PlaceObjects();
            //PlaceEnemies();

            obstacles.Add(new PlatformObstacle(-250, 200, 150, 100));
            obstacles.Add(new PlatformObstacle(200, 115, 150, 100));
            obstacles.Add(new PlatformObstacle(-250, 25, 150, 100));

        }

        public Homer GetHomer()
        {
            return Player;
        }

        public int GetFloor()
        {
            return floor;
        }

        public List<Potato> GetPotatoes()
        {
            return potatoes;
        }

        public List<Character> GetEnemies()
        {
            return enemies;
        }

        public List<Obstacle> GetObstacles()
        {
            return obstacles;
        }

        public void PlaceObjects()
        {
            //Places platforms, potatoes, and damaging objects
            var rand = new Random();
            var neg = new Random();
            for (int pot = 0; pot < 20; pot++)
            {
                int check_polarity = neg.Next(4);
                if (check_polarity == 0)
                {
                    potatoes.Add(new Potato(rand.Next(250), rand.Next(550), 20, 20));
                }
                else if (check_polarity == 1)
                {
                    potatoes.Add(new Potato(rand.Next(250), rand.Next(250) * -1, 20, 20));
                }
                else if (check_polarity == 2)
                {
                    potatoes.Add(new Potato(rand.Next(250) * -1, rand.Next(250), 20, 20));
                }
                else 
                {
                    potatoes.Add(new Potato(rand.Next(250) * -1, rand.Next(250) * -1, 20, 20));
                }
            }

            obstacles.Add(new PlatformObstacle(150,-200, 150, 100));
            obstacles.Add(new PlatformObstacle(-300, -150, 150, 100));
            obstacles.Add(new Wall(-500,-300, 150, 100));
        }

        public void PlaceEnemies()
        {
            //Places enemies
            var rand = new Random();
            var neg = new Random();
            for (int pot = 0; pot < 2; pot++)
            {
                int check_polarity = neg.Next(4);
                if (check_polarity == 0)
                {
                    enemies.Add(new Enemy(rand.Next(250), rand.Next(250), 50, 50));
                }
                else if (check_polarity == 1)
                {
                    enemies.Add(new Enemy(rand.Next(250), rand.Next(250) * -1, 50, 50));
                }
                else if (check_polarity == 2)
                {
                    enemies.Add(new Enemy(rand.Next(250) * -1, rand.Next(250), 50, 50));
                }
                else
                {
                    enemies.Add(new Enemy(rand.Next(250) * -1, rand.Next(250) * -1, 50, 50));
                }
            }
            for (int pot = 0; pot < potatoes.Count(); pot += 3)
            {
                if(pot < potatoes.Count())
                {
                    potatoes[pot].State = PotatoState.Big;
                }
            }
            for (int pot = 0; pot < potatoes.Count(); pot += 8)
            {
                if (pot < potatoes.Count())
                {
                    potatoes[pot].State = PotatoState.SmallPoisoned;
                }
            }

            //Should have some way to check if they're on a platform
        }

    }
}