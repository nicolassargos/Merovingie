using Common.Enums;
using Common.Helpers;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using AoC.Common.Interfaces;
using Common.AdvancedDescriptors;

namespace AoC.Api.Domain

{
    [Serializable]
    [XmlRoot]
    public class GameDescriptor : IGameDescriptor
    {
        #region Properties

        [XmlArrayItem]
        public List<CarryDescriptor> Carries { get; set; }
        public List<TreeDescriptor> Trees { get; set; }
        public List<GoldMineDescriptor> GoldMines { get; set; }

        public List<TownHallDescriptor> TownHalls { get; set; }
        public List<FarmDescriptor> Farms { get; set; }

        public List<WorkerDescriptor> Workers { get; set; }

        public SerializableDictionary<ResourcesType, int> Resources { get; set; }

        [XmlIgnore]
        public int MaxPopulation { get => this.Farms?.Count * 4 ?? 0; }
        [XmlIgnore]
        public int ActualPopulation { get => this.Workers?.Count ?? 0; }

        #endregion

        public GameDescriptor()
        {
            Carries = new List<CarryDescriptor>();
            Trees = new List<TreeDescriptor>();
            GoldMines = new List<GoldMineDescriptor>();

            TownHalls = new List<TownHallDescriptor>();
            Farms = new List<FarmDescriptor>();

            Workers = new List<WorkerDescriptor>();

            Resources = new SerializableDictionary<ResourcesType, int>();
        }
    }
}
