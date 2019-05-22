using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoC.Api.Domain;
using AoC.DataLayer.Descriptors;
using AutoMapper;

namespace AoC.Domain.TypeExtentions
{
    public static class TownHallMapper
    {
        public static TownHallDescriptor ToTownHallDescriptor(this TownHall townHall)
        {
            return Mapper.Map<TownHallDescriptor>(townHall);
        }

        public static TownHall ToTownHall(this TownHallDescriptor townHallDescriptor)
        {
            return Mapper.Map<TownHall>(townHallDescriptor);
        }
    }
}
