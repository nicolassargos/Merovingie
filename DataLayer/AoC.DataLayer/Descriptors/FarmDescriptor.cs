using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;
using Common.Helpers;

namespace AoC.DataLayer.Descriptors
{
    public class FarmDescriptor : ActiveBuildingDescriptor
    {
        public int Time { get; set; }

        public SerializableDictionary<ResourcesType, int> Cost { get; set; }

        public int PopulationIncrement { get; set; }
    }
}
