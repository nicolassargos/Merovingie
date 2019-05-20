using AoC.Common.Interfaces;
using Common.Enums;
using Common.Helpers;
using Common.Struct;

namespace Common.BasicDescriptors
{
    public class BuildingDescriptor : IBuilding
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Coordinates Position { get; set; }
        public SerializableDictionary<ResourcesType, int> Stock { get; set; }
        public Coordinates RallyPoint { get; set; }
    }
}
