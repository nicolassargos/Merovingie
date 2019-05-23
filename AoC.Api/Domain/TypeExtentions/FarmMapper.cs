using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoC.Api.Domain;
using AoC.Common.Descriptors;
using AutoMapper;

namespace AoC.Domain.TypeExtentions
{
    public static class FarmMapper
    {
        public static FarmDescriptor ToFarmDescriptor(this Farm farm)
        {
            return Mapper.Map<FarmDescriptor>(farm);
        }

        public static Farm ToFarm(this FarmDescriptor farmDescriptor)
        {
            return Mapper.Map<Farm>(farmDescriptor);
        }
    }
}
