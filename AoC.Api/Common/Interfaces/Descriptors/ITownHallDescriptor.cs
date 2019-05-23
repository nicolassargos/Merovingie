using System.Collections.Concurrent;
using AoC.Common.Interfaces;
using Common.Struct;

namespace AoC.Common.Interfaces.Descriptors
{
    public interface ITownHallDescriptor : IActiveBuildingDescriptor
    {
        #region TownHall Concrete
        ConcurrentQueue<IProductable> ProductionQueue { get; set; }
        Coordinates RallyPoint { get; set; }
        #endregion
    }
}
