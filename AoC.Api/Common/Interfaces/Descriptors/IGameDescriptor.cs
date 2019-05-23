using Common.Enums;
using Common.Helpers;
using System.Collections.Generic;

namespace AoC.Common.Interfaces.Descriptors
{
    public interface IGameDescriptor
    {
        List<ICarryDescriptor> Carries { get; set; }
        List<ITreeDescriptor> Trees { get; set; }
        List<IGoldMineDescriptor> GoldMines { get; set; }
        List<ITownHallDescriptor> TownHalls { get; set; }
        List<IFarmDescriptor> Farms { get; set; }
        List<IWorkerDescriptor> Workers { get; set; }
        SerializableDictionary<ResourcesType, int> Resources { get; set; }
        int MaxPopulation { get; set; }
        int ActualPopulation { get; set; }

    }
}