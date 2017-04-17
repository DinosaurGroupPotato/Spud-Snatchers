using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpudSnatch.Model.Objects;

namespace SpudSnatch.Model.Serialization
{

    public class SerializeData
    {
        static List<string> csv = new List<string>();

        public static void SerializeInfo(string filename)
        {
            csv = new List<string>();
            csv.Add(GameController.Instance.Serialize());
            csv.Add(GameController.Instance.ScoreSerialize());
            foreach (Potato obj in GameController.Instance.level.GetPotatoes())
            {
                csv.Add(obj.Serialize());
            }
            foreach (Enemy obj in GameController.Instance.level.GetEnemies())
            {
                csv.Add(obj.Serialize());
            }
            csv.Add(GameController.Instance.level.player.Serialize());
            //  Task save = Task.Run(() =>
            // {
            using (StreamWriter saveFile = File.AppendText(@"C:\Users\Public\Documents\SaveData.txt"))// +filename + ".txt"))
            {
                foreach (string line in csv)
                {
                    saveFile.WriteLine(line);
                }
            }
            //});

        }


        public static void DeserializeInfo(string filename)
        {
            csv = new List<string>();
            ///implementation of choosing which type here
            ///recommend that each type start with its own 2 character identifier
            using (StreamReader saveFile = File.OpenText(@"../../../..//SaveData.txt")) // + filename + ".txt")
            {
                csv.Add(saveFile.ReadLine());
            }

            foreach (string line in csv)
            {
                string[] attr = line.Split(',');
                if (attr[0] == "gc")
                {
                    GameController.Instance.Deserialize(attr);
                }
                else if (attr[0] == "hm")
                {
                    Homer.Deserialize(attr);
                }
                else if (attr[0] == "po")
                {
                    Potato.Deserialize(attr);
                }
                else if (attr[0] == "en")
                {
                    Enemy.Deserialize(attr);
                }

            }
            ///object specific data
            ///level progress
            ///x,y,health

        }
    }
}