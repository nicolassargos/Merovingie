using Common.Enums;
using Common.Helpers;
using AoC.Common.Interfaces;
using Common.Struct;
using System;
using System.Collections.Concurrent;
using System.Xml.Serialization;
using AoC.Api.Services;

namespace AoC.Api.Domain
{
    public class TownHall : ActiveBuilding, IBuilding, ICreator
    {
        [XmlIgnoreAttribute]
        public ConcurrentQueue<IProductable> ProductionQueue { get; set; }
        public Coordinates RallyPoint { get; set; }
        private Generator _generator;


        #region Constructor
        // TODO: initialiser les propriétés
        public TownHall(string name, int lifepoints, int maxLifePoints, bool attack, SerializableDictionary<ResourcesType, int> resources,
            Coordinates? position) :
            base(name, lifepoints, maxLifePoints, attack)
        {
            ProductionQueue = new ConcurrentQueue<IProductable>();
            Stock = resources;
            Position = position.GetValueOrDefault();
        }

        public TownHall() : base()
        {
            ProductionQueue = new ConcurrentQueue<IProductable>();
            _generator = new Generator(this);
        }

        #endregion

        public void LaunchProduction(IProductable productable, Action<IProductable> callBack)
        {
            _generator.CreateEntity(productable, callBack);
        }
    }
}
