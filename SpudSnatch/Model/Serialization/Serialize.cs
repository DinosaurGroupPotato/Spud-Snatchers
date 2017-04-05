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
            csv.Add(GameController.Serialize());
            //foreach (Potato obj in GameController.potatoes)
            //{
            //    csv.Add(obj.Serialize());
            //}
            //foreach (Enemy obj in GameController.enemies)
            //{
            //    csv.Add(obj.Serialize());
            //}
            //csv.Add(GameController.homer.Serialize());
            //using (StreamWriter saveFile = File.AppendText(filename + ".txt"))
            //{
            //    foreach (string line in csv)
             //   {
            //        saveFile.WriteLine(line);
            //    }
            }


        public static void DeserializeInfo(string filename)
        {
            csv = new List<string>();
            ///implementation of choosing which type here
            ///recommend that each type start with its own 2 character identifier
            using (StreamReader saveFile = File.OpenText(filename + ".txt"))
            {
                csv.Add(saveFile.ReadLine());
            }

            foreach (string line in csv)
            {
                string[] attr = line.Split(',');
                if (attr[0] == "gc")
                {
                    GameController.Deserialize(attr);
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