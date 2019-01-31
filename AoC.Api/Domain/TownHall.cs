using Common.Enums;
using Common.Helpers;
using Common.Interfaces;
using Common.Struct;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AoC.Api.Domain
{
    public class TownHall : ActiveBuilding, IBuilding, ICreator
    {
        public TownHall(string name, int lifepoints, int maxLifePoints, bool attack, SerializableDictionary<ResourcesType, int> resources) : 
            base(name, lifepoints, maxLifePoints, attack)
        {
            ProductionQueue = new ConcurrentQueue<IProductable>();
            Resources = resources;
        }

        public TownHall() : base()
        {
            ProductionQueue = new ConcurrentQueue<IProductable>();
        }

        [XmlIgnoreAttribute]
        public ConcurrentQueue<IProductable> ProductionQueue { get; set; }

    }
}
