using AoC.Api.Domain;
using Common.Enums;
using Common.Helpers;
using System.Collections.Generic;

namespace Domain
{
    public interface IGameDescriptor
    {
        List<Carry> Carries { get; set; }
        List<Tree> Trees { get; set; }
        List<GoldMine> GoldMines { get; set; }
        List<TownHall> TownHalls { get; set; }
        List<Farm> Farms { get; set; }
        List<Worker> Workers { get; set; }
        SerializableDictionary<ResourcesType, int> Resources { get; set; }
    }
}