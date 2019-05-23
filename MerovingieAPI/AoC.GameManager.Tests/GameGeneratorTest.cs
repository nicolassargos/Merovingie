using Common.Enums;
using Common.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AutoMapper;
using Domain;
using AoC.Common.Descriptors;

namespace AoC.MerovingieFileManager.Tests
{
    [TestClass]
    public class GameGeneratorTest
    {
        [ClassInitialize()]
        public static void GameGeneratorTestInitialize(TestContext testContext)
        {
            Mapper.Initialize(cfg => cfg.AddProfile<DomainProfile>());
        }

        #region GenerateDefaultMap

        [TestMethod]
        public void GenerateDefaultMap_Constructor_Ok()
        {
            var gameGenerated = GameGenerator.GenerateDefaultMap();

            // Assert no null values
            Assert.IsTrue(gameGenerated is GameDescriptor);
            Assert.IsNotNull(gameGenerated.TownHalls);
            Assert.IsNotNull(gameGenerated.Carries);
            Assert.IsNotNull(gameGenerated.Trees);
            Assert.IsNotNull(gameGenerated.GoldMines);
            Assert.IsNotNull(gameGenerated.Farms);
            Assert.IsNotNull(gameGenerated.Workers);
            Assert.IsNotNull(gameGenerated.Resources);

            // Assert are values properly populated
            Assert.IsTrue(gameGenerated.TownHalls.Count == 1);
            Assert.IsTrue(gameGenerated.Carries.Count == 1);
            Assert.IsTrue(gameGenerated.Trees.Count == 0);
            Assert.IsTrue(gameGenerated.GoldMines.Count == 1);
            Assert.IsTrue(gameGenerated.Farms.Count == 2);
            Assert.IsTrue(gameGenerated.Workers.Count == 2);
            Assert.IsTrue(gameGenerated.Resources.Count == 3);
        }

        #endregion


        #region GenerateMapFromOptions

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GenerateMapFromOptions_ThrowsArgumentNullException_IfResourcesAreNull()
        {
            GameGenerator.GenerateMapFromOptions(1, 1, null);
        }

        [TestMethod]
        public void GenerateMapFromOptions_Creates_Ok()
        {
            var gameGenerated = GameGenerator.GenerateMapFromOptions(1, 1,
                new SerializableDictionary<ResourcesType, int>()
                {
                    { ResourcesType.Gold, 5 },
                    { ResourcesType.Stone, 5 },
                    { ResourcesType.Wood, 5 }
                });

            // Assert no null values
            Assert.IsTrue(gameGenerated is GameDescriptor);
            Assert.IsNotNull(gameGenerated.TownHalls);
            Assert.IsNotNull(gameGenerated.Carries);
            Assert.IsNotNull(gameGenerated.Trees);
            Assert.IsNotNull(gameGenerated.GoldMines);
            Assert.IsNotNull(gameGenerated.Farms);
            Assert.IsNotNull(gameGenerated.Workers);
            Assert.IsNotNull(gameGenerated.Resources);

            // Assert are values properly populated
            Assert.IsTrue(gameGenerated.TownHalls.Count == 1);
            Assert.IsTrue(gameGenerated.Carries.Count == 1);
            Assert.IsTrue(gameGenerated.Trees.Count == 0);
            Assert.IsTrue(gameGenerated.GoldMines.Count == 1);
            Assert.IsTrue(gameGenerated.Farms.Count == 1);
            Assert.IsTrue(gameGenerated.Workers.Count == 1); // TODO: factoriser le GameGenerator
            Assert.IsTrue(gameGenerated.Resources.Count == 3);
            Assert.IsTrue(gameGenerated.Resources[ResourcesType.Gold] == 5);
            Assert.IsTrue(gameGenerated.Resources[ResourcesType.Stone] == 5);
            Assert.IsTrue(gameGenerated.Resources[ResourcesType.Wood] == 5);
        }

        #endregion

    }
}