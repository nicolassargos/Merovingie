using AoC.Common.Interfaces;

namespace AoC.Api.Domain.EventArgs
{
    public class PopulationChangedEventArgs : System.EventArgs
    {
        public int ActualPopulation { get; set; }
        public IUnit Unit { get; set; }
    }
}
