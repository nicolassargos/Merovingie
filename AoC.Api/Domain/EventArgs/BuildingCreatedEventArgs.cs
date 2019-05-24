using AoC.Common.Interfaces;
using System;

namespace AoC.Api.Domain.EventArgs
{
    // Argument de l'événement déclenché lors de la fin de création d'un building
    public class BuildingCreatedEventArgs : System.EventArgs
    {
        public IBuilding building { get; set; }
    }
}
