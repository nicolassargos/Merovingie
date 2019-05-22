using System.Xml.Serialization;
using Common.Enums;
using Common.Helpers;
using Common.Struct;

namespace AoC.DataLayer.Descriptors
{
    public class PassiveBuildingDescriptor
    {
        #region PassiveBuilding
        public int Id { get; set; }
        public string Name { get; set; }
        public Coordinates Position { get; set; }
        public ResourcesType Resource { get; set; }
        public int CollectQty { get; set; }
        public int FetchTimeEllapse { get; set; }
        public SerializableDictionary<ResourcesType, int> Stock { get; set; }
        public Coordinates RallyPoint { get; set; }
        #endregion

    }
}
