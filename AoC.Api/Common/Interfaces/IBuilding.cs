using Common.Enums;
using Common.Helpers;
using Common.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AoC.Common.Interfaces
{
    public interface IBuilding
    {
        int Id { get; set; }
        string Name { get; set; }
        Coordinates Position { get; set; }
        SerializableDictionary<ResourcesType, int> Stock { get; set; }
        Coordinates RallyPoint { get; set; }

    }
}
