using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Merovingie.Models
{

    public class MMessageModel
    {
        public dynamic Message { get; set; }
        public MessageTypes Type { get; set; }

        public MMessageModel(MessageTypes type, dynamic message)
        {
            Type = type;
            Message = message;
        }

        public string toString()
        {
            return Message;
        }
    }

    public enum MessageTypes
    {
        GAMECONNECT_DEMAND,
        GAMECONNECT_OK,
        GAMECONNECT_ERROR,
        CREATION_REQUEST,
        CREATION_ACCEPTED,
        CREATION_ABORTED,
        CREATION_COMPLETED,
        CREATION_ERROR,
        INFO
    }

    public class MCreationRequestBodyModel
    {
        public int creatorId { get; set; }
        public string productableName { get; set; }
        public int positionX { get; set; }
        public int positionY { get; set; }
    }

    public class MCreationAcceptedBodyModel : MResourcesBodyModel
    {
    }

    public class MResourcesBodyModel
    {
        public int gold { get; set; }
        public int stone { get; set; }
        public int wood { get; set; }
    }

}
