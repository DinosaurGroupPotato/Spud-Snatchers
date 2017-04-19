using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpudSnatch.Model.Serialization;
using SpudSnatch.Model.Objects;

namespace SpudSnatch.Model
{
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard, 
        Death
    }

    public class GameController
    {
        private static GameController instance = new GameController();
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

        public void ResetGame()
        {
            instance = null;
        }

        private Dictionary<string, int> scores = new Dictionary<string, int>();
        private Difficulty LevelDifficulty;
        public bool GameOver { get; set;}

        public Level level { get; set; }
        public int Score { get; set; }
        public int Time { get; set; }

        public int LevelProgress { get; set; }

        private GameController()
        {
            level = new Level();
            LevelProgress = 1;
            Score = 0;
            Time = 0;
            GameOver = false;
            Task collect = Task.Run(() => Homer.GrabTater());
        }

		public void UpdateGameController()
        {
            UpdateHomer();
            UpdateEnemies();
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

        public void LevelProgressAdvance()
        {
            throw new NotImplementedException();
        }

		public void UpdateHomer()
        {
            level.Player.Update();
            foreach (Potato potato in level.GetPotatoes())
            {
                if (level.Player.IsCollidedChar(potato))
                {
                    potato.CollectPotato();
                }
            }
        }

        private void UpdateEnemies()
        {
            foreach (Enemy enemy in level.GetEnemies())
            {
                enemy.Walk(enemy, 3);
            }
        }

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

        public Difficulty IsDifficultyLevel()
        {
            return LevelDifficulty;
        }

        public void SetDifficulty(Difficulty type)
        {
            LevelDifficulty = type;
        }

        public void CheckPotatoCollected(int xposit, int yposit, Potato potato)
        {
            if (xposit == potato.PositionX && yposit == potato.PositionY)
            {
                potato.CollectPotato();
            }
        }

    }
}
