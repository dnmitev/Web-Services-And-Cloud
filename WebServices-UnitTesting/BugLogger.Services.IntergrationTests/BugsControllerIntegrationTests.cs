namespace BugLogger.Services.IntergrationTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using Moq;

    using BugLogger.Data.Contracts;
    using BugLogger.Models;

    [TestClass]
    public class BugsControllerIntegrationTests
    {
        private string inMemoryServerUrl = "http://localhost:12345/";

        private static Mock<IAppData> repo;

        [TestInitialize]
        public void TestInit()
        {
            repo = new Mock<IAppData>();

            var bugs = new List<Bug>
            {
                new Bug
                {
                    Id = 1,
                    LogText = "Test Bug 1",
                    LogDate = new DateTime(2014,09,13),
                    Status = Status.Pending
                },
                new Bug
                {
                    Id = 2,
                    LogText = "Test Bug 2",
                    LogDate = new DateTime(2014,09,20),
                    Status = Status.Fixed
                },
                new Bug
                {
                    Id = 3,
                    LogText = "Test Bug 3",
                    LogDate = new DateTime(2014,09,21),
                    Status = Status.Pending
                }
            };

            repo.Setup(r => r.Bugs.All())
                .Returns(() => bugs.AsQueryable());
        }

        [TestMethod]
        public void GetAll_WhenBugsInDb_ShouldReturnAllBugsAndStatus200()
        {
            var server = new InMemoryHttpServer(this.inMemoryServerUrl, repo.Object);

            var response = server.CreateGetRequest("bugs");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Content);
        }

        [TestMethod]
        public void GetAllAfterDate_WhenBugsInDb_ShouldReturnStatus200()
        {
            var server = new InMemoryHttpServer(this.inMemoryServerUrl, repo.Object);

            var response = server.CreateGetRequest("bugs?date=2014-09-20");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Content);
        }

        [TestMethod]
        public void GetAllWithStatus_WhenBugsInDb_ShouldReturnStatus200()
        {
            var server = new InMemoryHttpServer(this.inMemoryServerUrl, repo.Object);

            var response = server.CreateGetRequest("bugs?status=Pending");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Content);
        }

        [TestMethod]
        public void CreateBug_WhenBugsInDb_ShouldReturnStatus200()
        {
            Bug bug = new Bug()
            {
                LogText = "bug-buugg"
            };

            var server = new InMemoryHttpServer(this.inMemoryServerUrl, repo.Object);

            var response = server.CreatePostRequest(string.Format("bugs?bugLogText={0}", bug.LogText), bug);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void CreateBug_InvalidData_ShouldReturnBadRequest()
        {
            Bug bug = new Bug()
            {
                LogText = ""
            };

            var server = new InMemoryHttpServer(this.inMemoryServerUrl, repo.Object);

            var response = server.CreatePostRequest(string.Format("bugs?bugLogText={0}", bug.LogText), bug);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void ChangeBugStatus_WhenBugsInDb_ShouldReturnStatus200()
        {
            var server = new InMemoryHttpServer(this.inMemoryServerUrl, repo.Object);

            var response = server.CreatePutRequest("bugs?bugId=1&status=ForTesting");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void ChangeBugStatus_Invalid_ShouldReturnBadRequest()
        {
            var server = new InMemoryHttpServer(this.inMemoryServerUrl, repo.Object);

            var response = server.CreatePutRequest("bugs?bugId=1&status=ForTesting");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}