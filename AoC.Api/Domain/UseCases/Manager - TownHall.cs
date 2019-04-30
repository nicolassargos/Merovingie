using AoC.Api.Domain;
using AoC.Api.EventArgs;
using Common.Exceptions;
using Common.Interfaces;
using System;
using System.Linq;


namespace AoC.Api.Domain.UseCases
{
    public partial class GameManager
    {
        // Town Hall
        #region TownHall

        /// <summary>
        /// Create Worker
        /// </summary>
        /// <param name="creator"></param>
        /// <returns></returns>
        public void CreateWorker(ICreator creator)
        {
            try
            {
                var worker = new Worker();
                CheckFreeSlotInPopulation(worker);
                RemoveResourcesFromStock(worker);
                //Generator.CreateEntity(creator, worker, ValidateWorkerCreation);
            }
            catch (Exception)
            {
                throw;
            }
        }
        


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Worker"></param>
        public void ValidateWorkerCreation(IProductable worker)
        {
            (worker as Worker).Id = GetNewUnitId();
            AddWorkerToList(worker as Worker);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Worker"></param>
        private void AddWorkerToList(Worker worker)
        {
            // Vérifie si le nombre de slots de populaton est suffisant 
            // avant de créer l'unité
            try
            {
                if (CheckFreeSlotInPopulation(worker))
                {
                    PopulationList.Add(worker);
                    PopulationChanged(this, new PopulationChangedEventArgs { CurrentPopulation = PopulationList.Sum(x => x.PopulationSlots), Unit = worker });
                }
            }
            
            catch (NotEnoughUnitSlotsAvailableException)
            {
                throw;
            }
        }

        #endregion
    }
}
