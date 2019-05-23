using AoC.Common.Descriptors;
using AoC.Common.Interfaces;
using Common.Enums;
using Common.Helpers;
using System.Collections.Generic;

namespace AoC.Common.Descriptors
{
    public class GameDescriptor : IGameDescriptor
    {
        public List<CarryDescriptor> Carries { get; set; }
        public List<TreeDescriptor> Trees { get; set; }
        public List<GoldMineDescriptor> GoldMines { get; set; }
        public List<TownHallDescriptor> TownHalls { get; set; }
        public List<FarmDescriptor> Farms { get; set; }
        public List<WorkerDescriptor> Workers { get; set; }
        public SerializableDictionary<ResourcesType, int> Resources { get; set; }
        public int MaxPopulation { get; set; }
        public int ActualPopulation { get; set; }
        public GameDescriptor() : this(0, 0)
        { }

        public GameDescriptor(int Max, int Actual)
        {
            Carries = new List<CarryDescriptor>();
            Trees = new List<TreeDescriptor>();
            GoldMines = new List<GoldMineDescriptor>();
            TownHalls = new List<TownHallDescriptor>();
            Farms = new List<FarmDescriptor>();
            Workers = new List<WorkerDescriptor>();
            Resources = new SerializableDictionary<ResourcesType, int>();
            MaxPopulation = Max;
            ActualPopulation = Actual;
        }
    }
}