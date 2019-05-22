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
    public static class TreeMapper
    {
        public static TreeDescriptor ToTreeDescriptor(this Tree tree)
        {
            return Mapper.Map<TreeDescriptor>(tree);
        }

        public static Tree ToTree(this TreeDescriptor treeDescriptor)
        {
            return Mapper.Map<Tree>(treeDescriptor);
        }
    }
}
