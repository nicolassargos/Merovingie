using Common.Enums;
using Common.Helpers;
using Common.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class PassiveBuildingDto
    {
        public int Id;
        public string Name;
        public Coordinates Position;
        SerializableDictionary<ResourceSet, int> Resources;
    }
}
