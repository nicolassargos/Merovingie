using AoC.Api.Domain;
using Merovingie.Models;
using AoC.Api.Domain.UseCases;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AoC.MerovingieFileManager;

namespace Merovingie
{
    public class MSocketHandler
    {
        private GameManager _gameManager;

        public MSocketHandler()
        {
            _gameManager = new GameManager();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="socket"></param>
        /// <returns></returns>
        public async Task Listen(HttpContext context, WebSocket socket)
        {  
            var buffer = new byte[6 * 1024];
            WebSocketReceiveResult result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!result.CloseStatus.HasValue)
            {
                var messageReceived = InterpretMessage(buffer);

                Array.Clear(buffer, 0, buffer.Length);

                var sentObject = SetBytesFromMessage(messageReceived);

                await socket.SendAsync(new ArraySegment<byte>(sentObject, 0, sentObject.Length), 0,
                    true, CancellationToken.None);

                result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await socket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }

        private MMessageModel InterpretMessage(byte[] buffer)
        {
            var messageReceived = GetMessageFromBytes(buffer);

            MMessageModel result;

            switch (messageReceived.Type)
            {
                // GAME_CONNECT
                case MessageTypes.GAMECONNECT_DEMAND:
                    try
                    {
                        var gameNameToLoad = messageReceived.Message.ToString();
                        var gameDescriptor = GameFileManager.ReadGame(gameNameToLoad);
                        _gameManager = new GameManager(gameDescriptor);
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("InterpretMessage: message of connection demand received is incorrectly formatted", messageReceived.Message.toString());
                    }
                    result = new MMessageModel(MessageTypes.GAMECONNECT_OK, "");
                    break;
                // GAMECOMMAND
                case MessageTypes.CREATION_REQUEST:
                    try
                    {
                        var data = JsonConvert.DeserializeObject<MCreationRequestBodyModel>(Convert.ToString(messageReceived.Message));
                        _gameManager.CreateWorker(data.CreatorId);
                    }
                    catch (Exception ex)
                    {

                        throw new ArgumentException("InterpretMessage: message of creation received is incorrectly formatted", messageReceived.Message.toString());
                    }
                    result = new MMessageModel(MessageTypes.CREATION_ACCEPTED, "message de retour");
                    break;
                // DEFAULT
                default:
                    result = new MMessageModel(MessageTypes.INFO, "message incomprehensible");
                    break;
            }

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private MMessageModel GetMessageFromBytes(ArraySegment<byte> buffer)
        {
            var jsonReceived = Encoding.UTF8.GetString(buffer);
            var messageObject = JsonConvert.DeserializeObject<MMessageModel>(jsonReceived);

            return messageObject;
        }


        private byte[] SetBytesFromMessage(MMessageModel message)
        {
            message.Message = "toto";

            var jsonMessage = JsonConvert.SerializeObject(message);



            return Encoding.ASCII.GetBytes(jsonMessage);
        }
    }
}
