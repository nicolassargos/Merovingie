using Common.Exceptions;
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
using AoC.Api.EventArgs;

namespace Merovingie
{
    public class MSocketHandler
    {
        private GameManager _gameManager;
        private WebSocket _socket;

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
            _socket = socket;
            var buffer = new byte[6 * 1024];

            if (_socket == null) throw new ArgumentNullException("Socket Listen: socket argument is null");

            WebSocketReceiveResult result = await _socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!result.CloseStatus.HasValue)
            {
                InterpretMessage(buffer);

                Array.Clear(buffer, 0, buffer.Length);

                result = await _socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await _socket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }

        private void InterpretMessage(byte[] buffer)
        {
            var messageReceived = GetMessageFromBytes(buffer);

            switch (messageReceived.Type)
            {
                // GAME_CONNECT
                case MessageTypes.GAMECONNECT_DEMAND:
                    try
                    {
                        var gameNameToLoad = messageReceived.Message.ToString();
                        var gameDescriptor = GameFileManager.ReadGame(gameNameToLoad);
                        // TODO: extraire une méthode initializeGameManager
                        _gameManager = new GameManager(gameDescriptor);
                        _gameManager.ResourcesChanged += SendResourcesChanged;
                        _gameManager.PopulationChanged += SendPopulationChanged;
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("InterpretMessage: message of connection demand received is incorrectly formatted", messageReceived.Message.toString());
                    }
                    SendMessage(new MMessageModel(MessageTypes.GAMECONNECT_OK, ""));
                    break;
                // GAMECOMMAND
                case MessageTypes.CREATION_REQUEST:
                    try
                    {
                        var data = JsonConvert.DeserializeObject<MCreationRequestBodyModel>(Convert.ToString(messageReceived.Message));
                        _gameManager.CreateWorker(data.creatorId, data.positionX, data.positionY);
                    }
                    catch(NotEnoughUnitSlotsAvailableException slex)
                    {
                        SendMessage(new MMessageModel(MessageTypes.CREATION_REFUSEDPOPULATION, ""));
                    }
                    catch(NotEnoughResourcesException rex)
                    {
                        SendMessage(new MMessageModel(MessageTypes.CREATION_REFUSEDRESOURCES, ""));
                    }
                    catch (Exception ex)
                    {
                        SendMessage(new MMessageModel(MessageTypes.INFO, "InterpretMessage: message of creation received is incorrectly formatted"));
                    }
                    break;
                // DEFAULT
                default:
                    SendMessage(new MMessageModel(MessageTypes.INFO, "message incomprehensible"));
                    break;
            }

            //return result;
        }

        private void SendPopulationChanged(object sender, PopulationChangedEventArgs e)
        {
            var messageBody = JsonConvert.SerializeObject(e.Unit);

            MMessageModel messageToSend = new MMessageModel(MessageTypes.CREATION_COMPLETED, messageBody);
            SendMessage(messageToSend);
        }

        private void SendResourcesChanged(object sender, ResourcesChangedArgs e)
        {
            var messageBody = JsonConvert.SerializeObject(e.CurrentResources);

            MMessageModel messageToSend = new MMessageModel(MessageTypes.CREATION_ACCEPTED, messageBody);
            SendMessage(messageToSend);
        }

        public async Task SendMessage(MMessageModel messageToSend)
        {
            var sentObject = SetBytesFromMessage(messageToSend);

            await _socket.SendAsync(new ArraySegment<byte>(sentObject, 0, sentObject.Length), 0,
                    true, CancellationToken.None);
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
            var jsonMessage = JsonConvert.SerializeObject(message);

            return Encoding.ASCII.GetBytes(jsonMessage);
        }
    }
}
