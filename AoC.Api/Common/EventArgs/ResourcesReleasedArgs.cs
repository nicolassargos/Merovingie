using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoC.Api.EventArgs;

namespace AoC.Api.EventArgs
{
    // Argument de l'événement déclenché lors de la libération des ressources par une unité
    public class ResourcesReleasedArgs : ResourcesChangedArgs
    {
        public int unitId { get; set; }
    }
}
