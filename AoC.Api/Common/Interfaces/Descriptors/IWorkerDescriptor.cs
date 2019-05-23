using System.Collections.Concurrent;
using AoC.Common.Interfaces;
using Common.Enums;
using Common.Helpers;
using Common.Struct;

namespace AoC.Common.Interfaces.Descriptors
{
    public interface IWorkerDescriptor
    {
        #region Properties

        ConcurrentQueue<IProductable> ProductionQueue { get; set; }
        SerializableDictionary<ResourcesType, int> Cost { get; }
        SerializableDictionary<ResourcesType, int> HoldedResources { get; set; }
        int FetchingBuildingId { get; set; }
        int LifePointsMax { get; set; }
        int LifePoints { get; set; }
        int AttackPower { get; set; }
        Coordinates Position { get; set; }
        bool Available { get; set; }
        int Id { get; set; }
        int PopulationSlots { get; set; }
        bool IsWorking { get; set; }

        #endregion
    }
}
