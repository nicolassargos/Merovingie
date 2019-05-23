using AoC.Common.Interfaces;

namespace AoC.Api.Domain.EventArgs
{
    // Argument de l'événement déclenché lors de la fin de création d'un building
    public class BuildingCreatedEventArgs
    {
        public IBuilding building { get; set; }
    }
}
