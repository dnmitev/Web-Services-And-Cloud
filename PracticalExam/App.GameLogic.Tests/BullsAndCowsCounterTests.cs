using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using App.GameLogic.Contracts;
using App.GameLogic;

namespace App.GameLogic.Tests
{
    [TestClass]
    public class BullsAndCowsCounterTests
    {
        private IBullsAndCowsCounter counter;

        [TestInitialize]
        public void TestInit()
        {
            this.counter = new BullsAndCowsCounter();
        }

        [TestMethod]
        public void ValidNumbers_ShouldReturnValidResult()
        {
            var result = counter.CountBullsAndCows(1234, 2735);

            Assert.AreEqual(1, result.BullsCount);
            Assert.AreEqual(1, result.CowsCount);
        }

        [TestMethod]
        public void ValidNumbers_ShouldReturn4Cows0Bulls()
        {
            var result = counter.CountBullsAndCows(1234, 4321);

            Assert.AreEqual(0, result.BullsCount);
            Assert.AreEqual(4, result.CowsCount);
        }

        [TestMethod]
        public void ValidNumbers_ShouldReturn0Cows4Bulls()
        {
            var result = counter.CountBullsAndCows(1234, 1234);

            Assert.AreEqual(4, result.BullsCount);
            Assert.AreEqual(0, result.CowsCount);
        }

        [TestMethod]
        public void ValidNumbers_ShouldReturn0Cows3Bulls()
        {
            var result = counter.CountBullsAndCows(1234, 1237);

            Assert.AreEqual(3, result.BullsCount);
            Assert.AreEqual(0, result.CowsCount);
        }
    }
}
