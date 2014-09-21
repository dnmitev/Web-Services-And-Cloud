namespace BugLogger.Data.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Transactions;
    using System.Linq;
    using System.Data;
    using BugLogger.Models;
    using System.Data.Entity.Validation;

    [TestClass]
    public class RepositoryTest
    {
        private static TransactionScope tran;

        [TestInitialize]
        public void TestInit()
        {
            tran = new TransactionScope();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void AddNewValidBug_ShouldReturn_CollectionWithCountPlus1()
        {
            var bug = this.GetValidBug();

            var data = new AppData();
            int count = data.Bugs.All().Count();

            data.Bugs.Add(bug);
            data.SaveChanges();

            Assert.AreEqual(count + 1, data.Bugs.All().Count());
        }

        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddNewInvalidBug_ThrowsExceptipn_DbEntityValidationException()
        {
            var bug = this.GetInvalidBug();

            var data = new AppData();
            int count = data.Bugs.All().Count();

            data.Bugs.Add(bug);
            data.SaveChanges();
        }

        [TestMethod]
        public void DeleteExistingEntity_ShouldReturn_CollectionwithCountMinus1()
        {
            var bug = this.GetValidBug();

            var data = new AppData();

            data.Bugs.Add(bug);
            data.SaveChanges();

            int count = data.Bugs.All().Count();

            data.Bugs.Delete(bug);
            data.SaveChanges();

            Assert.AreEqual(count - 1, data.Bugs.All().Count());
        }

        [TestMethod]
        public void UpdateExistingEntity_ShouldUpdateIt()
        {
            var bug = this.GetValidBug();

            var data = new AppData();

            data.Bugs.Add(bug);
            data.SaveChanges();

            var bugToChange = data.Bugs.All().FirstOrDefault(b => b.Id == bug.Id);

            bugToChange.LogText = "update log text";

            data.Bugs.Update(bugToChange);
            data.SaveChanges();

            Assert.AreEqual("update log text", data.Bugs.All().FirstOrDefault(b => b.Id == bug.Id).LogText);
        }

        private Bug GetValidBug()
        {
            return new Bug
            {
                LogText = "Test Bug",
                LogDate = DateTime.Now,
                Status = Status.Pending
            };
        }

        private Bug GetInvalidBug()
        {
            return new Bug
            {
                LogText = "bug"
            };
        }
    }
}