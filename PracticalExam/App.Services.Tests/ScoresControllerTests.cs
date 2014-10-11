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
    public class ScoresControllerTests
    {
        private static Mock<IAppData> mockData;

        [TestInitialize]
        public void TestInit()
        {
            mockData = new Mock<IAppData>();
            List<User> scores = new List<User>
            {
                new User() {UserName = "pesho1", WinsCount = 3, LossesCount = 1},
                new User() {UserName = "pesho2", WinsCount = 7, LossesCount = 0},
                new User() {UserName = "pesho3", WinsCount = 12, LossesCount = 1},
                new User() {UserName = "pesho5", WinsCount = 0, LossesCount = 1},
                new User() {UserName = "pesho5", WinsCount = 3, LossesCount = 1},
                new User() {UserName = "pesho9", WinsCount = 5, LossesCount = 1},
                new User() {UserName = "pesho7", WinsCount = 3, LossesCount = 1},
                new User() {UserName = "pesho8", WinsCount = 9, LossesCount = 1},
                new User() {UserName = "pesho6", WinsCount = 1, LossesCount = 1},
                new User() {UserName = "gosho", WinsCount = 2, LossesCount = 6},
                new User() {UserName = "stamat", WinsCount = 5, LossesCount = 3},
                new User() {UserName = "unufri", WinsCount = 9, LossesCount = 8}
            };

            mockData.Setup(r => r.Users.All())
                .Returns(() => scores.AsQueryable());
        }

        [TestMethod]
        public void ScoresAll_ShouldReturn10Entries()
        {
            var controller = new ScoresController(mockData.Object);

            var actionResult = controller.All();
            var contentResult = actionResult as OkNegotiatedContentResult<IQueryable<ScoreDataModel>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(10, contentResult.Content.Count());
        }

        [TestMethod]
        public void ScoresAll_ShouldReturnSortedEntriesByRank()
        {
            var controller = new ScoresController(mockData.Object);

            var actionResult = controller.All();
            var contentResult = actionResult as OkNegotiatedContentResult<IQueryable<ScoreDataModel>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual("pesho3", contentResult.Content.FirstOrDefault().Username);
        }

        [TestMethod]
        public void ScoresAll_ShouldReturnBestScores()
        {
            var controller = new ScoresController(mockData.Object);

            var actionResult = controller.All();
            var contentResult = actionResult as OkNegotiatedContentResult<IQueryable<ScoreDataModel>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.IsFalse(contentResult.Content.Any(u => u.Username == "pesho5" && u.Rank == 15));
        }
    }
}