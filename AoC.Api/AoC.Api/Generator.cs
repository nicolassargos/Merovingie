using Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace AoC.Api.Services
{
    public class Generator
    {
        private ICreator _creator;

        public Generator(ICreator creator)
        {
            _creator = creator;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="creator"></param>
        /// <param name="productable"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>
        public bool CreateEntity(IProductable productable, Action<IProductable> callBack)
        {
            if (productable == null) throw new ArgumentNullException("CreateEntity: productable is null");

            // Récupérer les ressources necessaires à la production
            var productionResources = productable.Cost;

            //Todo : Calculer la somme totale des ressources.

            // Si OK => Créer l'entité
            AddToProductionQueue(productable, callBack);

            // Ajouter au fil de production
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="creator"></param>
        /// <param name="productable"></param>
        /// <param name="callBack"></param>
        public void AddToProductionQueue(IProductable productable, Action<IProductable> callBack)
        {
            if (_creator == null) throw new ArgumentNullException("AddToProductionQueue: Creator is null");
            if (productable == null) throw new ArgumentNullException("AddToProductionQueue: Productable is null");


            bool taskStarted = _creator.ProductionQueue.Count > 0;
            _creator.ProductionQueue.Enqueue(productable);

            if (taskStarted == false)
            {
                ProductQueueLauncher(_creator.ProductionQueue, callBack);
            }
        }

        public static void ProductQueueLauncher(ConcurrentQueue<IProductable> Queue, Action<IProductable> callBack)
        {
            IProductable productable;

            Task.Run(() =>
            {
                while (Queue.Count > 0)
                {
                    Queue.TryDequeue(out productable);
                    Thread.Sleep(productable.Time);
                    callBack(productable);
                }
            });
        }
    }
}
