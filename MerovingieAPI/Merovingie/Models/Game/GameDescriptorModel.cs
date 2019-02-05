using AoC.Api.Domain;
using Common.Enums;
using Common.Helpers;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Merovingie.Models.Game
{
    public class GameDescriptorModel: IGameDescriptor
    {
        public List<Carry> Carries { get; set; }
        public List<Tree> Trees { get; set; }
        public List<GoldMine> GoldMines { get; set; }

        public List<TownHall> TownHalls { get; set; }
        public List<Farm> Farms { get; set; }

        public List<Worker> Workers { get; set; }

        public SerializableDictionary<ResourcesType, int> Resources { get; set; }
    }
}
