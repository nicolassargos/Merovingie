using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoC.Api.EventArgs;
using Common.Enums;
using Common.Helpers;
using Common.Interfaces;

namespace AoC.Common.Interfaces
{
    public interface IGameManager
    {
        event PropertyChangedEventHandler PropertyChanged;
        event EventHandler<PopulationChangedEventArgs> PopulationChanged;
        event EventHandler<ResourcesChangedArgs> ResourcesChanged;
        event EventHandler<MaxPopulationChangedArgs> MaxPopulationChanged;
        event EventHandler<MaxPopulationChangedArgs> MaxPopulationReached;
        event EventHandler<BuildingCreatedEventArgs> BuildingCreated;
        event EventHandler<ResourcesFetchedArgs> BuildingResourcesChanged;

        // Fields
        #region Fields

        int MaxPopulation { get; }
        int QtyOr { get; }
        int QtyWood { get; }
        int QtyStone { get; }

        // Total Population
        int TotalPopulation { get; }


        SerializableDictionary<ResourcesType, int> Resources { get; set; }
        List<IUnit> PopulationList { get; set; }
        List<IBuilding> BuildingList { get; set; }

        #endregion
    }
}
