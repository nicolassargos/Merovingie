using Common.Struct;

namespace AoC.Common.Network.Models
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
        FILELOAD_REQUESTED,
        FILELOAD_ERROR_NOTFOUND,
        FILELOAD_ERROR_CORRUPTED,
        FILELOAD_ERROR_UNAUTHORIZED,
        FILELOAD_ACCEPTED,
        FILESAVE_REQUESTED_FIRSTPART,
        FILESAVE_REQUESTED_NEXTPART,
        FILESAVE_REQUESTED_END,
        FILESAVE_COMPLETED,
        FILESAVE_ERROR_CORRUPTED,
        FILESAVE_ERROR_UNAUTHORIZED,
        FILESAVE_ERROR_COMPLETED,
        CREATION_REQUESTED,
        CREATION_ACCEPTED,
        CREATION_ABORTED,
        CREATION_COMPLETED,
        CREATION_REFUSEDRESOURCES,
        CREATION_REFUSEDPOPULATION,
        CREATION_ERROR,
        CLIENTDATA_UNITSSTATE,
        FETCHWAY_REQUESTED,
        FETCHWAY_ACCEPTED,
        FETCHWAY_ABORTED,
        FETCHWAY_COMPLETED,
        FETCHBACK_REQUESTED,
        FETCHBACK_ACCEPTED,
        FETCHBACK_ABORTED,
        FETCHBACK_COMPLETED,
        INFO,
        INFO_UPDATESTOCK
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

    public class MUnitsStateModel
    {
        public int Id { get; set; }
        public Coordinates Position { get; set; }
    }

    public class MUnitCollectRequestedModel
    {
        public int unitId { get; set; }
        public int buildingId { get; set; }
    }

    public class MUnitCollectCompletedModel : MUnitCollectRequestedModel
    {
        public MResourcesBodyModel resources { get; set; }
    }

    public class MUnitReleaseRequestedModel : MUnitCollectRequestedModel
    {
        public MResourcesBodyModel resources { get; set; }
    }
}
