using Common.Enums;
using Common.Helpers;

namespace AoC.Common.Network.Models
{
    public class GameDescriptorModel
    {
        public string Name { get; set; }
        public int Farms { get; set; }

        public int Workers { get; set; }

        public SerializableDictionary<ResourcesType, int> Resources { get; set; }
    }
}
