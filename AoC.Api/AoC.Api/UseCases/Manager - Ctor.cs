using AoC.Api.Domain;
using AoC.Api.EventArgs;
using Common.Dto;
using Common.Enums;
using Common.Exceptions;
using Common.Helpers;
using Common.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace AoC.Api.UseCases
{
    public partial class GameManager
    {
        // Events
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<PopulationChangedEventArgs> PopulationChanged;
        public event EventHandler<ResourcesChangedArgs> ResourcesChanged;
        public event EventHandler<MaxPopulationChangedArgs> OnMaxPopulationChanged;
        public event EventHandler<BuildingCreatedEventArgs> OnBuildingCreated;

        #endregion

        // Fields
        #region Fields

        public int MaxPopulation { get; private set; }
        public int QtyOr { get => Resources[ResourcesType.Gold]; }
        public int QtyWood { get => Resources[ResourcesType.Wood]; }
        public int QtyStone { get => Resources[ResourcesType.Stone]; }

        // Total Population
        public int TotalPopulation
        {
            get
            {
                return PopulationList.Count();
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
                PopulationList.Add(worker);

            foreach (var farm in game.Farms)
                BuildingList.Add(farm);

            foreach (var hall in game.TownHalls)
                BuildingList.Add(hall);

            foreach (var tree in game.Trees)
                BuildingList.Add(tree);

            foreach (var mine in game.GoldMines)
                BuildingList.Add(mine);

            foreach (var carry in game.Carries)
                BuildingList.Add(carry);
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
                        Resources[res.Key] -= res.Value;
                    }
                    ResourcesChanged(this, new ResourcesChangedArgs { CurrentResources = Resources });
                }
            }
            catch (NotEnoughResourcesException ex)
            {
                throw ex;
            }
            
        }

        #endregion

        

        

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        //private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        //{

        //        if (PropertyChanged != null)
        //        {
        //            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //        }
        //}
    }

}
