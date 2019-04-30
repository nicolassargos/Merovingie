using Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

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
        private static void AddToProductionQueue(ICreator creator, IProductable productable, Action<IProductable> callBack)
        {
            bool taskStarted = creator.ProductionQueue.Count > 0;
            creator.ProductionQueue.Enqueue(productable);
            if (taskStarted == false)
            {
                Task.Run(() =>
                {
                    while (creator.ProductionQueue.Count > 0)
                    {
                        Thread.Sleep(productable.Time);
                        creator.ProductionQueue.TryDequeue(out productable);
                        callBack(productable);
                    }
                });
            }
        }
    }
}
