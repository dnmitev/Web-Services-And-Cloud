namespace App.Data.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Transactions;

    [TestClass]
    public class RepositoryTests
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
        public void TestMethod1()
        {
            // TODO: Test repository methods
            // TODO: Introduce valid connection string
        }
    }
}