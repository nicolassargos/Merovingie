using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AoC.Api.Services;
using AoC.Api.Domain;
using System.Threading.Tasks;
using System.Threading;

namespace AoC.Services.Tests
{
    [TestClass]
    public class GeneratorTest
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddToProductionQueue_ThrowsArgumentNullException_IfProductableIsNull()
        {
            var worker = new Worker();
            worker.LaunchProduction(null, ((f) => { }));
        }

        [TestMethod]
        public void AddToProductionQueue_ExecutesCallback_Ok()
        {
            bool hasBeenCalled = false;
            var worker = new Worker();
            var farm = new Farm();

            var thread = new Thread(() =>
            {
                worker.LaunchProduction(farm, ((f) => { hasBeenCalled = true; }));
            });
            thread.Start();
            thread.Join();

            Console.WriteLine(hasBeenCalled);

            Assert.IsTrue(hasBeenCalled);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qty"></param>
        [DataTestMethod]
        [DataRow(8)]
        [DataRow(3)]
        [DataRow(1)]
        [DataRow(5)]
        public void ProductQueueLauncher_ExecutesNCallbackGivingANumberOfTasks_Ok(int qty)
        {
            var worker = new Worker();
            var farm = new Farm();
            int counter = 0;

            var thread = new Thread(() =>
            {
                for (int i = 0; i < qty; i++)
                {
                    worker.ProductionQueue.Enqueue(farm);
                    Generator.ProductQueueLauncher(worker.ProductionQueue,
                        ((f) => {
                            Interlocked.Increment(ref counter);
                            Console.WriteLine(counter);
                        }));
                }
            });

            thread.Start();
            thread.Join(qty*(farm.Time + 500));


            Console.WriteLine(counter.ToString());

            Assert.AreEqual(qty, counter);
        }
    }
}
