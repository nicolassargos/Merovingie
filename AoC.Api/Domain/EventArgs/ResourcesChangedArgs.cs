using Common.Enums;
using Common.Helpers;

namespace AoC.Api.Domain.EventArgs
{
    public class ResourcesChangedArgs
    {
        public SerializableDictionary<ResourcesType, int> resources;
    }
}