using Common.Struct;
using System.Collections.Generic;
using Common.Enums;
using Common.Helpers;
using AoC.Common.Interfaces;

namespace AoC.Api.Domain
{

    public abstract class PassiveBuilding : IPassiveBuilding
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Coordinates Position { get; set; }
        public ResourcesType Resource { get; set; }
        public int CollectQty { get; set; }
        public int FetchTimeEllapse { get; set; }
        public SerializableDictionary<ResourcesType, int> Stock { get; set; }
        public Coordinates RallyPoint { get; set; }


        protected PassiveBuilding(string name, Coordinates position, ResourcesType resource, int collectQty, int fetchTime)
        {
            Name = name;
            Position = position;
            Resource = resource;
            CollectQty = collectQty;
            FetchTimeEllapse = fetchTime;
            Stock = ResourceHelper.CreateEmptyResourcesDictionary();
        }

        public PassiveBuilding() { }

        public virtual bool DestroyBuilding()
        {
            return true;
        }

        public abstract KeyValuePair<ResourcesType, int> Remove(int quantityToCollect);

    }
}
