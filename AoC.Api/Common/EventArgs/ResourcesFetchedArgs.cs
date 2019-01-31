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
    public class ResourcesFetchedArgs
    {
        public SerializableDictionary<ResourcesType, int> ResourcesFetched;

        //public ResolveEventArgs(int low, int high)
        //    : base (low, high)
        //{

        //}
    }
}
