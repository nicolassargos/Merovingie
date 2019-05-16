using Common.Interfaces;
using Common.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;

namespace AoC.Api.Domain
{


    public abstract class PassiveBuilding : IBuilding
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Coordinates Position { get; set; }
        public ResourcesType Resource { get; set; }
        public int CollectQty { get; set; }
        public int FetchTimeEllapse { get; protected set; }


        protected PassiveBuilding(string name, Coordinates position, ResourcesType resource, int collectQty)
        {
            Name = name;
            Position = position;
            Resource = resource;
            CollectQty = collectQty;
        }

        public PassiveBuilding() { }

        public virtual bool DestroyBuilding()
        {
            return true;
        }

        public abstract KeyValuePair<ResourcesType, int> Remove(int quantityToCollect);

    }
}
