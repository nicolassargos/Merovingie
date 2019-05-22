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
    public static class GoldMineMapper
    {
        public static GoldMineDescriptor ToGoldMineDescriptor(this GoldMine goldMine)
        {
            return Mapper.Map<GoldMineDescriptor>(goldMine);
        }

        public static GoldMine ToGoldMine(this GoldMineDescriptor goldMineDescriptor)
        {
            return Mapper.Map<GoldMine>(goldMineDescriptor);
        }
    }
}
