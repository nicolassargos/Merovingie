using AoC.Api.Domain;
using AoC.Common.Descriptors;
using AoC.Domain.TypeExtentions;
using Common.Enums;
using Common.Helpers;
using Common.Struct;
using System;
using System.Collections.Generic;
using AoC.Common.Interfaces;

namespace AoC.MerovingieFileManager
{
    public static class GameGenerator
    {
        public static IGameDescriptor GenerateDefaultMap()
        {
            TownHall townHall = new TownHall("TownHall", 100, 100, false, null, new Coordinates { x = 13, y = 34 });
            Carry carry = new Carry("Carry1", new Coordinates { x = 1220, y = 620 });
            //Tree tree = new Tree("Tree1", new Coordinates { x = 30, y = 30 });
            GoldMine mine = new GoldMine("Gold mine1", new Coordinates { x = 341, y = 1122 });
            Farm farm1 = new Farm(0, "Farm1", new Coordinates { x = 23, y = 557 });
            Farm farm2 = new Farm(1, "Farm2", new Coordinates { x = 178, y = 557 });
            Worker worker1 = new Worker(100, false, null, new Coordinates { x = 400, y = 400 });
            Worker worker2 = new Worker(100, false, null, new Coordinates { x = 450, y = 450 });

            var Resources = new SerializableDictionary<ResourcesType, int> { { ResourcesType.Gold, 1000 }, { ResourcesType.Stone, 1000 }, { ResourcesType.Wood, 1000 } };

            var game = new GameDescriptor();

            game.TownHalls.Add(townHall.ToTownHallDescriptor());
            game.Carries.Add(carry.ToCarryDescriptor());
            //game.Trees.Add(tree);
            game.GoldMines.Add(mine.ToGoldMineDescriptor());
            game.Farms.Add(farm1.ToFarmDescriptor());
            game.Farms.Add(farm2.ToFarmDescriptor());
            game.Workers.Add(worker1.ToWorkerDescriptor());
            game.Workers.Add(worker2.ToWorkerDescriptor());
            game.Resources = Resources;
            game.MaxPopulation = game.Farms.Count * game.Farms[0].PopulationIncrement;
            game.ActualPopulation = game.Workers.Count * game.Workers[0].PopulationSlots;

            return (IGameDescriptor)game;
        }

        public static IGameDescriptor GenerateMapFromOptions(int workers, int farms, SerializableDictionary<ResourcesType, int> resources)
        {
            var gameDescriptor = GenerateDefaultMap();

            if (resources == null) throw new ArgumentNullException("GenerateMapFromOptions: resources are null");

            if (gameDescriptor.Workers.Count != workers)
            {
                gameDescriptor.Workers = new List<WorkerDescriptor>();
                var nbWorkersToCreate = workers - gameDescriptor.Workers.Count;
                for (int i = 0; i < nbWorkersToCreate; i++)
                {
                    gameDescriptor.Workers.Add(new Worker().ToWorkerDescriptor());
                }
            }

            if (gameDescriptor.Farms.Count != farms)
            {
                gameDescriptor.Farms = new List<FarmDescriptor>();
                for (int i = 0; i < farms; i++)
                {
                    gameDescriptor.Farms.Add(new Farm($"Farm{i}", new Coordinates { x = 10, y = 5 }).ToFarmDescriptor());
                }
            }

            foreach (var resource in resources)
            {
                gameDescriptor.Resources[resource.Key] = resource.Value;
            }

            gameDescriptor.MaxPopulation = gameDescriptor.Farms.Count * gameDescriptor.Farms[0].PopulationIncrement;
            gameDescriptor.ActualPopulation = gameDescriptor.Workers.Count * gameDescriptor.Workers[0].PopulationSlots;

            return gameDescriptor;
        }
    }
}
