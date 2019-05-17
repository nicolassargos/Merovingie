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
using System.Data;
using System.IO;
using Common.Struct;

namespace Merovingie
{
    public class MSocketHandler
    {

        private GameDescriptor _gameDescriptor;
        private GameManager _gameManager;
        private List<GameDescriptor> _partialMessage = new List<GameDescriptor>();
        private string _gameName;
        private WebSocket _socket;

        public MSocketHandler()
        {
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
            var buffer = new byte[4 * 1024];

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
            MMessageModel messageReceived;
            try
            {
                messageReceived = GetMessageFromBytes(buffer);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            switch (messageReceived.Type)
            {
                // GAME_CONNECT
                case MessageTypes.GAMECONNECT_DEMAND:
                    try
                    {
                        _gameName = messageReceived.Message.ToString();
                        _gameDescriptor = (GameDescriptor)GameFileManager.ReadGame(_gameName);
                        // TODO: extraire une méthode initializeGameManager

                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException("InterpretMessage: message of connection demand received is incorrectly formatted", messageReceived.Message.toString());
                    }
                    SendMessage(new MMessageModel(MessageTypes.GAMECONNECT_OK, ""));
                    break;
                // FILELOAD
                case MessageTypes.FILELOAD_REQUESTED:
                    try
                    {
                        var data = JsonConvert.SerializeObject(_gameDescriptor);
                        SendMessage(new MMessageModel(MessageTypes.FILELOAD_ACCEPTED, data));
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                    break;
                case MessageTypes.FILESAVE_REQUESTED_FIRSTPART:
                    try
                    {
                        _partialMessage.Add(JsonConvert.DeserializeObject<GameDescriptor>(messageReceived.Message.ToString()));
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    break;
                case MessageTypes.FILESAVE_REQUESTED_NEXTPART:
                    try
                    {
                        _partialMessage.Add(JsonConvert.DeserializeObject<GameDescriptor>(messageReceived.Message.ToString()));

                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                    break;
                case MessageTypes.FILESAVE_REQUESTED_END:
                    try
                    {
                        GameDescriptor gameDescriptor = InitializeEachGameItem(_partialMessage, _gameDescriptor);
                        GameFileManager.SaveGame(gameDescriptor, _gameName);
                        // La partie est correctement initialisée
                        _gameManager = new GameManager(_gameDescriptor);
                        _gameManager.ResourcesChanged += SendResourcesChanged;
                        _gameManager.PopulationChanged += SendPopulationChanged;
                        _gameManager.WorkerCompletedCollect += SendResourceCollected;
                        _gameManager.WorkerCompletedBringback += SendResourcesReleased;
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                    finally
                    {
                        _partialMessage.Clear();
                    }
                    break;
                // GAMECOMMAND
                case MessageTypes.CREATION_REQUESTED:
                    try
                    {
                        var data = JsonConvert.DeserializeObject<MCreationRequestBodyModel>(Convert.ToString(messageReceived.Message));
                        _gameManager.CreateWorker(data.creatorId, data.positionX, data.positionY);
                    }
                    catch (NotEnoughUnitSlotsAvailableException slex)
                    {
                        SendMessage(new MMessageModel(MessageTypes.CREATION_REFUSEDPOPULATION, ""));
                    }
                    catch (NotEnoughResourcesException rex)
                    {
                        SendMessage(new MMessageModel(MessageTypes.CREATION_REFUSEDRESOURCES, ""));
                    }
                    catch (Exception ex)
                    {
                        SendMessage(new MMessageModel(MessageTypes.INFO, "InterpretMessage: message of creation received is incorrectly formatted"));
                    }
                    break;
                // CLIENTDATA_UNITSSTATE
                case MessageTypes.CLIENTDATA_UNITSSTATE:
                    try
                    {
                        List<MUnitsStateModel> data = JsonConvert.DeserializeObject<List<MUnitsStateModel>>(messageReceived.Message.ToString());
                        foreach (var unit in data)
                        {
                            _gameManager.SetUnitPosition(unit.Id, unit.Position);
                        }
                        GameFileManager.SaveGame(_gameManager.ToGameDescriptor(), _gameName);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    break;
                // Premier message pour un fetch (quand l'unité arrive devant la ressource)
                case MessageTypes.FETCHWAY_REQUESTED:
                    try
                    {
                        MUnitCollectRequestedModel data = JsonConvert.DeserializeObject<MUnitCollectRequestedModel>(messageReceived.Message);
                        _gameManager.FetchResource(data.unitId, data.buildingId);
                        SendMessage(new MMessageModel(MessageTypes.FETCHWAY_ACCEPTED, "accepted"));
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    break;
                case MessageTypes.FETCHBACK_REQUESTED:
                    try
                    {
                        MUnitReleaseRequestedModel data = JsonConvert.DeserializeObject<MUnitReleaseRequestedModel>(messageReceived.Message);
                        _gameManager.ReleaseUnitResources(data.unitId, data.buildingId);
                        SendMessage(new MMessageModel(MessageTypes.FETCHBACK_ACCEPTED, "accepted"));
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                    break;
                // DEFAULT
                default:
                    SendMessage(new MMessageModel(MessageTypes.INFO, "unknown message type"));
                    break;
            }

            //return result;
        }


        /// <summary>
        /// Signale à l'UI qu'un worker vient de libérer ses ressources
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendResourcesReleased(object sender, ResourcesReleasedArgs e)
        {
            var messageBody = JsonConvert.SerializeObject(e);

            MMessageModel messageToSend = new MMessageModel(MessageTypes.FETCHBACK_COMPLETED, messageBody);
            SendMessage(messageToSend);
        }


        /// <summary>
        /// Signale à l'UI qu'un worker a fini 
        /// de collecter des ressources
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendResourceCollected(object sender, ResourcesFetchedArgs e)
        {
            var messageBody = JsonConvert.SerializeObject(e);

            MMessageModel messageToSend = new MMessageModel(MessageTypes.FETCHWAY_COMPLETED, messageBody);
            SendMessage(messageToSend);
        }

        private GameDescriptor AssembleFromMultiParts(List<GameDescriptor> partialMessage)
        {
            var newGameDescriptor = new GameDescriptor();
            foreach (var part in partialMessage)
            {
                if (part.Carries != null && part.Carries.Count > 0)
                    newGameDescriptor.Carries.AddRange(part.Carries);

                if (part.Farms != null && part.Farms.Count > 0)
                    newGameDescriptor.Farms.AddRange(part.Farms);

                if (part.GoldMines != null && part.GoldMines.Count > 0)
                    newGameDescriptor.GoldMines.AddRange(part.GoldMines);

                if (part.TownHalls != null && part.TownHalls.Count > 0)
                    newGameDescriptor.TownHalls.AddRange(part.TownHalls);

                if (part.Trees != null && part.Trees.Count > 0)
                    newGameDescriptor.Trees.AddRange(part.Trees);

                if (part.Workers != null && part.Workers.Count > 0)
                    newGameDescriptor.Workers.AddRange(part.Workers);

                if (part.Resources != null && part.Resources.Count > 0)
                    foreach (var res in part.Resources)
                        newGameDescriptor.Resources.Add(res.Key, res.Value);
            }

            return newGameDescriptor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="partialMessage"></param>
        /// <param name="serverGameDescriptor"></param>
        private GameDescriptor InitializeEachGameItem(List<GameDescriptor> partialMessage, GameDescriptor serverGameDescriptor)
        {
            var assembledGameDescriptor = AssembleFromMultiParts(partialMessage);

            try
            {
                if (assembledGameDescriptor.Carries != null && assembledGameDescriptor.Carries.Count == serverGameDescriptor.Carries.Count)
                {
                    for (var i = 0; i < assembledGameDescriptor.Carries.Count; i++)
                    {
                        serverGameDescriptor.Carries[i].Id = assembledGameDescriptor.Carries[i].Id;
                    }
                }

                if (assembledGameDescriptor.Farms != null && assembledGameDescriptor.Farms.Count == serverGameDescriptor.Farms.Count)
                {
                    for (var i = 0; i < assembledGameDescriptor.Farms.Count; i++)
                    {
                        serverGameDescriptor.Farms[i].Id = assembledGameDescriptor.Farms[i].Id;
                    }
                }

                if (assembledGameDescriptor.GoldMines != null && assembledGameDescriptor.GoldMines.Count == serverGameDescriptor.GoldMines.Count)
                {
                    for (var i = 0; i < assembledGameDescriptor.GoldMines.Count; i++)
                    {
                        serverGameDescriptor.GoldMines[i].Id = assembledGameDescriptor.GoldMines[i].Id;
                    }
                }

                if (assembledGameDescriptor.TownHalls != null && assembledGameDescriptor.TownHalls.Count == serverGameDescriptor.TownHalls.Count)
                {
                    for (var i = 0; i < assembledGameDescriptor.TownHalls.Count; i++)
                    {
                        serverGameDescriptor.TownHalls[i].Id = assembledGameDescriptor.TownHalls[i].Id;
                    }
                }

                if (assembledGameDescriptor.Trees != null && assembledGameDescriptor.Trees.Count >= 0)
                {
                    for (var i = 0; i < assembledGameDescriptor.Trees.Count; i++)
                    {
                        if (i >= serverGameDescriptor.Trees.Count)
                            serverGameDescriptor.Trees.Add(
                                new Tree(
                                    "tree",
                                    new Coordinates
                                    {
                                        x = assembledGameDescriptor.Trees[i].Position.x,
                                        y = assembledGameDescriptor.Trees[i].Position.y,
                                    }));
                        serverGameDescriptor.Trees[i].Id = assembledGameDescriptor.Trees[i].Id;
                    }
                }

                if (assembledGameDescriptor.Workers != null && assembledGameDescriptor.Workers.Count == serverGameDescriptor.Workers.Count)
                {
                    for (var i = 0; i < assembledGameDescriptor.Workers.Count; i++)
                    {
                        serverGameDescriptor.Workers[i].Id = assembledGameDescriptor.Workers[i].Id;
                    }
                }

                if (assembledGameDescriptor.Resources != null && assembledGameDescriptor.Resources.Count == serverGameDescriptor.Resources.Count)
                {
                    foreach (var res in assembledGameDescriptor.Resources)
                    {
                        serverGameDescriptor.Resources[res.Key] = assembledGameDescriptor.Resources[res.Key];
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return serverGameDescriptor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendPopulationChanged(object sender, PopulationChangedEventArgs e)
        {
            var messageBody = JsonConvert.SerializeObject(e.Unit);

            MMessageModel messageToSend = new MMessageModel(MessageTypes.CREATION_COMPLETED, messageBody);
            SendMessage(messageToSend);
        }

        private void SendResourcesChanged(object sender, ResourcesChangedArgs e)
        {
            var messageBody = JsonConvert.SerializeObject(e.resources);

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
        private MMessageModel GetMessageFromBytes(byte[] buffer)
        {
            MMessageModel messageObject = null;

            var jsonReceived = Encoding.UTF8.GetString(buffer);
            try
            {
                messageObject = JsonConvert.DeserializeObject<MMessageModel>(jsonReceived);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return messageObject;
        }


        private byte[] SetBytesFromMessage(MMessageModel message)
        {
            var jsonMessage = JsonConvert.SerializeObject(message);

            return Encoding.ASCII.GetBytes(jsonMessage);
        }
    }
}
