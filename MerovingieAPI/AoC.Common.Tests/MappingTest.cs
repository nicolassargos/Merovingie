using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;
using Domain;
using AoC.Api.Domain;
using AoC.Domain.TypeExtentions;
using Common.Enums;
using Domain.TypeExtentions;
using AoC.DataLayer.Descriptors;
using Common.Helpers;

namespace AoC.Domain.Tests
{
    /// <summary>
    /// Summary description for MappingTest
    /// </summary>
    [TestClass]
    public class MappingTest
    {
        public MappingTest()
        {
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MappingTestInitialize(TestContext testContext)
        {
            Mapper.Initialize(cfg => cfg.AddProfile<DomainProfile>());
        }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ConvertGoldMineToDescriptor_Ok()
        {
            var goldMine = new GoldMine();

            var goldMineDescriptor = goldMine.ToGoldMineDescriptor();

            Assert.AreEqual(goldMine.Stock[ResourcesType.Gold], goldMineDescriptor.Stock[ResourcesType.Gold]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ConvertWorkerToDescriptor_Ok()
        {
            var worker = new Worker();

            var workerDescriptor = worker.ToWorkerDescriptor();

            Assert.AreEqual(worker.AttackPower, workerDescriptor.AttackPower);
        }


        [TestMethod]
        public void ConvertTreeToDescriptor_Ok()
        {
            var tree = new Tree();
            tree.Stock[ResourcesType.Wood] = 123;

            var treeDescriptor = tree.ToTreeDescriptor();

            Assert.AreEqual(tree.Stock[ResourcesType.Wood], treeDescriptor.Stock[ResourcesType.Wood]);
        }

        [TestMethod]
        public void ConvertTreeDescriptorToTree_Ok()
        {
            var treeDescriptor = new TreeDescriptor();
            treeDescriptor.Stock = ResourceHelper.CreateEmptyResourcesDictionary();
            treeDescriptor.Stock[ResourcesType.Wood] = 123;

            var tree = treeDescriptor.ToTree();

            Assert.AreEqual(treeDescriptor.Stock[ResourcesType.Wood], tree.Stock[ResourcesType.Wood]);
        }


        [TestMethod]
        public void ConvertPassiveBuildingDescriptorToPassiveBuilding_Ok()
        {
            var passiveBuildingDescriptor = new PassiveBuildingDescriptor();
            passiveBuildingDescriptor.Stock = ResourceHelper.CreateEmptyResourcesDictionary();
            passiveBuildingDescriptor.Stock[ResourcesType.Wood] = 123;

            var passiveBuilding = passiveBuildingDescriptor.ToPassiveBuilding();

            Assert.AreEqual(passiveBuildingDescriptor.Stock[ResourcesType.Wood], passiveBuilding.Stock[ResourcesType.Wood]);
        }
    }
}
