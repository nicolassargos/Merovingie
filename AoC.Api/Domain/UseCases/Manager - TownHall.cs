using AoC.Api.Domain;
using AoC.Api.Services;
using AoC.Api.EventArgs;
using Common.Exceptions;
using Common.Interfaces;
using Common.Struct;
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
                creator.LaunchProduction(worker, ValidateWorkerCreation);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Creates worker from a building Id
        /// TODO: recupérer le building depuis la liste des buildings via son id
        /// </summary>
        /// <param name="creatorId"></param>
        public void CreateWorker(int creatorId, int positionX, int positionY)
        {
            // TODO: remplacer par ce code quand on charge 
            // correctement la partie depuis un fichier XML
            // this.BuildingList.FirstOrDefault(bld => bld.Id == creatorId);

            var creator = this.BuildingList.OfType<TownHall>().FirstOrDefault();

            try
            {
                var position = new Coordinates { x = positionX, y = positionY };
                var worker = new Worker(position);
                CheckFreeSlotInPopulation(worker);
                RemoveResourcesFromStock(worker);
                creator.LaunchProduction(worker, ValidateWorkerCreation);
            }
            catch (Exception ex)
            {
                throw ex;
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
                throw new NotEnoughUnitSlotsAvailableException();
            }
        }

        #endregion
    }
}
