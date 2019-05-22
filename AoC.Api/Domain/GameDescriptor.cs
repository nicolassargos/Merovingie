using Newtonsoft.Json;
using AoC.Api.Domain;
using Common.Enums;
using Common.Helpers;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AoC.Api.Domain

{
    [Serializable]
    [XmlRoot]
    public class GameDescriptor
    {
        #region Properties

        [XmlArrayItem]
        public List<Carry> Carries { get; set; }
        public List<Tree> Trees { get; set; }
        public List<PassiveBuilding> PassiveBuildings { get; set; }

        public List<TownHall> TownHalls { get; set; }
        public List<Farm> Farms { get; set; }

        public List<Worker> Workers { get; set; }

        public SerializableDictionary<ResourcesType, int> Resources { get; set; }

        [XmlIgnore]
        // TODO : utiliser PopulationIncrement
        public int MaxPopulation { get => this.Farms?.Count * 4 ?? 0; }
        [XmlIgnore]
        public int ActualPopulation { get => this.Workers?.Count ?? 0; }

        #endregion

        public GameDescriptor()
        {
            Carries = new List<Carry>();
            Trees = new List<Tree>();
            PassiveBuildings = new List<PassiveBuilding>();

            TownHalls = new List<TownHall>();
            Farms = new List<Farm>();

            Workers = new List<Worker>();

            Resources = new SerializableDictionary<ResourcesType, int>();
        }
    }
}
