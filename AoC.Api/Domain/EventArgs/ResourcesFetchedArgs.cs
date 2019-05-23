namespace AoC.Api.Domain.EventArgs
{
    // Argument de l'événement déclenché lors de la fin de la collecte de ressources
    public class ResourcesFetchedArgs : ResourcesChangedArgs
    {
        public int unitId { get; set; }
        public int buildingId { get; set; }
    }
}
