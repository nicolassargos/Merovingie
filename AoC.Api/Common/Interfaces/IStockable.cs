using Common.Enums;
using Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Common.Interfaces
{
    public class IStockable
    {
        SerializableDictionary<ResourcesType, int> Stock;
    }
}
