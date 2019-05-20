using AoC.Common.Interfaces;

namespace AoC.Api.EventArgs
{
    public class BuildingCreatedEventArgs
    {
        public IBuilding building { get; set; }
    }
}
