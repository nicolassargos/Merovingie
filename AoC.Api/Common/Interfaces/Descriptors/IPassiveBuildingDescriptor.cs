using System.Xml.Serialization;
using Common.Enums;
using Common.Helpers;
using Common.Struct;

namespace AoC.Common.Interfaces.Descriptors
{
    public interface IPassiveBuildingDescriptor
    {
        #region PassiveBuilding
        int Id { get; set; }
        string Name { get; set; }
        Coordinates Position { get; set; }
        ResourcesType Resource { get; set; }
        int CollectQty { get; set; }
        int FetchTimeEllapse { get; set; }
        SerializableDictionary<ResourcesType, int> Stock { get; set; }
        Coordinates RallyPoint { get; set; }
        #endregion

    }
}
