using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpudSnatch.Model.Objects;

namespace SpudSnatch.Model
{
    class Level
    {
        public static Homer player = new Homer(0, 0);
        public static List<Potato> potatoes = new List<Potato>();
        public static List<Character> enemies = new List<Character>();
        public static List<Obstacle> obstacles = new List<Obstacle>();
        public Level()
        {
            //initializes the game
            PlaceObjects();
            PlaceEnemies();
            PlaceHomer();
        }

        public void PlaceObjects()
        {
            //Places platforms, potatoes, and damaging objects
        }

        public void PlaceEnemies()
        {
            //Places enemies

            //Should have some way to check if they're on a platform
        }

        public void PlaceHomer()
        {
            //Place Homer at start location
        }
    }
}