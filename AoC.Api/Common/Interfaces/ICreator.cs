using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface ICreator
    {
        ConcurrentQueue<IProductable> ProductionQueue { get; set;}
    }
}
