using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpudSnatch.Model.Serialization;
using SpudSnatch.Model.Objects;

namespace SpudSnatch.Model
{
    public class GameController: Serialized
    {
        /// <summary>
        /// merely for tracking level progress, highscroes can go here for now.
        /// </summary>

        public Dictionary<string, int> scores;
        public static int levelProgress = 1;
        public static int score = 0;
        private bool GameOver = false;

        public Level Level { get; set; }

        public GameController()
        {
            Level = new Level();
        }

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
            Level.potatoes = new List<Potato>();
            Level.enemies = new List<Character>();
            Level.obstacles = new List<Obstacle>();
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
