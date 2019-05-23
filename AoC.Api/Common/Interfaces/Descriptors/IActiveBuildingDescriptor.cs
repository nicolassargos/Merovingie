using System.Xml.Serialization;
using Common.Enums;
using Common.Helpers;
using Common.Struct;

namespace AoC.Common.Interfaces.Descriptors
{
    public interface IActiveBuildingDescriptor
    {
        #region ActiveBuilding
        int Id { get; set; }
        string Name { get; set; }
        Coordinates Position { get; set; }
        int LifePoints { get; set; }
        int MaxLifePoints { get; set; }
        bool Attack { get; set; }
        SerializableDictionary<ResourcesType, int> Stock { get; set; }
        Coordinates RallyPoint { get; set; }

        #endregion
    }
}