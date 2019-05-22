using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Common.Enums;
using Common.Helpers;
using Common.Interfaces;
using Common.Struct;

namespace AoC.DataLayer.Descriptors
{
    public class WorkerDescriptor
    {
        #region Properties

        [XmlIgnore]
        public ConcurrentQueue<IProductable> ProductionQueue
        {
            get; set;
        }

        public SerializableDictionary<ResourcesType, int> Cost
        {
            get;
        }

        public SerializableDictionary<ResourcesType, int> HoldedResources { get; set; }
        public int FetchingBuildingId { get; set; }
        public int LifePointsMax { get; set; }
        public int LifePoints { get; set; }
        public int AttackPower { get; set; }
        public Coordinates Position { get; set; }
        public bool Available { get; set; }
        public int Id { get; set; }
        public int PopulationSlots { get; set; }
        public bool IsWorking { get; set; }

        #endregion
    }
}
