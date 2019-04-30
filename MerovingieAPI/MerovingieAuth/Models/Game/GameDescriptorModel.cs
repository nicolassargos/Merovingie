using Common.Enums;
using Common.Helpers;

namespace Merovingie.Models.Game
{
    public class GameDescriptorModel
    {
        public string Name { get; set; }
        public int Farms { get; set; }

        public int Workers { get; set; }

        public SerializableDictionary<ResourcesType, int> Resources { get; set; }
    }
}
