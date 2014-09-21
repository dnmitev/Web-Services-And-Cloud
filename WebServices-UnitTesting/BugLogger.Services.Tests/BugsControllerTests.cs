namespace BugLogger.Services.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Results;
    
    using Moq;

    using BugLogger.Data.Contracts;
    using BugLogger.Models;
    using BugLogger.Services.Controllers;
    using BugLogger.Services.Models;

    [TestClass]
    public class BugsControllerTests
    {
        private static Mock<IAppData> repo;
        private bool isAddCalled = false;
        private bool isSaveCalled = false;

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

            repo.Setup(r => r.Bugs.Add(It.Is<Bug>(b => b.LogText != null)))
                .Callback(() => isAddCalled = true);

            repo.Setup(r => r.SaveChanges())
                .Callback(() => isSaveCalled = true);
        }

        [TestMethod]
        public void BugsController_All_ShouldReturn_AllEntries()
        {
            var controller = new BugsController(repo.Object);

            var actionResult = controller.All();
            var contentResult = actionResult as OkNegotiatedContentResult<IQueryable<BugModel>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(3, contentResult.Content.Count());
        }

        [TestMethod]
        public void BugsController_AllAfterDate_WithExistingDate_ShouldReturn_AllEntriesAfterTheDate()
        {
            var controller = new BugsController(repo.Object);

            var actionResult = controller.AllAfterDate(new DateTime(2014, 09, 21));
            var contentResult = actionResult as OkNegotiatedContentResult<IQueryable<BugModel>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.Count());
        }

        [TestMethod]
        public void BugsController_AllAfterDate_NoExistingEntries_ShouldReturn_EmptyCollection()
        {
            var controller = new BugsController(repo.Object);

            var actionResult = controller.AllAfterDate(new DateTime(2014, 10, 25));
            var contentResult = actionResult as OkNegotiatedContentResult<IQueryable<BugModel>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(0, contentResult.Content.Count());
        }

        [TestMethod]
        public void BugsController_AllAfterDate_InclusiveDate_ShouldReturn_ValidEntries()
        {
            var controller = new BugsController(repo.Object);

            var actionResult = controller.AllAfterDate(new DateTime(2014, 09, 20));
            var contentResult = actionResult as OkNegotiatedContentResult<IQueryable<BugModel>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(2, contentResult.Content.Count());
        }

        [TestMethod]
        public void BugsController_AllWithStatus_ValidStatus_ShouldReturn_ValidEntries()
        {
            var controller = new BugsController(repo.Object);

            var actionResult = controller.AllWithStatus(Status.Pending);
            var contentResult = actionResult as OkNegotiatedContentResult<IQueryable<BugModel>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(2, contentResult.Content.Count());
        }

        [TestMethod]
        public void BugsController_AllWithStatus_InvalidStatus_ShouldReturn_EmptyCollection()
        {
            var controller = new BugsController(repo.Object);

            var actionResult = controller.AllWithStatus(Status.ForTesting);
            var contentResult = actionResult as OkNegotiatedContentResult<IQueryable<BugModel>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(0, contentResult.Content.Count());
        }

        [TestMethod]
        public void BugsController_Create_ValidBugEntry_ShoudlAddEntry()
        {
            var controller = new BugsController(repo.Object);

            var actionResult = controller.Create("newly created bug text");
            var createdResult = actionResult as OkNegotiatedContentResult<BugModel>;

            Assert.IsNotNull(createdResult);
            Assert.IsTrue(isAddCalled);
            Assert.IsTrue(isSaveCalled);
        }

        [TestMethod]
        public void BugsController_Create_EmptyStringAsBugLogText_ShouldReturnBadReques()
        {
            var controller = new BugsController(repo.Object);

            var actionResult = controller.Create(string.Empty);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void BugsController_Create_TooShortStringAsBugLogText_ShouldReturnBadReques()
        {
            var controller = new BugsController(repo.Object);

            var actionResult = controller.Create("xxx");

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void BugsController_UpdateStatus_ValidEntry_ShouldHaveChangedStatus()
        {
            var controller = new BugsController(repo.Object);

            var beforeUpdate = repo.Object.Bugs.All().FirstOrDefault(r => r.Id == 1).Status;

            var actionResult = controller.ChangeBugStatus(1, Status.ForTesting);
            var contentResult = actionResult as OkNegotiatedContentResult<BugModel>;

            Assert.IsNotNull(contentResult);
            Assert.IsTrue(isSaveCalled);
            Assert.IsFalse(beforeUpdate == contentResult.Content.Status);
        }

        [TestMethod]    
        public void BugsController_UpdateStatus_NonExistingBugId_ShouldReturnBadReques()
        {
            var controller = new BugsController(repo.Object);

            var actionResult = controller.ChangeBugStatus(10, Status.ForTesting);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        public void BugsController_UpdateStatus_SameStatus_ShouldReturnBadReques()
        {
            var controller = new BugsController(repo.Object);

            var actionResult = controller.ChangeBugStatus(1, Status.Pending);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }
    }
}