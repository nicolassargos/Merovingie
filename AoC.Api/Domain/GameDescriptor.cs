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
    public class GameDescriptor : IGameDescriptor
    {
        #region Properties

        [XmlArrayItem]
        public List<Carry> Carries { get; set; }
        public List<Tree> Trees { get; set; }
        public List<GoldMine> GoldMines { get; set; }

        public List<TownHall> TownHalls { get; set; }
        public List<Farm> Farms { get; set; }

        public List<Worker> Workers { get; set; }

        public SerializableDictionary<ResourcesType, int> Resources { get; set; }

        #endregion

        public GameDescriptor()
        {
            Carries = new List<Carry>();
            Trees = new List<Tree>();
            GoldMines = new List<GoldMine>();

            TownHalls = new List<TownHall>();
            Farms = new List<Farm>();

            Workers = new List<Worker>();

            Resources = new SerializableDictionary<ResourcesType, int>();
        }
    }
}
