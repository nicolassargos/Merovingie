using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoC.Api.Domain;
using AoC.DataLayer.Descriptors;
using AutoMapper;

namespace Domain
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<WorkerDescriptor, Worker>().ForMember(x => x.FetchingBuilding, opt => opt.Ignore());
            CreateMap<TownHall, TownHallDescriptor>();
            CreateMap<Farm, FarmDescriptor>();
            CreateMap<Carry, CarryDescriptor>();
            CreateMap<GoldMine, GoldMineDescriptor>();
            CreateMap<Tree, TreeDescriptor>();
            CreateMap<PassiveBuilding, PassiveBuildingDescriptor>();
        }
    }
}
