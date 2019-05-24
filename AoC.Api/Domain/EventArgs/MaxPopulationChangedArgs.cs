namespace AoC.Api.Domain.EventArgs
{
    public class MaxPopulationChangedArgs : System.EventArgs
    {
        public int CurrentMaxPopulation { get; set; }
    }
}