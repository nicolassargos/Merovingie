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
using AoC.Api.Services;
using AoC.Common.Interfaces;

namespace AoC.Api.Domain
{
    public class TownHall : ActiveBuilding, IBuilding, ICreator
    {
        public ConcurrentQueue<IProductable> ProductionQueue { get; set; }
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
