using AoC.Common.Interfaces;
using Common.Enums;

namespace Common.BasicDescriptors
{
    public class PassiveBuildingDescriptor : BuildingDescriptor
    {
        public int CollectQty { get; set; }
        public int FetchTimeEllapse { get; set; }
        public ResourcesType Resource { get; set; }
    }
}
