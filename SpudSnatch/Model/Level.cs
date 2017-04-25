// Level.cs
// Contains the code for the level object, holds the state of a in-game level

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

        //Level contstructor
        public Level()
        {
            //initializes the game
            //PlaceObjects();
            //PlaceEnemies();
            
            //Adds four platforms at the given coordinates to the level
            obstacles.Add(new PlatformObstacle(-350, 250, 150, 100));
            obstacles.Add(new PlatformObstacle(300, 250, 150, 100));
            obstacles.Add(new PlatformObstacle(-500, 150, 150, 100));
            obstacles.Add(new PlatformObstacle(450, 150, 150, 100));

            //Adds two enemies at the given coordinates to the level
            enemies.Add(new Enemy(-500, 100, 50, 50));
            enemies.Add(new Enemy(450, 100, 50, 50));

            //Creates RNG
            //Places 30 potatoes according to the whims of the RNG
            var r = new Random();
            for (int i = 0; i < 30; i++)
                potatoes.Add(new Potato(r.Next(-500, 450), r.Next(-100, 375), 20, 20));


        }

        //Returns Player object
        //Player is a Homer object
        public Homer GetHomer()
        {
            return Player;
        }

        //Returns floor, minimum y-position player is allowed to reach
        public int GetFloor()
        {
            return floor;
        }

        //Returns list of potato objects in level
        public List<Potato> GetPotatoes()
        {
            return potatoes;
        }

        //Returns list of enemy objects in level
        public List<Character> GetEnemies()
        {
            return enemies;
        }
        
        //Returns list of obstacle objects in level
        public List<Obstacle> GetObstacles()
        {
            return obstacles;
        }

        //Adds potato and obstacle objects to level lists
        //Creates RNG for said shenanigans
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

        //Adds enemies to the level
        //Creates RNG for shenanigans
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