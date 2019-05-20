using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoC.Common.Network.Models;

namespace AoC.Network
{
    public interface INetworkGameDispatcher
    {
        void InterpretMessage(MMessageModel message);
    }
}
