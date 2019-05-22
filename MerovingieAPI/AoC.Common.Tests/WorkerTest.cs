using System;
using System.Threading;
using AoC.Api.Domain;
using Common.Helpers;
using Common.Enums;
using Common.Struct;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AoC.Domain.Tests
{
    [TestClass]
    public class WorkerTest
    {
        #region Constructor

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_4ArgumentsThrowsArgumentException_IfLifepointsAre0()
        {
            new Worker(0, false, new SerializableDictionary<ResourcesType, int>(), new Coordinates { x = 50, y = 50 });
        }


        [TestMethod]
        public void Constructor_4ArgumentsOmitted_Ok()
        {
            //
            var worker = new Worker(50, false, null, new Coordinates { x = 0, y = 0 });

            //
            Assert.AreEqual(false, worker.IsWorking);
            Assert.IsNotNull(worker.HoldedResources);
            Assert.AreEqual(3, worker.HoldedResources.Count);
            Assert.IsNotNull(worker.Position);
            Assert.AreEqual(0, worker.Position.x);
            Assert.AreEqual(0, worker.Position.y);
        }


        [TestMethod]
        public void Constructor_4ArgumentsInitializes_Ok()
        {
            var worker = new Worker(
                            50, false, 
                            new SerializableDictionary<ResourcesType, int>
                            {
                                { ResourcesType.Gold, 50 }, { ResourcesType.Stone, 100 }, { ResourcesType.Wood, 200 }
                            }, 
                            new Coordinates { x = 30, y = 60 }
                        );

            // Assert
            Assert.AreEqual(50, worker.LifePoints);
            Assert.AreEqual(false, worker.IsWorking);
            Assert.AreEqual(50, worker.HoldedResources[ResourcesType.Gold]);
            Assert.AreEqual(100, worker.HoldedResources[ResourcesType.Stone]);
            Assert.AreEqual(200, worker.HoldedResources[ResourcesType.Wood]);
            Assert.AreEqual(30, worker.Position.x);
            Assert.AreEqual(60, worker.Position.y);
        }

        [TestMethod]
        public void Constructor_0Arguments_Ok()
        {
            var worker = new Worker();

            // Assert
            Assert.AreEqual(1, worker.AttackPower);

            Assert.AreEqual(50, worker.LifePoints);
            Assert.AreEqual(50, worker.LifePointsMax);

            Assert.AreEqual(1, worker.PopulationSlots);
            Assert.AreEqual(false, worker.IsWorking);
            Assert.AreEqual(null, worker.FetchingBuilding);
            Assert.IsNotNull(worker.ProductionQueue);

            Assert.IsNotNull(worker.Cost);
            Assert.AreEqual(50, worker.Cost[ResourcesType.Gold]);
            Assert.AreEqual(1, worker.Cost.Count);

            Assert.IsNotNull(worker.HoldedResources);
            Assert.AreEqual(0, worker.HoldedResources[ResourcesType.Gold]);
            Assert.AreEqual(0, worker.HoldedResources[ResourcesType.Stone]);
            Assert.AreEqual(0, worker.HoldedResources[ResourcesType.Wood]);

            Assert.AreEqual(0, worker.Position.x);
            Assert.AreEqual(0, worker.Position.y);
        }

        #endregion


        #region LaunchActions

        /// <summary>
        /// Teste si le worker collecte bien de l'or
        /// </summary>
        [TestMethod]
        public void FetchResource_FetchesResource_Ok()
        {
            //
            var worker = new Worker();
            var mine = new GoldMine("mine", new Coordinates { x = 10, y = 10 }, 2000);

            //
            Assert.AreEqual(0, worker.HoldedResources[ResourcesType.Gold]);
            worker.FetchResource(mine);

            //
            Thread.Sleep(mine.FetchTimeEllapse + 500);
            Assert.IsTrue(worker.HoldedResources[ResourcesType.Gold] > 0);
        }

        /// <summary>
        /// Teste si le worker collecte bien du bois puis s'en décharge
        /// </summary>
        [TestMethod]
        public void FetchResource_AddsResources_Ok()
        {
            //
            var worker = new Worker();
            var tree = new Tree("tree", new Coordinates { x = 10, y = 10 }, 100);

            //
            Assert.AreEqual(0, worker.HoldedResources[ResourcesType.Wood]);
            worker.FetchResource(tree);

            //
            Thread.Sleep(tree.FetchTimeEllapse + 500);
            Assert.IsTrue(worker.HoldedResources[ResourcesType.Wood] > 0);
        }

        /// <summary>
        /// Vérifie que l'événement OnResourceFetched est bien déclenché
        /// en changeant la valeur d'un booléen à true
        /// </summary>
        [TestMethod]
        public void FetchResource_TriggersEventOnResourceFetched_Ok()
        {
            //
            var worker = new Worker();
            var carry = new Carry("tree", new Coordinates { x = 10, y = 10 }, 2000);
            bool isTriggered = false;

            //
            Assert.AreEqual(0, worker.HoldedResources[ResourcesType.Stone]);
            // Trigger déclenché
            worker.ResourceCollected += (obj, args) => isTriggered = true;
            worker.FetchResource(carry);

            //
            Thread.Sleep(carry.FetchTimeEllapse + 500);
            Assert.IsTrue(worker.HoldedResources[ResourcesType.Stone] > 0);
            Assert.IsTrue(isTriggered);
        }


        /// <summary>
        /// Teste si le worker se décharge bien du bois qu'il porte
        /// </summary>
        [TestMethod]
        public void ReleaseResource_FreesResources_Ok()
        {
            //
            var worker = new Worker();
            worker.HoldedResources[ResourcesType.Wood] = 20;

            //
            worker.ReleaseResources();

            //
            Assert.AreEqual(0, worker.HoldedResources[ResourcesType.Wood]);
        }

        #endregion


        #region CancelActions

        [TestMethod]
        public void CancelAllActions_EmptiesProductionQueue_Ok()
        {
            //
            var worker = new Worker();

            //
            worker.LaunchProduction(new Farm(), (f) => { });
            worker.LaunchProduction(new Farm(), (f) => { });
            worker.LaunchProduction(new Farm(), (f) => { });

            Assert.AreEqual(3, worker.ProductionQueue.Count);
            Assert.IsTrue(worker.IsWorking);

            worker.CancelAllActions();

            //
            Assert.AreEqual(0, worker.ProductionQueue.Count);
            Assert.IsFalse(worker.IsWorking);
        }


        [TestMethod]
        public void CancelAllActions_CancelsAllFetchActions_Ok()
        {
            //
            var worker = new Worker();

            //
            worker.FetchResource(new GoldMine());

            Assert.IsTrue(worker.IsWorking);

            worker.CancelAllActions();

            //
            Assert.AreEqual(0, worker.ProductionQueue.Count);
            Assert.IsFalse(worker.IsWorking);
        }



        #endregion

    }
}
