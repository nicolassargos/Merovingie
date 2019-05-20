using Common.Enums;
using Common.Helpers;
using Common.Interfaces;
using Common.Struct;

namespace AoC.Common.Interfaces
{
    public interface IActiveBuilding : IBuilding
    {
        #region Properties
        bool Attack { get; set; }
        int LifePoints { get; set; }
        int MaxLifePoints { get; set; }
        #endregion


        #region Methods
        bool CreateBuilding();
        bool DestroyBuilding();
        #endregion
    }
}