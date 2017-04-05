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
        public static List<Character> characters = new List<Character>();
        public static List<Obstacle> obstacles = new List<Obstacle>();

        public virtual void AddToObjects()
        {
            //necessary only for the interface, not for this class
        }
        public virtual string Serialize()
        {
            throw new NotImplementedException();
        }

        public virtual string Deserialize()
        {
            throw new NotImplementedException();
        }

        public virtual void LevelProgressAdvance()
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
