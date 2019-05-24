using Common.Enums;
using Common.Helpers;

namespace AoC.Api.Domain.EventArgs
{
    public class ResourcesChangedArgs : System.EventArgs
    {
        public SerializableDictionary<ResourcesType, int> resources { get; set; }
    }
}