using AoC.Api.Domain;
using Merovingie.Models;
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

namespace Merovingie
{
    public class MSocketHandler
    {
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
                var receivedObject = GetMessageFromBytes(buffer);


                var sentObject = SetBytesFromMessage(receivedObject);

                await socket.SendAsync(new ArraySegment<byte>(sentObject, 0, sentObject.Length), 0,
                    true, CancellationToken.None);

                result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await socket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
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
