using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpudSnatch.Model.Serialization;
using SpudSnatch.Model.Objects;

namespace SpudSnatch.Model
{
    class GameController: Serialized
    {
        /// <summary>
        /// merely for tracking level progress, highscroes can go here for now.
        /// </summary>

        public Dictionary<string, int> scores;
        public static int levelProgress = 1;
        public static Level game = new Level();
        public static int score = 0;
        public static List<Potato> potatoes = new List<Potato>();
        public static List<Character> enemies = new List<Character>();
        public static List<Obstacle> obstacles = new List<Obstacle>();
        public static Homer homer = new Homer(0,0);

        public virtual void AddToObjects()
        {
            //necessary only for the interface, not for this class
        }
        public static string Serialize()
        {
            string data = "gc" + "," + Convert.ToString(levelProgress) + "," + Convert.ToString(score);
            return data;
        }

        public string LevelSerialize()
        {
            string data = "gl" + "," + Convert.ToString(levelProgress);
            return data;
        }

        public static void Deserialize(string[] line)
        {
            potatoes = new List<Potato>();
            enemies = new List<Character>();
            obstacles = new List<Obstacle>();
            levelProgress = Convert.ToInt32(line[1]);
            score = Convert.ToInt32(line[2]);
        }

        public void LevelProgressAdvance()
        {
            throw new NotImplementedException();
        }

        public static void DisplayHighscores()
        {
            throw new NotImplementedException();
        }

        public void DisplayEnvironment()
        {
            throw new NotImplementedException();
        }

        public static void UpdateScores()
        {
            throw new NotImplementedException();
        }

        public static void IncreaseScore()
        {
            score += 20;
        }
    }
}
