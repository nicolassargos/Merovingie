using AoC.Common.Interfaces;
using Common.BasicDescriptors;
using Common.Enums;
using Common.Helpers;

namespace Common.AdvancedDescriptors
{
    public class WorkerDescriptor : UnitDescriptor, IUnit
    {
        public SerializableDictionary<ResourcesType, int> HoldedResources { get; set; }
        public bool Available { get; set; }
        public bool IsWorking { get; set; }
    }
}
