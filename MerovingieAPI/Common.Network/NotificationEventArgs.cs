using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoC.Common.Network.Models;

namespace Common.Network
{
    public class NotificationEventArgs : EventArgs
    {
        public MMessageModel Message { get; set; }
    }
}
