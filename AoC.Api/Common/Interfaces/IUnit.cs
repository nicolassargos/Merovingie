using Common.Struct;

namespace AoC.Common.Interfaces
{
    public interface IUnit
    {
        int Id { get; set; }
        int LifePointsMax { get; set; }
        int LifePoints { get; set; }
        int AttackPower { get; set; }
        Coordinates Position { get; set; }
        int PopulationSlots { get; set; }
    }
}
