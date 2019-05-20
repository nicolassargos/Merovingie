using System.Collections.Generic;
using Common.Enums;
using Common.Helpers;
using Common.Interfaces;
using Common.Struct;

namespace AoC.Common.Interfaces
{
    public interface IPassiveBuilding : IBuilding
    {
        #region Properties

        int CollectQty { get; set; }
        int FetchTimeEllapse { get; set; }
        ResourcesType Resource { get; set; }

        #endregion


        #region Methods

        bool DestroyBuilding();
        KeyValuePair<ResourcesType, int> Remove(int quantityToCollect);

        #endregion

    }
}