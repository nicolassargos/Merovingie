namespace AoC.Api.Domain.EventArgs
{
    // Argument de l'événement déclenché lors de la libération des ressources par une unité
    public class ResourcesReleasedArgs : ResourcesChangedArgs
    {
        public int unitId { get; set; }
    }
}
