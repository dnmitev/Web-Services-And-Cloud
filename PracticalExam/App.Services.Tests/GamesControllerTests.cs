namespace App.Services.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using App.Data.Contracts;
    using Moq;
    using App.Models;
    using App.Services.DataModels;
    using System.Collections.Generic;
    using App.Services.Controllers;
    using System.Linq;
    using System.Web.Http.Results;

    [TestClass]
    public class GamesControllerTests
    {
        private static Mock<IAppData> mockData;

        [TestInitialize]
        public void TestInit()
        {
            mockData = new Mock<IAppData>();
            List<Game> games = new List<Game>
            {
                new Game() { Name = "Game1", GameState = GameState.WaitingForOpponent, DateCreated = DateTime.Now, FirstUser = new User(), SecondUser = new User() },
                new Game() { Name = "Game2", GameState = GameState.Finished, DateCreated = DateTime.Now, FirstUser = new User(), SecondUser = new User() },
                new Game() { Name = "Game3", GameState = GameState.WaitingForOpponent, DateCreated = DateTime.Now, FirstUser = new User(), SecondUser = new User() },
            };

            mockData.Setup(r => r.Games.All())
                    .Returns(() => games.AsQueryable());
        }

        [TestMethod]
        public void GamesAll_ShouldReturnGameStatesOnlyAvailableWithCount2()
        {
            var controller = new GamesController(mockData.Object);

            var actionResult = controller.All();
            var contentResult = actionResult as OkNegotiatedContentResult<IQueryable<GetGameDataModel>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(2, contentResult.Content.Count());
        }

        [TestMethod]
        public void GamesAll_ShouldReturnGameStatesOnlyAvailable()
        {
            var controller = new GamesController(mockData.Object);

            var actionResult = controller.All();
            var contentResult = actionResult as OkNegotiatedContentResult<IQueryable<GetGameDataModel>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);

            foreach (var game in contentResult.Content)
            {
                Assert.AreEqual(GameState.WaitingForOpponent.ToString(),game.GameState);
            }
        }
    }
}