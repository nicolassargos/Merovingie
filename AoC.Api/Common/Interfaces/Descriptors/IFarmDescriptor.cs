using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;
using Common.Helpers;

namespace AoC.Common.Interfaces.Descriptors
{
    public interface IFarmDescriptor : IActiveBuildingDescriptor
    {
        int Time { get; set; }

        SerializableDictionary<ResourcesType, int> Cost { get; set; }

        int PopulationIncrement { get; set; }
    }
}
