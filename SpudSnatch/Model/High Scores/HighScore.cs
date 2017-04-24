using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpudSnatch.Model.HighScores
{

    // Contains the information for a high score entry
    class HighScoreEntry
    {
        string PlayerName, Time;
        int Score;

        // Constructor for the high score entry
        public HighScoreEntry(string n, int s, string time)
        {
            PlayerName = n;
            Score = s;
            Time = time;
        }

        //Return PlayerName
        public string GiveName()
        {
            return PlayerName;
        }

        //Return Score
        public int GiveScore()
        {
            return Score;
        }

        public string GiveTime()
        {
            return Time;
        }
    }

    // Contains a list of the high scores
    class HighScoreList
    {
        private static HighScoreList instance;
        // Private instance variable for the list of high scores
        private List<HighScoreEntry> scores = new List<HighScoreEntry>();
        // Public property for the high score list
        public List<HighScoreEntry> Scores { get { return scores; } }

        // Constructor for the high score list
        private HighScoreList()
        {

        }

        public static HighScoreList Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HighScoreList();
                }
                return instance;
            }
        }

        // Resets the entries to null
        void Reset()
        {
            throw new NotImplementedException();
        }

        // Add a new entry to the list
        // 'n' is the name of the high scorer.
        // 's' is the score they achieved.
        // 't' is the time they achieved.
        public void AddEntry(string n, int s, string t)
        {
            // Check if score is good enough for list, and add it if it is.
            scores.Add(new HighScoreEntry(n, s, t));
        }

        //Return high score player name
        public string GetName(HighScoreEntry entry)
        {
            return entry.GiveName();
        }

        //Return high score player score
        public int GetScore(HighScoreEntry entry)
        {
            return entry.GiveScore();
        }

        //Return high score player time
        public string GetTime(HighScoreEntry entry)
        {
            return entry.GiveTime();
        }
    }
}