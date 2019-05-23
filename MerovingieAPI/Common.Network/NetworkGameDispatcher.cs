﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoC.Api.Domain;
using AoC.Api.Domain.EventArgs;
using AoC.Api.Domain.UseCases;
using AoC.Common.Descriptors;
using AoC.Common.Interfaces;
using AoC.Common.Network.Models;
using AoC.DataLayer;
using AoC.Domain.TypeExtentions;
using Common.Exceptions;
using Common.Struct;
using Newtonsoft.Json;

namespace Common.Network
{
    public class NetworkGameDispatcher : INetworkGameDispatcher
    {
        #region fields
        private GameDescriptor _gameDescriptor;
        private GameManager _gameManager;
        private List<GameDescriptor> _partialMessage = new List<GameDescriptor>();
        private string _gameName;
        #endregion

        #region Events
        public event EventHandler<PopulationChangedEventArgs> PopulationChanged;
        public event EventHandler<ResourcesChangedArgs> ResourcesChanged;
        public event EventHandler<MaxPopulationChangedArgs> MaxPopulationChanged;
        //TODO: créer la classe des args et changer son nom
        public event EventHandler<MaxPopulationChangedArgs> MaxPopulationReached;
        public event EventHandler<BuildingCreatedEventArgs> BuildingCreated;
        public event EventHandler<ResourcesFetchedArgs> BuildingResourcesChanged;
        // Evenement déclenché lors de la fin de la collecte de ressources par un worker
        public event EventHandler<ResourcesFetchedArgs> WorkerCompletedCollect;

        // Evenement déclenché lors de libération des ressources au stock (retour à la base)
        public event EventHandler<ResourcesReleasedArgs> WorkerCompletedBringback;
        #endregion


        /// <summary>
        /// Interprète un message reçu
        /// </summary>
        /// <param name="message"></param>
        public MMessageModel ProcessMessage(MMessageModel messageReceived)
        {
            MMessageModel messageReturned = null;

            string messageContent = Convert.ToString(messageReceived.Message);

            switch (messageReceived.Type)
            {
            // CONNECTION
                case MessageTypes.GAMECONNECT_DEMAND:
                    messageReturned = ProcessConnectionRequest();
                    break;

            // FILELOAD
                case MessageTypes.FILELOAD_REQUESTED:
                    messageReturned = ProcessFileLoadRequest(messageContent);
                    break;

            // FILE SAVE - FIRST PART
                case MessageTypes.FILESAVE_REQUESTED_FIRSTPART:
                    messageReturned = ProcessFileSavePartial(messageContent);
                    break;

            // FILE SAVE - NEXT PARTS
                case MessageTypes.FILESAVE_REQUESTED_NEXTPART:
                    messageReturned = ProcessFileSavePartial(messageContent);
                    break;

            // FILE SAVE - LAST PART
                case MessageTypes.FILESAVE_REQUESTED_END:
                    messageReturned = ProcessFileSaveLastPart();
                    break;

            // CREATION REQUEST - WORKER
                case MessageTypes.CREATION_REQUESTED:
                    messageReturned = ProcessCreationWorkerRequest(messageContent);
                    break;

            // SAVE DATA UNITSSTATE
                case MessageTypes.CLIENTDATA_UNITSSTATE:
                    messageReturned = ProcessSaveUnitState(messageContent);
                    break;

            // COLLECT RESOURCES REQUESTED
                case MessageTypes.FETCHWAY_REQUESTED:
                    messageReturned = ProcessFetchRequested(messageContent);
                    break;

            // RELEASE RESOURCES REQUESTED
                case MessageTypes.FETCHBACK_REQUESTED:
                    messageReturned = ProcessReleaseResourcesRequested(messageContent);
                    break;
            // DEFAULT
                default:
                    messageReturned = new MMessageModel(MessageTypes.INFO, "{\"status\" : \"refused\", \"exception\":\"unknown message type\"}");
                    break;
            }
            return messageReturned;
        }

        /// <summary>
        /// Initialise la demande transfert des ressources 
        /// d'une entité à un bâtiment
        /// </summary>
        /// <param name="messageContent"></param>
        /// <returns></returns>
        private MMessageModel ProcessReleaseResourcesRequested(string messageContent)
        {
            MMessageModel returnedResult = null;
            try
            {
                MUnitReleaseRequestedModel data = JsonConvert.DeserializeObject<MUnitReleaseRequestedModel>(messageContent);
                _gameManager.ReleaseUnitResources(data.unitId, data.buildingId);
                returnedResult = new MMessageModel(MessageTypes.FETCHBACK_ACCEPTED, "{\"status\" : \"ok\"}");
            }
            catch (Exception ex)
            {
                returnedResult = new MMessageModel(MessageTypes.FETCHBACK_ABORTED, $"{{\"status\" : \"error : {ex.Message}\"}}");
            }
            return returnedResult;
        }



        /// <summary>
        /// Initialise la demande requête de collecte de ressources
        /// </summary>
        /// <param name="messageContent"></param>
        /// <returns></returns>
        private MMessageModel ProcessFetchRequested(string messageContent)
        {
            MMessageModel returnedResult = null;
            try
            {
                MUnitCollectRequestedModel data = JsonConvert.DeserializeObject<MUnitCollectRequestedModel>(messageContent);
                _gameManager.FetchResource(data.unitId, data.buildingId);
                returnedResult = new MMessageModel(MessageTypes.FETCHWAY_ACCEPTED, "{\"status\" : \"ok\"}");
            }
            catch (Exception ex)
            {
                returnedResult = new MMessageModel(MessageTypes.FETCHWAY_ABORTED, $"{{\"status\" : \"error : {ex.Message}\"}}");
            }
            return returnedResult;
        }



        #region Creation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private MMessageModel ProcessCreationWorkerRequest(string message)
        {
            MMessageModel returnedResult = null;
            try
            {
                var data = JsonConvert.DeserializeObject<MCreationRequestBodyModel>(Convert.ToString(message));
                _gameManager.CreateWorker(data.creatorId, data.positionX, data.positionY);
            }
            catch (NotEnoughUnitSlotsAvailableException slex)
            {
                returnedResult = new MMessageModel(MessageTypes.CREATION_REFUSEDPOPULATION, $"{{\"status\" : \"refused : {slex.Message}\"}}");
            }
            catch (NotEnoughResourcesException rex)
            {
                returnedResult = new MMessageModel(MessageTypes.CREATION_REFUSEDRESOURCES, $"{{\"status\" : \"refused : {rex.Message}\"}}");
            }
            catch (Exception ex)
            {
                returnedResult = new MMessageModel(MessageTypes.CREATION_ERROR, "{\"status\" : \"refused\", \"exception\": \"InterpretMessage: message of creation received is incorrectly formatted\"}");
            }
            return returnedResult;
        }
        #endregion


        #region SavingProcess
        /// <summary>
        /// Process l'enregistrement des blocks de fichier à sauvegarder
        /// </summary>
        /// <param name="gameData"></param>
        /// <returns></returns>
        private MMessageModel ProcessFileSavePartial(string gameData)
        {
            MMessageModel returnedResult = null;
            try
            {
                _partialMessage.Add(JsonConvert.DeserializeObject<GameDescriptor>(gameData));
            }
            catch (Exception ex)
            {
                returnedResult = new MMessageModel(MessageTypes.FILESAVE_ERROR_CORRUPTED, $"{{\"status\" : \"{ex.Message}\"}}");
            }
            return returnedResult;
        }

        /// <summary>
        /// Process l'initialisation d'un nouveau GameDescriptor,
        /// 
        /// et l'enregistrement du fichier
        /// </summary>
        /// <param name="dynamic"></param>
        /// <returns></returns>
        private MMessageModel ProcessFileSaveLastPart()
        {
            MMessageModel returnedResult = null;
            try
            {
                IGameDescriptor gameDescriptor = InitializeEachGameItem(_partialMessage, _gameDescriptor);
                GameFileManager.SaveGame(gameDescriptor, _gameName);
                // La partie est correctement initialisée
                _gameManager = new GameManager(_gameDescriptor);
                AttachNotificationsToGameManager();
            }
            catch (Exception ex)
            {
                returnedResult = new MMessageModel(MessageTypes.FILESAVE_ERROR_CORRUPTED, $"{{\"status\" : \"{ex.Message}\"}}");
            }
            finally
            {
                _partialMessage.Clear();
            }
            return returnedResult;
        }


        // TODO: ne le faire que lorsque la partie vient d'être créée
        /// <summary>
        /// Prend en paramètre les gameDescriptor partiels envoyés par le client
        /// et les assemble en un seul, puis recopie les ID de chacun des éléments du jeu
        /// </summary>
        /// <param name="partialMessage"></param>
        /// <param name="serverGameDescriptor"></param>
        private IGameDescriptor InitializeEachGameItem(List<GameDescriptor> partialMessage, GameDescriptor serverGameDescriptor)
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
                                    }).ToTreeDescriptor());
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
        /// <param name="partialMessage"></param>
        /// <returns></returns>
        private IGameDescriptor AssembleFromMultiParts(List<GameDescriptor> partialMessage)
        {
            var newGameDescriptor = new GameDescriptor();
            foreach (var part in partialMessage)
            {
                if (part.Carries != null && part.Carries.Count > 0)
                    newGameDescriptor.Carries.AddRange(part.Carries);

                if (part.Farms != null && part.Farms.Count > 0)
                    newGameDescriptor.Farms.AddRange(part.Farms);

                if (part.GoldMines != null && part.GoldMines.Count > 0) ;
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
        /// Méthode d'enregistrement en "continu" 
        /// des coordonnées des unités
        /// </summary>
        /// <param name="messageContent"></param>
        /// <returns></returns>
        private MMessageModel ProcessSaveUnitState(string messageContent)
        {
            MMessageModel returnedResult = null;
            try
            {
                List<MUnitsStateModel> data = JsonConvert.DeserializeObject<List<MUnitsStateModel>>(messageContent);
                foreach (var unit in data)
                {
                    _gameManager.SetUnitPosition(unit.Id, unit.Position);
                }
                GameFileManager.SaveGame(_gameManager.ToGameDescriptor(), _gameName);
            }
            catch (Exception ex)
            {
                returnedResult = new MMessageModel(MessageTypes.FILESAVE_ERROR_CORRUPTED, $"{{\"status\" : \"refused : {ex.Message}\"}}");
            }
            return returnedResult;
        }
        #endregion


        /// <summary>
        /// Attach callback methods to game manager events
        /// </summary>
        private void AttachNotificationsToGameManager()
        {
            _gameManager.ResourcesChanged += NotifyClientResourcesChanged;
            _gameManager.PopulationChanged += NotifyClientPopulationChanged;
            _gameManager.WorkerCompletedCollect += NotifyClientResourceCollected;
            _gameManager.WorkerCompletedBringback += NotifyClientResourcesReleased;
        }

        // Emet l'event de notification de changement de ressources
        private void NotifyClientResourcesChanged(object sender, ResourcesChangedArgs e)
        {
            ResourcesChanged?.Invoke(sender, e);
        }

        // Emet l'event de notification de changement de ressources
        private void NotifyClientPopulationChanged(object sender, PopulationChangedEventArgs e)
        {
            PopulationChanged?.Invoke(sender, e);
        }

        // Emet l'event de notification de changement de ressources
        private void NotifyClientResourceCollected(object sender, ResourcesFetchedArgs e)
        {
            WorkerCompletedCollect?.Invoke(sender, e);
        }

        // Emet l'event de notification de changement de ressources
        private void NotifyClientResourcesReleased(object sender, ResourcesReleasedArgs e)
        {
            WorkerCompletedBringback?.Invoke(sender, e);
        }



        /// <summary>
        /// Process la demande de chargement de fichier
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private MMessageModel ProcessFileLoadRequest(dynamic fileName)
        {
            MMessageModel returnedResult = null;
            try
            {
                _gameName = fileName.Message.ToString();
                _gameDescriptor = (GameDescriptor)GameFileManager.ReadGame(_gameName);
                var gameData = JsonConvert.SerializeObject(_gameDescriptor);
                returnedResult = new MMessageModel(MessageTypes.FILELOAD_ACCEPTED, gameData);
            }
            catch (Exception ex)
            {
                if (ex is FileNotFoundException || ex is ArgumentNullException)
                {
                    returnedResult = new MMessageModel(MessageTypes.FILELOAD_ERROR_NOTFOUND, $"{{\"status\" : \"error : {ex.Message}\"}}");
                }
                else
                    returnedResult = new MMessageModel(MessageTypes.FILELOAD_ERROR_CORRUPTED, $"{{\"status\" : \"error : {ex.Message}\"}}");
            }
            return returnedResult;
        }

        /// <summary>
        /// Process la demande connexion au serveur
        /// </summary>
        /// <returns></returns>
        private MMessageModel ProcessConnectionRequest()
        {
            MMessageModel returnedResult = null;
            try
            {
                returnedResult = new MMessageModel(MessageTypes.GAMECONNECT_OK, "{\"status\" : \"ok\"}");
            }
            catch (Exception ex)
            {
                returnedResult = new MMessageModel(MessageTypes.GAMECONNECT_ERROR, $"{{\"status\" : \"{ex.Message}\"}}");
            }
            return returnedResult;
        }
    }
}
