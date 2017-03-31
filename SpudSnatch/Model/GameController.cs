using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpudSnatch.Model.Serialization;

namespace SpudSnatch.Model
{
    class GameController: Serialized
    {
        /// <summary>
        /// merely for tracking level progress, highscroes can go here for now.
        /// </summary>

        public Dictionary<string, int> scores;
        public static int levelProgress = 1;

        public virtual void AddToObjects()
        {
            throw new NotImplementedException();
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
    }
}
