using AoC.Api.EventArgs;
using Common.Enums;
using Common.Struct;
using Common.Helpers;
using System;
using System.Collections.Generic;

namespace AoC.Api.Domain
{
    public class Carry : PassiveBuilding
    {
        public event EventHandler<ResourcesChangedArgs> CarryStockChanged;

        #region Propriétés
        public int StoneStock { get; set; }
        public Coordinates RallyPoint { get; set; }
        #endregion


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="position"></param>
        /// <param name="Quantity"></param>
        public Carry(String Name, Coordinates position, int stockQty = 1000)
            : base(Name, position, ResourcesType.Stone, 20)
        {
            StoneStock = stockQty;
        }

        public Carry() : base() { }

        /// <summary>
        /// Retire de la mine une quantitée collectée par un worker
        /// </summary>
        /// <param name="quantityToCollect"></param>
        /// <returns></returns>
        public override KeyValuePair<ResourcesType, int> Remove(int quantityToCollect)
        {
            int quantityCollected;

            // S'il ne reste pas assez, on récolte ce qu'il reste dans la mine
            if (StoneStock <= quantityToCollect)
            {
                quantityCollected = StoneStock;
            }
            // Sinon, on retire la quantité désirée au stock de la mine
            else
            {
                quantityCollected = quantityToCollect;
            }

            // Retire la quantité collectée au stock
            StoneStock -= quantityCollected;
            // Si le stock est à 0, on détruit la mine
            if (StoneStock == 0) DestroyBuilding();

            // Signale à l'UI que le stock a changé
            OnCarryStockChanged(new ResourcesChangedArgs {
                CurrentResources = ResourceHelper.GetResourcesCollected(ResourcesType.Stone, StoneStock)});

            // Retourne la ressource associée à la quantité collectée
            return new KeyValuePair<ResourcesType, int>(ResourcesType.Stone, quantityCollected);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool DestroyBuilding()
        {
            return base.DestroyBuilding();
        }

        protected void OnCarryStockChanged(ResourcesChangedArgs args)
        {
            CarryStockChanged?.Invoke(this, args);
        }
    }
}
