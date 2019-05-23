using AoC.Api.Domain.EventArgs;
using Common.Enums;
using Common.Helpers;
using AoC.Common.Interfaces;
using System;
using System.Linq;
using Common.Struct;

namespace AoC.Api.Domain.UseCases
{
    public partial class GameManager
    {
        // Evenement déclenché lors de la fin de la collecte de ressources par un worker
        public event EventHandler<ResourcesFetchedArgs> WorkerCompletedCollect;

        // Evenement déclenché lors de libération des ressources au stock (retour à la base)
        public event EventHandler<ResourcesReleasedArgs> WorkerCompletedBringback;

        // Worker
        #region Worker

        /// <summary>
        /// 
        /// </summary>
        /// <param name="creator"></param>
        public bool CreateFarm(int unitId)
        {
            // Vérifie si un Worker est libre pour créer la ferme
            try
            {
                var worker = PopulationList[unitId] as Worker;

                // Annule les tâches courantes
                CancelTask(unitId);

                var farm = new Farm("NewFarm", new Coordinates { x = 0, y = 0 });
                RemoveResourcesFromStock(farm);
                worker.LaunchProduction(farm, ValidateFarmCreation);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="farm"></param>
        private void ValidateFarmCreation(IProductable farm)
        {
            IncreasePopulationLimit(farm as Farm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="farm"></param>
        private void IncreasePopulationLimit(Farm farm)
        {
            MaxPopulation += farm.PopulationIncrement;
            MaxPopulationChanged(this, new MaxPopulationChangedArgs { CurrentMaxPopulation = MaxPopulation });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="resource"></param>
        public void FetchResource(int workerId, int buildingId)
        {
            if (buildingId == 0 || workerId == 0) throw new ArgumentNullException("Manager FetchResource: workerId or buildingId is missing");

            // TODO: faire un try/catch pour valider le workerId comme étant un Id de Worker
            var worker = PopulationList.FirstOrDefault(wk => wk.Id == workerId) as Worker;
            var building = BuildingList.FirstOrDefault(bld => bld.Id == buildingId) as PassiveBuilding;

            if (building == null || worker == null) throw new ArgumentException("Manager FetchResource: workerId or buildingId does not exist");

            // Annule la tâche en cours
            CancelTask(workerId);

            // Créé la tâche de collecter les ressources
            worker.FetchResource(building);

            // Capte l'evénement de collecte de ressources par ce worker
            worker.ResourceCollected += OnWorkerCompletedCollect;
            // worker.ResourceFetched += AddResourcesToStock;
        }


        /// <summary>
        /// Capte la fin de la collecte de ressources par un worker
        /// et gère les actions conséquentes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWorkerCompletedCollect(object sender, ResourcesFetchedArgs e)
        {
            WorkerCompletedCollect?.Invoke(sender, e);
        }

        /// <summary>
        /// Libère les ressources d'un worker et les ajoute au stock
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        public bool ReleaseUnitResources(int unitId, int buildingId)
        {
            if (unitId == 0) throw new ArgumentOutOfRangeException("ReleaseResources: unitId is 0");
            if (buildingId == 0) throw new ArgumentOutOfRangeException("ReleaseResources: buildingId is 0");

            var worker = this.PopulationList.First(unit => unit.Id == unitId) as Worker;
            if (worker == null) throw new Exception("ReleaseResources: worker could not be found");

            // Ajoute les ressources au stock
            AddResourcesToStock(worker.HoldedResources);

            // Efface les ressources du stock de l'unité
            worker.ReleaseResources();

            // Déclenche l'événement signalant la fin de l'opération
            WorkerCompletedBringback?.Invoke(this, new ResourcesReleasedArgs { unitId = worker.Id, resources = worker.HoldedResources });

            return true;
        }

        /// <summary>
        /// Ajoute un dictionnaire de ressources au stock
        /// et le transmet à l'UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddResourcesToStock(SerializableDictionary<ResourcesType, int> resources)
        {
            if (resources != null)
            {
                foreach (var resource in resources)
                {
                    this.Resources[resource.Key] += resource.Value;
                }
                ResourcesChanged(this, new ResourcesChangedArgs { resources = this.Resources });
            }
        }


        /// <summary>
        /// Annule toutes les tâches en cours d'un worker 
        /// </summary>
        /// <param name="workerId"></param>
        public void CancelTask(int workerId)
        {
            // TODO: faire un try/catch pour valider le workerId comme étant un Id de Worker
            var worker = PopulationList.FirstOrDefault(wk => wk.Id == workerId) as Worker;

            if (worker.IsWorking) worker.CancelAllActions();
        }


        public void SetUnitPosition(int id, Coordinates coordinates)
        {
            try
            {
                var unit = PopulationList.FirstOrDefault(u => u.Id == id);
                if (unit == null || unit.Id == 0)
                {
                    unit = PopulationList.FirstOrDefault(u => u.Id == 0);
                    if (unit == null)
                        throw new Exception("SetUnitPosition: unit not found");
                    // Cas où il s'agit d'une nouvelle unité qui n'a pas encore d'Id
                    else
                        unit.Id = id;
                }

                unit.Position = coordinates;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
