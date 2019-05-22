using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Common.Enums;
using Common.Helpers;
using Common.Interfaces;
using Common.Struct;

namespace AoC.DataLayer.Descriptors
{
    public class TownHallDescriptor : ActiveBuildingDescriptor
    {
        #region TownHall Concrete
        [XmlIgnoreAttribute]
        public ConcurrentQueue<IProductable> ProductionQueue { get; set; }
        public Coordinates RallyPoint { get; set; }
        #endregion
    }
}
