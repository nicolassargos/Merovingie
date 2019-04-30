using Common.Helpers;
using Common.Struct;
using System.Resources;

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
