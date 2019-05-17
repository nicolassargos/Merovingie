using Common.Enums;
using Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AoC.Api.EventArgs
{
    public class ResourcesFetchedArgs : ResourcesChangedArgs
    {
        public int unitId { get; set; }
        public int buildingId { get; set; }
    }
}
