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
        string PlayerName;
        int Score;

        // Constructor for the high score entry
        public HighScoreEntry(string n, int s)
        {
            PlayerName = n;
            Score = s;
        }
    }

    // Contains a list of the high scores
    class HighScoreList
    {
        // Private instance variable for the list of high scores
        private List<HighScoreEntry> scores;
        // Public property for the high score list
        public List<HighScoreEntry> Scores { get { return scores; } }

        // Constructor for the high score list
        public HighScoreList()
        {
            throw new NotImplementedException();
        }

        // Resets the entries to null
        void Reset()
        {
            throw new NotImplementedException();
        }

        // Add a new entry to the list
        // 'n' is the name of the high scorer.
        // 's' is the score they achieved.
        void AddEntry(string n, int s)
        {
            // Check if score is good enough for list, and add it if it is.
            throw new NotImplementedException();
        }

    }
}
