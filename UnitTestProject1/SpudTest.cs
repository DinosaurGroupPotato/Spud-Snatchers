using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using SpudSnatch.Model.Objects;

namespace SpudTest
{
    [TestClass]
    class SpudTest
    {
        [TestMethod]
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
        }

        [TestMethod]
        public void TestWalk()
        {
            Homer homer = new Homer(5, 0);
            int[] distance = homer.Walk();
            int start = distance[0];
            int finish = distance[1];
            int initialStart = 5;
            int intendedFinish = 20;
            Assert.IsTrue(finish == intendedFinish && initialStart == start);
        }
    }
}
