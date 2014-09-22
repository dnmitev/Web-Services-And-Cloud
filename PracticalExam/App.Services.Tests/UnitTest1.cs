namespace App.Services.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using App.Data.Contracts;
    using Moq;

    [TestClass]
    public class UnitTest1
    {
        private static Mock<IAppData> mockData;
        private bool isAddCalled = false;
        private bool isSaveCalled = false;

        [TestInitialize]
        public void TestInit()
        {
            mockData = new Mock<IAppData>();
            // TODO: Setup mocked repo
            //var bugs = new List<Bug>
            //{
            //    new Bug
            //    {
            //        Id = 1,
            //        LogText = "Test Bug 1",
            //        LogDate = new DateTime(2014,09,13),
            //        Status = Status.Pending
            //    },
            //    new Bug
            //    {
            //        Id = 2,
            //        LogText = "Test Bug 2",
            //        LogDate = new DateTime(2014,09,20),
            //        Status = Status.Fixed
            //    },
            //    new Bug
            //    {
            //        Id = 3,
            //        LogText = "Test Bug 3",
            //        LogDate = new DateTime(2014,09,21),
            //        Status = Status.Pending
            //    }
            //};
            // TODO: Setup mocks
            //mockData.Setup(r => r.Bugs.All())
            //    .Returns(() => bugs.AsQueryable());
            //mockData.Setup(r => r.Bugs.Add(It.Is<Bug>(b => b.LogText != null)))
            //    .Callback(() => isAddCalled = true);
            //mockData.Setup(r => r.SaveChanges())
            //    .Callback(() => isSaveCalled = true);
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}