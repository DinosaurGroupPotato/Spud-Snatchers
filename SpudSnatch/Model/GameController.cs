﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpudSnatch.Model.Serialization;
using SpudSnatch.Model.Objects;

namespace SpudSnatch.Model
{
    public class GameController
    {
        private static GameController instance = new GameController();
        private static Dictionary<string, int> scores = new Dictionary<string, int>();
        private static string LevelDifficulty;
        public static bool GameOver { get; set;}

        public static Level level { get; set; }
        public static int Score { get; set; }
        public static int Time { get; set; }

        public static int LevelProgress { get; set; }

        private GameController()
        {
            level = new Level();
            GameController.LevelProgress = 1;
            GameController.Score = 0;
            GameController.Time = 0;
            GameOver = false;
            Task collect = Task.Run(() => Homer.GrabTater());
        }

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
		public static void UpdateGameController()
        {
            UpdateHomer();
            //UpdateEnemies();
        }

        public static string Serialize()
        {
            string data = "gc" + "," + Convert.ToString(GameController.LevelProgress) + "," + Convert.ToString(GameController.Score);
            return data;
        }
        public static string ScoreSerialize()
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
            string data = "gl" + "," + Convert.ToString(GameController.LevelProgress);
            return data;
        }

        public static void Deserialize(string[] line)
        {
            GameController.LevelProgress = Convert.ToInt32(line[1]);
            GameController.Score = Convert.ToInt32(line[2]);
        }

        public void LevelProgressAdvance()
        {
            throw new NotImplementedException();
        }

		public static void UpdateHomer()
        {
            int xpos = level.ReturnPlayerPosition("x", level.GetHomer());
            int ypos = level.ReturnPlayerPosition("y", level.GetHomer());
            foreach (Potato potato in GameController.level.GetPotatoes())
            {
                CheckPotatoCollected(xpos, ypos, potato);
            }
        }
        private static void UpdateEnemies()
        {
            //level.
            throw new NotImplementedException();
        }

        public static void IncreaseScore(bool big, bool poisoned)
        {
            if (big && !poisoned)
                GameController.Score += 60;
            else if (poisoned && !big)
                GameController.Score = GameController.Score - 20;
            else if (poisoned && big)
                GameController.Score = GameController.Score - 60;
            else
                GameController.Score += 20;
        }
        
        public static void CheckPotatoCollected(int xposit, int yposit, Potato potato)
        {
            if (xposit == potato.positionX && yposit == potato.positionY)
            {
                potato.CollectPotato();
            }
        }
    }
}
