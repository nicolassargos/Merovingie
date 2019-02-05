using AoC.Api.Domain;
using Common.Enums;
using Common.Helpers;
using Common.Struct;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Map
{
    public static class GameGenerator
    {
        public static IGameDescriptor GenerateMap()
        {
            TownHall townHall = new TownHall("TownHall", 100, 100, false, null);
            Carry carry = new Carry("Carry1", new Coordinates { x = 50, y = 50 });
            Tree tree = new Tree("Tree1", new Coordinates { x = 30, y = 30 });
            GoldMine mine = new GoldMine("Gold mine1", new Coordinates { x = 30, y = 40 });
            Farm farm1 = new Farm(0, "Farm1", new Coordinates { x = 10, y = 5 });
            Farm farm2 = new Farm(1, "Farm2", new Coordinates { x = 20, y = 5 });
            Worker worker1 = new Worker(0);
            Worker worker2 = new Worker(1);

            var Resources = new SerializableDictionary<ResourcesType, int> { { ResourcesType.Gold, 1000 }, { ResourcesType.Stone, 1000 }, { ResourcesType.Wood, 1000 } };

            IGameDescriptor game = new GameDescriptor();

            game.TownHalls.Add(townHall);
            game.Carries.Add(carry);
            game.Trees.Add(tree);
            game.GoldMines.Add(mine);
            game.Farms.Add(farm1);
            game.Farms.Add(farm2);
            game.Workers.Add(worker1);
            game.Workers.Add(worker2);
            game.Resources = Resources;

            return game;
        }
    }
}
