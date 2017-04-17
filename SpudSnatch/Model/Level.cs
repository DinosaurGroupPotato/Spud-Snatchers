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
        public Homer player = new Homer(0, 0);
        public List<Potato> potatoes = new List<Potato>();
        public List<Potato> collectedPotatoes = new List<Potato>();
        public List<Character> enemies = new List<Character>();
        public List<Obstacle> obstacles = new List<Obstacle>();
        private int floor = 375;

        public Level()
        {
            //initializes the game
            PlaceObjects();
            PlaceEnemies();
        }

        public Homer GetHomer()
        {
            return player;
        }

        public int GetFloor()
        {
            return floor;
        }

        public string GetPlayerState()
        {
            string state = "";
            if (player.State == Homer.HomerState.Jumping)
            {
                 state = "jumping";
                return state;
            }
            else if (player.State == Homer.HomerState.Ducking)
            {
                 state = "ducking";
                return state;
            }
            else
            {
                 state = "standing";
                return state;
            }
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
            potatoes.Add(new Potato(150,0));
            //Places platforms, potatoes, and damaging objects
            var rand = new Random();
            var neg = new Random();
            for (int pot = 0; pot < 20; pot++)
            {
                int check_polarity = neg.Next(4);
                if (check_polarity == 0)
                {
                    potatoes.Add(new Potato(rand.Next(250), rand.Next(550)));
                }
                else if (check_polarity == 1)
                {
                    potatoes.Add(new Potato(rand.Next(250), rand.Next(250) * -1));
                }
                else if (check_polarity == 2)
                {
                    potatoes.Add(new Potato(rand.Next(250) * -1, rand.Next(250)));
                }
                else 
                {
                    potatoes.Add(new Potato(rand.Next(250) * -1, rand.Next(250) * -1));
                }
            }

            obstacles.Add(new PlatformObstacle(150,-200));
            obstacles.Add(new PlatformObstacle(-300, -150));
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
                    enemies.Add(new Enemy(rand.Next(250), rand.Next(250)));
                }
                else if (check_polarity == 1)
                {
                    enemies.Add(new Enemy(rand.Next(250), rand.Next(250) * -1));
                }
                else if (check_polarity == 2)
                {
                    enemies.Add(new Enemy(rand.Next(250) * -1, rand.Next(250)));
                }
                else
                {
                    enemies.Add(new Enemy(rand.Next(250) * -1, rand.Next(250) * -1));
                }
            }
            for (int pot = 0; pot < potatoes.Count(); pot += 3)
            {
                if(pot < potatoes.Count())
                {
                    potatoes[pot].big = true;
                }
            }
            for (int pot = 0; pot < potatoes.Count(); pot += 8)
            {
                if (pot < potatoes.Count())
                {
                    potatoes[pot].poisoned = true;
                }
            }

            //Should have some way to check if they're on a platform
        }

        public int ReturnPlayerPosition(string partial, Homer homer)
        {
            int partialcoordinate;
            int[] fullcoordinates;

            fullcoordinates = homer.GetLocation();

            if (partial == "x")
            {
                partialcoordinate = fullcoordinates[0];
                return partialcoordinate;
            }

            else
            {
                partialcoordinate = fullcoordinates[1];
                return partialcoordinate;
            }
        }

    }
}