using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Merovingie.Models
{

    public class MMessageModel
    {
        public string Message { get; set; }
        public MessageTypes Type { get; set; }

        public MMessageModel(MessageTypes type, string message)
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
        CREATION_CANCELED,
        CREATION_FINISHED,
        CREATION_ERROR,
        INFO
    }

    public class MCreationRequestBodyModel
    {
        public int CreatorId { get; set; }
        public string ProductableName { get; set; }

    }

}
