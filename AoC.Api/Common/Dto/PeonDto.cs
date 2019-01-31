using Common.Interfaces;
using Common.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class WorkerDto
    {
        public int LifePointsMax;
        public int LifePoints;
        public int AttackPower;
        public Coordinates Position;
    }
}
