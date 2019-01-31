using Common.Enums;
using Common.Helpers;
using Common.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Api.Domain
{
    public class MiltaryHall : ICreator, IProductable
    {
        public ConcurrentQueue<IProductable> ProductionQueue
        {
            get; set;
        }
        public int Time { get => 15000; }

        public SerializableDictionary<ResourcesType, int> Cost
        {
            get => new SerializableDictionary<ResourcesType, int> { { ResourcesType.Gold, 250 } };
        }
    }
}
