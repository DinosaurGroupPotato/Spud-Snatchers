using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpudSnatch.Model.Serialization;
using SpudSnatch.Model.Objects;
using SpudSnatch.State;

namespace SpudSnatch.Model
{
    //Difficulty variable states for difficulty selection
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard, 
        Death
    }

    public class GameController
    {
        //Singleton instance of GameController class
        private static GameController instance = new GameController();
        //Getter for GameController instance
        //Calls private constructor if no instance of GameController
        //has been created yet
        public static GameController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameController();
                }
                return instance;
            }
        }

        //Clears GameController instance
        public void ResetGame()
        {
            instance = null;
        }

        private Dictionary<string, int> scores = new Dictionary<string, int>();
        private Difficulty LevelDifficulty = Difficulty.Easy;
        public bool GameOver { get; set;}
        public bool IsCheatMode = false;

        public Level level { get; set; }
        public int Score { get; set; }
        public int Time { get; set; }

        public int LevelProgress { get; set; }

        //Private constructor for GameController
        private GameController()
        {
            level = new Level();
            LevelProgress = 1;
            Score = 0;
            Time = 0;
            GameOver = false;
            IsCheatMode = false;
        }

		public void UpdateGameController()
        {
            // Cheat mode
            if (KeyboardState.C == KeyState.Down)
            {
                IsCheatMode = !IsCheatMode;
            }

            // Update Homer
            level.Player.Update();

            //UpdateEnemies();
            //UpdateObstacles();
        }

        public string Serialize()
        {
            string data = "gc" + "," + Convert.ToString(LevelProgress) + "," + Convert.ToString(Score);
            return data;
        }

        public string ScoreSerialize()
        {
            string data = "sc";
            foreach(string key in scores.Keys)
            {
                data += "," + key + "." + Convert.ToString(scores[key]);
            }
            return data;
        }

        public string LevelSerialize()
        {
            string data = "gl" + "," + Convert.ToString(LevelProgress);
            return data;
        }

        public void Deserialize(string[] line)
        {
            LevelProgress = Convert.ToInt32(line[1]);
            Score = Convert.ToInt32(line[2]);
        }

        //Advance to next level
        //For future expansion
        public void LevelProgressAdvance()
        {
            throw new NotImplementedException();
        }

        //Calls Walk three times for each enemy in the level
        private void UpdateEnemies()
        {
            foreach (Enemy enemy in level.GetEnemies())
            {
                enemy.Walk(enemy, 3);
            }
        }

        //Takes state of potato
        //Increases score if state is not poisoned
        public void IncreaseScore(PotatoState state)
        {
            if (state == PotatoState.Big)
                Score += 60;
            else if (state == PotatoState.SmallPoisoned )
                Score = Score - 20;
            else if (state == PotatoState.BigPoisoned)
                Score = Score - 60;
            else
                Score += 20;
        }

        //Gets difficulty selected
        public Difficulty IsDifficultyLevel()
        {
            return LevelDifficulty;
        }

        //Sets difficulty of game
        public void SetDifficulty(Difficulty type)
        {
            LevelDifficulty = type;
        }

        //Checks coordinates of 'potato' against the x- and y- coordinates passed 
        //in 'xposit' and 'yposit'
        public void CheckPotatoCollected(int xposit, int yposit, Potato potato)
        {
            if (xposit == potato.PositionX && yposit == potato.PositionY)
            {
                potato.CollectPotato();
            }
        }

    }
}
