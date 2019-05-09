using AoC.Api.EventArgs;
using AoC.Api.Services;
using Common.Interfaces;
using System;
using System.Linq;

namespace AoC.Api.Domain.UseCases
{
    public partial class GameManager
    {
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

                var farm = new Farm("NewFarm", new Common.Struct.Coordinates { x = 0, y = 0 });
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
        public void FetchResource(int workerId, PassiveBuilding building)
        {
            // TODO: faire un try/catch pour valider le workerId comme étant un Id de Worker
            var worker = PopulationList.FirstOrDefault(x => x.Id == workerId) as Worker;

            // Annule la tâche en cours
            CancelTask(workerId);

            // Créé la tâche d'aller chercher des ressources
            worker.FetchResource(building);

            // Ajoute les ressources collectées au stock
            worker.ResourceFetched += AddResourcesToStock;
        }


        /// <summary>
        /// Ajoute un dictionnaire de ressources au stock
        /// et le transmet à l'UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddResourcesToStock(object sender, ResourcesFetchedArgs e)
        {
            if (e.ResourcesFetched != null)
            {
                foreach (var resource in e.ResourcesFetched)
                {
                    Resources[resource.Key] += resource.Value;
                }
                ResourcesChanged(this, new ResourcesChangedArgs { CurrentResources = Resources });
            }
        }


        /// <summary>
        /// Annule toutes les tâches en cours d'un worker 
        /// </summary>
        /// <param name="workerId"></param>
        public void CancelTask(int workerId)
        {
            // TODO: faire un try/catch pour valider le workerId comme étant un Id de Worker
            var worker = PopulationList[workerId] as Worker;

            if (worker.IsWorking) worker.CancelAllActions();
        }
        #endregion
    }
}
