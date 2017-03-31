using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpudSnatch.Model.Objects;

namespace SpudSnatch.Model.Serialization
{
    interface Serialized
    {
        void AddToObjects();
        string Serialize();
        string Deserialize();

    }

    class SerializeData
    {
        static List<Serialized> objects = new List<Serialized>();

        static List<string> csv = new List<string>();

        public static void SerializeInfo()
        {
            foreach (Serialized obj in objects)
            {
                csv.Add(obj.Serialize());
            }
            using (StreamWriter saveFile = File.AppendText("SaveData.txt"))
            {
                foreach (string line in csv)
                {
                    saveFile.WriteLine(line);
                }
            }

            //will do research and rework
        }

        public static void DeserializeInfo()
        {
            csv = new List<string>();
            ///implementation of choosing which type here
            ///recommend that each type start with its own 2 character identifier
            using (StreamReader saveFile = File.OpenText("SaveData.txt"))
            {
                csv.Add(saveFile.ReadLine());
            }

            foreach(string line in csv)
            {
                string[] attr = line.Split(',');
                if(attr[0] == "00")
                {
                    Homer homer = new Homer(Convert.ToInt32(attr[1]), Convert.ToInt32(attr[2]));
                }
                else
                {
                    GameController.levelProgress += Convert.ToInt32(attr[1]) - 1;
                }
               
            }
            ///object specific data
            ///level progress
            ///x,y,health
        }
    }
}
