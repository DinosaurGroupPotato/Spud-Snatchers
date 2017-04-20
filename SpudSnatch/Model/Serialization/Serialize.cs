using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpudSnatch.Model.Objects;
using Windows.Storage;
using Windows.ApplicationModel;
using SpudSnatch.Screens;


namespace SpudSnatch.Model.Serialization
{

    public class SerializeData
    {
        static List<string> csv = new List<string>();

        public async static void SerializeInfo(string filename)
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
            csv.Add(GameController.Instance.level.Player.Serialize());
            StorageFolder saves = ApplicationData.Current.LocalFolder;
            StorageFile save =  await saves.CreateFileAsync(filename + ".txt", CreationCollisionOption.ReplaceExisting);
            using (Stream saveData = await save.OpenStreamForWriteAsync())
            {
                using (StreamWriter content = new StreamWriter(saveData))
                {
                    foreach (string line in csv)
                    {
                        await content.WriteLineAsync(line);
                    }
                }
            }
        }


        public async static void DeserializeInfo(string filename)
        {
            List<string> csv = new List<string>();
            Character.nextID = 1;
            GameController.Instance.level.potatoes = new List<Potato>();
            StorageFolder saves = ApplicationData.Current.LocalFolder;
            StorageFile save;
            if (File.Exists(saves.Path +"\\"+ filename + ".txt"))
            {
                save = await saves.GetFileAsync(filename + ".txt");
                using (Stream saveData = await save.OpenStreamForWriteAsync())
                {
                    using (StreamReader content = new StreamReader(saveData))
                    {
                        string data;
                        while ((data = await content.ReadLineAsync()) != null)
                        {
                            csv.Add(data);
                        }
                    }
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
            }
            return;
        }
    }
}