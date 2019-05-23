using Common.Enums;
using Common.Exceptions;
using Common.Helpers;
using Common.Interfaces;
using AoC.Domain.TypeExtentions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AoC.Api.Domain.EventArgs;

namespace AoC.Api.Domain.UseCases
{
    public partial class GameManager
    {
        // Events
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<PopulationChangedEventArgs> PopulationChanged;
        public event EventHandler<ResourcesChangedArgs> ResourcesChanged;
        public event EventHandler<MaxPopulationChangedArgs> MaxPopulationChanged;
        //TODO: créer la classe des args et changer son nom
        public event EventHandler<MaxPopulationChangedArgs> MaxPopulationReached;
        public event EventHandler<BuildingCreatedEventArgs> BuildingCreated;
        public event EventHandler<ResourcesFetchedArgs> BuildingResourcesChanged;

        #endregion

        // Fields
        #region Fields
        private int _maxPopulation;
        public int QtyOr { get => Resources[ResourcesType.Gold]; }
        public int QtyWood { get => Resources[ResourcesType.Wood]; }
        public int QtyStone { get => Resources[ResourcesType.Stone]; }

        // Total Population
        public int MaxPopulation {
            get
            {
                if (BuildingList.OfType<Farm>()?.Count() > 0)
                {
                    return BuildingList.OfType<Farm>().Count() * BuildingList.OfType<Farm>().First().PopulationIncrement;
                }
                else
                    return _maxPopulation;
            }
            private set {
                _maxPopulation = value;
            }
        }
        public int ActualPopulation
        {
            get
            {
                return PopulationList.Sum(x => x.PopulationSlots);
            }
        }


        public SerializableDictionary<ResourcesType, int> Resources { get; set; }
        public List<IUnit> PopulationList;
        public List<IBuilding> BuildingList;

        #endregion

        // Contructor
        #region Constructor

        public GameManager()
        {
            Resources = new SerializableDictionary<ResourcesType, int>();
            PopulationList = new List<IUnit>();
            BuildingList = new List<IBuilding>();
            MaxPopulation = 0;
        }

        public GameManager(IGameDescriptor game)
        {
            Resources = game.Resources;
            PopulationList = new List<IUnit>();
            BuildingList = new List<IBuilding>();

            foreach (var worker in game.Workers)
                PopulationList.Add(worker.ToWorker());

            foreach (var farm in game.Farms)
                BuildingList.Add(farm.ToFarm());

            foreach (var hall in game.TownHalls)
                BuildingList.Add(hall.ToTownHall());

            foreach (var tree in game.Trees)
                BuildingList.Add(tree.ToTree());

            foreach (var mine in game.GoldMines)
                BuildingList.Add(mine.ToGoldMine());

            foreach (var carry in game.Carries)
                BuildingList.Add(carry.ToCarry());

            MaxPopulation = game.MaxPopulation;
        }

        #endregion

        // Methods
        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productable"></param>
        /// <returns></returns>
        private bool CheckResourcesForProduction(IProductable productable)
        {
            // Verifier si les ressources necessaires sont dispo
            foreach (var res in productable.Cost)
            {
                if (res.Value > Resources[res.Key]) throw new NotEnoughResourcesException();
            }
            return true;
        }

        /// <summary>
        /// Check if there are enough free slots 
        /// to create a unit
        /// </summary>
        /// <param name="unit"></param>
        private bool CheckFreeSlotInPopulation(IUnit unit)
        {
            if (MaxPopulation - PopulationList.Count >= unit.PopulationSlots) return true;
            else throw new NotEnoughUnitSlotsAvailableException();
        }

        /// <summary>
        /// Cherche un worker "libre" parmi la liste des unités
        /// Renvoie le premier libre
        /// ou une exception NoWorkerAvailableException sinon
        /// </summary>
        /// <param name="productable"></param>
        /// <returns></returns>
        private Worker GetWorkerAvailableForProduction()
        {
            // Récupère un worker dont la liste de production est vide
            // (autorise les workers qui prélèvent des ressources)
            var worker = PopulationList.FirstOrDefault( x => 
                x.GetType().Name == "Worker" && 
                (x as ICreator).ProductionQueue.Count == 0) 
                as Worker;

            if (worker == null) throw new NoWorkerAvailableException();

            return worker;
        }


        /// <summary>
        /// Attribue un nouvel Id à une unité nouvellement créée
        /// </summary>
        /// <returns></returns>
        private int GetNewUnitId()
        {
            if (PopulationList.Count == 0) return 0;
            return PopulationList.Max(x => x.Id) + 1;
        }


        /// <summary>
        /// Vérifie si le stock est suffisant pour retirer des ressources du stock
        /// Si le stock est suffisant, les retire de la liste
        /// Renvoie une NotEnoughResourcesException sinon
        /// </summary>
        /// <param name="Worker"></param>
        private void RemoveResourcesFromStock(IProductable productable)
        {
            try
            {
                if (CheckResourcesForProduction(productable))
                {
                    foreach (var res in productable.Cost)
                    {
                        this.Resources[res.Key] -= res.Value;
                    }
                    ResourcesChanged(this, new ResourcesChangedArgs { resources = this.Resources });
                }
            }
            catch (NotEnoughResourcesException ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Utils

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public GameDescriptor ToGameDescriptor()
        {
            var gameDescriptor = new GameDescriptor();
            try
            {
                // Carries
                gameDescriptor.Carries.AddRange(BuildingList.OfType<Carry>().Select(bld => bld.ToCarryDescriptor()));

                // Trees
                gameDescriptor.Trees.AddRange(BuildingList.OfType<Tree>().Select(bld => bld.ToTreeDescriptor()));

                // Gold mines
                gameDescriptor.GoldMines.AddRange(BuildingList.OfType<GoldMine>().Select(bld => bld.ToGoldMineDescriptor()));

                // Town Hall
                gameDescriptor.TownHalls.AddRange(BuildingList.OfType<TownHall>().Select(bld => bld.ToTownHallDescriptor()));

                // Farms
                gameDescriptor.Farms.AddRange(BuildingList.OfType<Farm>().Select(bld => bld.ToFarmDescriptor()));

                // Workers
                gameDescriptor.Workers.AddRange(PopulationList.OfType<Worker>().Select(bld => bld.ToWorkerDescriptor()));

                // Resources
                foreach (var res in Resources)
                    gameDescriptor.Resources.Add(res.Key, res.Value);

                gameDescriptor.MaxPopulation = MaxPopulation;
                gameDescriptor.ActualPopulation = this.ActualPopulation;
                    
            }
            catch (Exception)
            {

                throw new TranscriptionToDescriptorException();
            }

            return gameDescriptor;
            
        }

        #endregion
    }

}
