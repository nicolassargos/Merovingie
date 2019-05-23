using Common.Interfaces;

namespace AoC.Api.Domain.EventArgs
{
    public class PopulationChangedEventArgs
    {
        public int CurrentPopulation { get; set; }
        public IUnit Unit { get; set; }
    }
}
