using AoC.Api.Domain;
using Common.Enums;
using Common.Helpers;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Merovingie.Models.Game
{
    public class GameDescriptorModel
    {
        public string Name { get; set; }
        public int Farms { get; set; }

        public int Workers { get; set; }

        public SerializableDictionary<ResourcesType, int> Resources { get; set; }
    }
}
