using AoC.Api.Domain;
using AoC.Common.Interfaces;
using Common.AdvancedDescriptors;
using Common.Enums;
using Common.Helpers;
using Common.Struct;
using System;
using System.Collections.Generic;

namespace AoC.MerovingieFileManager
{
    public static class GameGenerator
    {
        public static IGameDescriptor GenerateDefaultMap()
        {
            TownHallDescriptor townHall = new TownHallDescriptor()
            {
                Name = "TownHall",
                MaxLifePoints = 100,
                LifePoints = 100,
                Attack = false,
                Stock = null,
                Position = new Coordinates { x = 13, y = 34 }
            };

            CarryDescriptor carry = new CarryDescriptor()
            {
                Name = "Carry1",
                Position = new Coordinates { x = 1220, y = 620 }
            };
            GoldMineDescriptor mine = new GoldMineDescriptor() {
                Name = "Gold mine1",
                Position = new Coordinates { x = 341, y = 1122 }
            };
            FarmDescriptor farm1 = new FarmDescriptor()
            {
                Name = "Farm1",
                Position = new Coordinates { x = 23, y = 557 }
            };
            FarmDescriptor farm2 = new FarmDescriptor() {
                Name = "Farm2",
                Position = new Coordinates { x = 178, y = 557 }
            };
            WorkerDescriptor worker1 = new WorkerDescriptor() {
                LifePoints = 100,
                IsWorking = false,
                HoldedResources = null,
                Position = new Coordinates { x = 400, y = 400 }
            };
            WorkerDescriptor worker2 = new WorkerDescriptor()
            {
                LifePoints = 100,
                IsWorking = false,
                HoldedResources = null,
                Position = new Coordinates { x = 450, y = 450 }
            };

            var Resources = new SerializableDictionary<ResourcesType, int> { { ResourcesType.Gold, 1000 }, { ResourcesType.Stone, 1000 }, { ResourcesType.Wood, 1000 } };

            IGameDescriptor game = new GameDescriptor();

            game.TownHalls.Add(townHall);
            game.Carries.Add(carry);
            //game.Trees.Add(tree);
            game.GoldMines.Add(mine);
            game.Farms.Add(farm1);
            game.Farms.Add(farm2);
            game.Workers.Add(worker1);
            game.Workers.Add(worker2);
            game.Resources = Resources;

            return game;
        }

        public static IGameDescriptor GenerateMapFromOptions(int workers, int farms, SerializableDictionary<ResourcesType, int> resources)
        {
            var gameDescriptor = GenerateDefaultMap();

            if (resources == null) throw new ArgumentNullException();

            if (gameDescriptor.Workers.Count < workers)
            {
                gameDescriptor.Workers = new List<WorkerDescriptor>();
                var nbWorkersToCreate = workers - gameDescriptor.Workers.Count;
                for (int i = 0; i < nbWorkersToCreate; i++)
                {
                    gameDescriptor.Workers.Add(new WorkerDescriptor());
                }
            }

            if (gameDescriptor.Farms.Count != farms)
            {
                gameDescriptor.Farms = new List<FarmDescriptor>();
                for (int i = 0; i < farms; i++)
                {
                    gameDescriptor.Farms.Add(new FarmDescriptor());
                }
            }

            foreach (var resource in resources)
            {
                gameDescriptor.Resources[resource.Key] = resource.Value;
            }

            return gameDescriptor;
        }
    }
}
