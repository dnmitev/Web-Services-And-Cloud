using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToe.GameLogic;
using TicTacToe.Models;

namespace TicTacToe.Tests
{
    [TestClass]
    public class GameResultValidatorTests
    {
        private IGameResultValidator validator;

        [TestInitialize]
        public void TestInit()
        {
            validator = new GameResultValidator();
        }

        [TestMethod]
        public void GetResult_NoWinner_ShouldReturnGameStateNotFinished()
        {
            var result = validator.GetResult("OXOOOXXOX");

            Assert.AreEqual(GameResult.NotFinished, result);
        }

        [TestMethod]
        public void GetResult_WinnerInRow_ShouldReturnGameStateWonByO()
        {
            var result = validator.GetResult("OOOXOXOXO");

            Assert.AreEqual(GameResult.WonByO, result);
        }

        [TestMethod]
        public void GetResult_WinnerInRow_ShouldReturnGameStateWonByX()
        {
            var result = validator.GetResult("OXOXXXOOX");

            Assert.AreEqual(GameResult.WonByX, result);
        }

        [TestMethod]
        public void GetResult_WinnerInColumn_ShouldReturnGameStateWonByO()
        {
            var result = validator.GetResult("OXOOOXOXO");

            Assert.AreEqual(GameResult.WonByO, result);
        }

        [TestMethod]
        public void GetResult_WinnerInColumn_ShouldReturnGameStateWonByX()
        {
            var result = validator.GetResult("oxoxxooxx");

            Assert.AreEqual(GameResult.WonByX, result);
        }

        [TestMethod]
        public void GetResult_WinnerInDiagnoal_ShouldReturnGameStateWonByO()
        {
            var result = validator.GetResult("oxxxoooxo");

            Assert.AreEqual(GameResult.WonByO, result);
        }

        [TestMethod]
        public void GetResult_WinnerInDiagnoal_ShouldReturnGameStateWonByX()
        {
            var result = validator.GetResult("ooxxxoxoo");

            Assert.AreEqual(GameResult.WonByX, result);
        }

        [TestMethod]
        public void GetResult_InvalidData_ShouldReturnGameStateNotFinished()
        {
            var result = validator.GetResult("o--------");

            Assert.AreEqual(GameResult.NotFinished, result);
        }
    }
}
