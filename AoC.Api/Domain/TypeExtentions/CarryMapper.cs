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
    public static class CarryMapper
    {
        public static CarryDescriptor ToCarryDescriptor(this Carry carry)
        {
            return Mapper.Map<CarryDescriptor>(carry);
        }

        public static Carry ToCarry(this CarryDescriptor carryDescriptor)
        {
            return Mapper.Map<Carry>(carryDescriptor);
        }
    }
}
