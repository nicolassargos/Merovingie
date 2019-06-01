using Common.Exceptions;
using AoC.Api.Domain;
using AoC.Api.Domain.UseCases;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AoC.MerovingieFileManager;
using AoC.Api.Domain.EventArgs;
using Common.Struct;
using AoC.Common.Network.Models;
using AoC.Domain.TypeExtentions;
using AoC.Common.Interfaces;
using AoC.Common.Descriptors;
using AoC.DataLayer;
using Common.Network;
using Microsoft.Extensions.Logging;

namespace Merovingie
{
    public class MSocketHandler
    {

        private WebSocket _socket;
        private INetworkGameDispatcher _networkGameDispatcher;
        


        public MSocketHandler(INetworkGameDispatcher networkGameDispatcher)
        {
            _networkGameDispatcher = networkGameDispatcher;
            _networkGameDispatcher.NotificationPopedUp += OnNotification;
        }

        private void InitializeNetworkDispatcher()
        {
            _networkGameDispatcher.Initialize();
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
            InitializeNetworkDispatcher();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        private async void InterpretMessage(byte[] buffer)
        {
            MMessageModel messageReceived = null;
            try
            {
                messageReceived = GetMessageFromBytes(buffer);
                var messageBack = _networkGameDispatcher.ProcessMessage(messageReceived);
                if (messageBack != null) await SendMessage(messageBack);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async void OnNotification(object sender, NotificationEventArgs e)
        {
            await SendMessage(e.Message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageToSend"></param>
        /// <returns></returns>
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
