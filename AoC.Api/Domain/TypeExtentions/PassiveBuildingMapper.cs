using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoC.Api.Domain;
using AoC.Common.Descriptors;
using AutoMapper;

namespace Domain.TypeExtentions
{
    public static class PassiveBuildingMapper
    {
        public static PassiveBuildingDescriptor ToPassiveBuildingDescriptor(this PassiveBuilding goldMine)
        {
            return Mapper.Map<PassiveBuildingDescriptor>(goldMine);
        }

        public static PassiveBuilding ToPassiveBuilding(this PassiveBuildingDescriptor goldMineDescriptor)
        {
            return Mapper.Map<PassiveBuilding>(goldMineDescriptor);
        }
    }
}
