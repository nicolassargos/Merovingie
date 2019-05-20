using AoC.Common.Interfaces;
using Common.Struct;

namespace Common.BasicDescriptors
{
    public class UnitDescriptor : IUnit
    {
        public int Id { get; set; }
        public int LifePointsMax { get; set; }
        public int LifePoints { get; set; }
        public int AttackPower { get; set; }
        public Coordinates Position { get; set; }
        public int PopulationSlots { get; set; }
    }
}
