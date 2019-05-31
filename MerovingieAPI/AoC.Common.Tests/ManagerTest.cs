using System;
using System.Linq;
using AoC.Api.Domain;
using AoC.Api.Domain.UseCases;
using AoC.Common.Descriptors;
using AoC.MerovingieFileManager;
using AutoMapper;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AoC.Domain.Tests
{
    [TestClass]
    public class ManagerTest
    {

        [ClassInitialize()]
        public static void MappingTestInitialize(TestContext testContext)
        {
            Mapper.Initialize(cfg => cfg.AddProfile<DomainProfile>());
        }

        #region Constructor

        [TestMethod]
        public void ActualPopulation_With2Workers_Returns2()
        {
            //
            var gameManager = new GameManager();

            //
            gameManager.PopulationList.Add(new Worker());
            gameManager.PopulationList.Add(new Worker());

            //
            Assert.AreEqual(2, gameManager.PopulationList.Count);
            Assert.AreEqual(1, gameManager.PopulationList[0].PopulationSlots);
            Assert.AreEqual(2, gameManager.ActualPopulation);
        }


        [TestMethod]
        public void ActualPopulation_With2Farms_Returns2()
        {
            //
            var gameManager = new GameManager();

            //
            gameManager.BuildingList.Add(new Farm());
            gameManager.BuildingList.Add(new Farm());

            //
            Assert.AreEqual(2, gameManager.BuildingList.Count);
            Assert.AreEqual(4, ((Farm)gameManager.BuildingList[0]).PopulationIncrement);
            Assert.AreEqual(8, gameManager.MaxPopulation);
        }

        [TestMethod]
        public void CreatesId_WithDescriptor_Ok()
        {
            //
            var gameManager = new GameManager(GameGenerator.GenerateDefaultMap());

            //TODO: décaler ceci dans un test du GameGenerator
            Assert.AreEqual(5, gameManager.BuildingList.Count);
            Assert.AreEqual(2, gameManager.PopulationList.Count);

            Assert.AreEqual(5, gameManager.BuildingList.Select(x => x.Id).Distinct().Count());
            Assert.AreEqual(2, gameManager.PopulationList.Select(x => x.Id).Distinct().Count());
        }
        #endregion

    }
}
