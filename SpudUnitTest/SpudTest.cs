﻿using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using SpudSnatch.Model.Objects;
using System.IO;
using SpudSnatch.Model.Serialization;
using SpudSnatch.Model;
using System.Collections.Generic;

namespace SpudTest
{
    [TestClass]
    public class SpudTest
    {
        /*[TestMethod]
        public void TestJump()
        {
            Homer homer = new Homer(5,0);
            int[] height = homer.Jump();
            int max = height[0];
            int last = height[1];
            int start = height[2];
            int intendedMax = 50;
            int intendedland = 0;
            int initialStart = 0;
            Assert.IsTrue(max == intendedMax && last == intendedland && initialStart == start);
        }*/

        /*[TestMethod]
        public void TestWalk()
        {
            Homer homer = new Homer(5, 0);
            int[] distance = homer.Walk();
            int start = distance[0];
            int finish = distance[1];
            int initialStart = 5;
            int intendedFinish = 20;
            Assert.IsTrue(finish == intendedFinish && initialStart == start);
        }*/

        [TestMethod]
        public void TestSave()
        {
            List<string> csv = new List<string>();
            csv.Add("asdfasdf");
            //using (FileStream save = new FileStream(@"C:\Users\Public\Documents\SaveData.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            //{
            FileStream data = File.Open(@"Data\Levels\TestLevel.txt", FileMode.Open, FileAccess.ReadWrite);
            //foreach (string line in csv)
            //wr.WriteLine(line);

        }


        [TestMethod]
        public void TestLoad()
        {
            SerializeData.DeserializeInfo("SaveDataTest");
            Assert.IsTrue(GameController.LevelProgress == 5 && GameController.Score == 9001);
            Assert.IsTrue(GameController.level.Player.positionX == 15 && GameController.level.Player.positionY == 87);
            List<Potato> potatoes = GameController.level.GetPotatoes();
            Assert.IsTrue(potatoes[0].positionX == 5 && potatoes[0].positionY == 15 && potatoes[0].retrieved == true);
            Assert.IsTrue(potatoes[1].positionX == 9 && potatoes[1].positionY == 67 && potatoes[1].retrieved == false);
            Assert.IsTrue(potatoes[2].positionX == 0 && potatoes[2].positionY == 0 && potatoes[2].retrieved == true);
            List<Character> enemies = GameController.level.GetEnemies();
            Assert.IsTrue(enemies[0].positionX == 5 && enemies[0].positionY == 0);
            Assert.IsTrue(enemies[1].positionX == 10 && enemies[1].positionY == 9);
        }
    }
}
