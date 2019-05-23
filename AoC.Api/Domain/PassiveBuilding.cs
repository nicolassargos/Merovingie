using Common.Interfaces;
using Common.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;
using Common.Helpers;
using AoC.Api.Domain.EventArgs;

namespace AoC.Api.Domain
{


    public class PassiveBuilding : IBuilding
    {
        public event EventHandler<ResourcesFetchedArgs> BuildingStockChanged;

        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public Coordinates Position { get; set; }
        public ResourcesType Resource { get; set; }
        public int CollectQty { get; set; }
        public int FetchTimeEllapse { get; set; }
        public SerializableDictionary<ResourcesType, int> Stock { get; set; }
        public Coordinates RallyPoint { get; set; }

        #endregion


        #region Constructor
        protected PassiveBuilding(string name, Coordinates position, ResourcesType resource, int collectQty, int fetchTime)
        {
            Name = name;
            Position = position;
            Resource = resource;
            CollectQty = collectQty;
            FetchTimeEllapse = fetchTime;
            Stock = ResourceHelper.CreateEmptyResourcesDictionary();
        }
        public PassiveBuilding() { }

        #endregion


        #region Methods
        public virtual bool DestroyBuilding()
        {
            return true;
        }

        public KeyValuePair<ResourcesType, int> Remove(KeyValuePair<ResourcesType, int> resourcesToCollect)
        {
            int collectedQuantity;

            // S'il ne reste pas assez, on récolte ce qu'il reste dans le bâtiment
            if (Stock[resourcesToCollect.Key] <= resourcesToCollect.Value)
            {
                collectedQuantity = Stock[resourcesToCollect.Key];
            }
            // Sinon, on retire la quantité désirée au stock du bâtiment
            else
            {
                collectedQuantity = resourcesToCollect.Value;
            }

            // Retire la quantité collectée au stock
            Stock[ResourcesType.Stone] -= collectedQuantity;
            // Si le stock est à 0, on détruit la mine
            if (Stock[ResourcesType.Stone] == 0) DestroyBuilding();

            // Signale à l'UI que le stock a changé
            OnBuildingStockChanged(new ResourcesFetchedArgs
            {
                buildingId = this.Id,
                resources = ResourceHelper.GetResourcesCollected(resourcesToCollect.Key, Stock[resourcesToCollect.Key])
            });

            // Retourne la ressource associée à la quantité collectée
            return new KeyValuePair<ResourcesType, int>(resourcesToCollect.Key, collectedQuantity);
        }

        protected virtual void OnBuildingStockChanged(ResourcesFetchedArgs e)
        {
            BuildingStockChanged?.Invoke(this, e);
        }
        #endregion



    }
}
