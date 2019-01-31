using Common.Enums;
using Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IProductable
    {
        int Time { get; }
        SerializableDictionary<ResourcesType , int> Cost { get; }

    }
}
