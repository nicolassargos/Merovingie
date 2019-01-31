using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Api.EventArgs
{
    public class BuildingCreatedEventArgs
    {
        public IBuilding building { get; set; }
    }
}
