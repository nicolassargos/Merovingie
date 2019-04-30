using Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace AoC.Api.Services
{
    public static class Generator
    {

        public delegate void MyDel(Action<IProductable> callback);

        public static bool CreateEntity(ICreator creator, IProductable productable, Action<IProductable> callBack)
        {
            // Récupérer les ressources necessaires à la production
            var productionResources = productable.Cost;

            //Todo : Calculer la somme totale des ressources.

            // Si OK => Créer l'entité
            AddToProductionQueue(creator, productable, callBack);

            // Ajouter au fil de production
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="creator"></param>
        /// <param name="productable"></param>
        /// <param name="callBack"></param>
        public static void AddToProductionQueue(ICreator creator, IProductable productable, Action<IProductable> callBack)
        {
            if (creator == null) throw new ArgumentNullException("AddToProductionQueue: Creator is null");
            if (productable == null) throw new ArgumentNullException("AddToProductionQueue: Productable is null");


            bool taskStarted = creator.ProductionQueue.Count > 0;
            creator.ProductionQueue.Enqueue(productable);

            if (taskStarted == false)
            {
                ProductQueueLauncher(creator.ProductionQueue, callBack);
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
