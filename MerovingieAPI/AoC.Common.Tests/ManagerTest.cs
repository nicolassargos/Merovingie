using System;
using AoC.Api.Domain;
using AoC.Api.Domain.UseCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AoC.Domain.Tests
{
    [TestClass]
    public class ManagerTest
    {
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
        #endregion

    }
}
