using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using SpudSnatch.Model.Objects;
using System.IO;
using SpudSnatch.Model.Serialization;
using SpudSnatch.Model;
using System.Collections.Generic;
using Windows.Storage;
using Windows.ApplicationModel;
using SpudSnatch.Screens;

namespace SpudTest
{
    [TestClass]
    public class SpudTest
    {
        public async void TestSave()
        {
            List<string> csv = new List<string>();
            csv.Add("gc,5,9001");
            csv.Add("hm, 15, 87");
            csv.Add("po,5,15,true");
            csv.Add("po,9,67,false");
            csv.Add("po, 0, 0, true");
            csv.Add("en,5,0");
            csv.Add("en,10,9");
            StorageFolder saves = ApplicationData.Current.LocalFolder;
            StorageFile save = await saves.CreateFileAsync("SaveDataTest.txt", CreationCollisionOption.ReplaceExisting);
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


        [TestMethod]
        public void TestLoad()
        {
            TestSave();
            SerializeData.DeserializeInfo("SaveDataTest");
            GameController game = GameController.Instance;
            Assert.IsTrue(game.LevelProgress == 5 && game.Score == 9001);
            Assert.IsTrue(game.level.Player.PositionX == 15 && game.level.Player.PositionY == 87);
            List<Potato> potatoes = game.level.GetPotatoes();
            Assert.IsTrue(potatoes[0].PositionX == 5 && potatoes[0].PositionY == 15 && potatoes[0].Retrieved == true);
            Assert.IsTrue(potatoes[1].PositionX == 9 && potatoes[1].PositionY == 67 && potatoes[1].Retrieved == false);
            Assert.IsTrue(potatoes[2].PositionX == 0 && potatoes[2].PositionY == 0 && potatoes[2].Retrieved == true);
            List<Character> enemies = game.level.GetEnemies();
            Assert.IsTrue(enemies[0].PositionX == 5 && enemies[0].PositionY == 0);
            Assert.IsTrue(enemies[1].PositionX == 10 && enemies[1].PositionY == 9);
        }
    }
}
