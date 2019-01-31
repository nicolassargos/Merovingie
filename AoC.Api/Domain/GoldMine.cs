using AoC.BLL;
using Common.Enums;
using Common.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Api.Domain
{
    public class GoldMine : PassiveBuilding
    {
        #region Propriétés
        public int GoldStock { get; set; }
        #endregion


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="position"></param>
        /// <param name="Quantity"></param>
        public GoldMine(String Name, Coordinates position, int stockQty = 2000)
            : base(Name, position, ResourcesType.Gold, 10)
        {
            GoldStock = stockQty;
        }

        public GoldMine() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quantityToCollect"></param>
        /// <returns></returns>
        public override KeyValuePair<ResourcesType, int> Remove(int quantityToCollect)
        {
            int quantityCollected;

            // S'il ne reste pas assez, on récolte ce qu'il reste dans la mine
            if (GoldStock <= quantityToCollect)
            {
                quantityCollected = GoldStock;
            }
            // Sinon, on retire la quantité désirée au stock de la mine
            else
            {
                quantityCollected = quantityToCollect;
            }

            // Retire la quantité collectée au stock
            GoldStock -= quantityCollected;
            // Si le stock est à 0, on détruit la mine
            if (GoldStock == 0) DestroyBuilding();

            // Retourne la ressource associée à la quantité collectée
            return new KeyValuePair<ResourcesType, int>(ResourcesType.Gold, quantityCollected);
        }

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
