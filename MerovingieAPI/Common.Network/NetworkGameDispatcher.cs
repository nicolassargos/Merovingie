using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoC.Api.Domain;
using AoC.Common.Interfaces;
using AoC.Common.Network.Models;

namespace Common.Network
{
    public class NetworkGameDispatcher
    {
        #region fields
        private IGameDescriptor _gameDescriptor;
        //private IGameManager _gameManager;
        private List<IGameDescriptor> _partialMessage = new List<IGameDescriptor>();
        private string _gameName;
        #endregion


        /// <summary>
        /// Interprète un message reçu
        /// </summary>
        /// <param name="message"></param>
        public void InterpretMessage(MMessageModel message)
        {
            //switch (message.Type)
            //{
            //    // GAME_CONNECT
            //    case MessageTypes.GAMECONNECT_DEMAND:
            //        _gameName = messageReceived.Message.ToString();
            //        _gameDescriptor = (IGameDescriptor)GameFileManager.ReadGame(_gameName);
            //        break;
            //}
        }
    }
}
