using Common.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
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
