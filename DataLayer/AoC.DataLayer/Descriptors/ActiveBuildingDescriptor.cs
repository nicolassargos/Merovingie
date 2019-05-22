using System.Xml.Serialization;
using Common.Enums;
using Common.Helpers;
using Common.Struct;

namespace AoC.DataLayer.Descriptors
{
    public class ActiveBuildingDescriptor
    {
        #region ActiveBuilding
        [XmlElement]
        public int Id { get; set; }
        public string Name { get; set; }
        public Coordinates Position { get; set; }
        public int LifePoints { get; set; }
        public int MaxLifePoints { get; set; }
        public bool Attack { get; set; }
        public SerializableDictionary<ResourcesType, int> Stock { get; set; }
        public Coordinates RallyPoint { get; set; }

        #endregion
    }
}