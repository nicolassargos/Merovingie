using Common.Enums;
using Common.Helpers;
using Common.Struct;
using System;
using System.Collections.Generic;

namespace AoC.Api.Domain
{
    public class GoldMine : PassiveBuilding
    {
        #region Propriétés
        #endregion


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="position"></param>
        /// <param name="Quantity"></param>
        public GoldMine(int Id, String Name, Coordinates position, int stockQty = 2000)
            : base(Name, position, ResourcesType.Gold, 10, stockQty)
        {
            this.Id = Id;
            Stock[ResourcesType.Gold] = stockQty;
        }

        public GoldMine()
            : this(0, "GoldMine", new Coordinates() { x=0, y=0 }, 2000)
        {
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="quantityToCollect"></param>
        ///// <returns></returns>
        //public override KeyValuePair<ResourcesType, int> Remove(int quantityToCollect)
        //{
        //    int quantityCollected;

        //    // S'il ne reste pas assez, on récolte ce qu'il reste dans la mine
        //    if (Stock[ResourcesType.Gold] <= quantityToCollect)
        //    {
        //        quantityCollected = Stock[ResourcesType.Gold];
        //    }
        //    // Sinon, on retire la quantité désirée au stock de la mine
        //    else
        //    {
        //        quantityCollected = quantityToCollect;
        //    }

        //    // Retire la quantité collectée au stock
        //    Stock[ResourcesType.Gold] -= quantityCollected;
        //    // Si le stock est à 0, on détruit la mine
        //    if (Stock[ResourcesType.Gold] == 0) DestroyBuilding();

        //    // Retourne la ressource associée à la quantité collectée
        //    return new KeyValuePair<ResourcesType, int>(ResourcesType.Gold, quantityCollected);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool DestroyBuilding()
        {
            return base.DestroyBuilding();
        }
    }
}
