using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System;
using fitfuet.back.IServices;

namespace fitfuet.back.Services
{
    public class ChatService: IChatService
    {

        public async Task HandleWebSocket(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                await Echo(context, webSocket);
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        }

        private async Task Echo(HttpContext context, WebSocket webSocket)
        {
            // Mensaje de bienvenida
            string welcomeMessage = "¡Bienvenido al servidor de chat!";
            var welcomeBuffer = Encoding.UTF8.GetBytes(welcomeMessage);
            await webSocket.SendAsync(new ArraySegment<byte>(welcomeBuffer), WebSocketMessageType.Text, true, CancellationToken.None);

            // Mantener la conexión abierta y esperar mensajes entrantes
            var buffer = new byte[1024 * 4];
            while (webSocket.State == WebSocketState.Open)
            {
                // Recibir mensaje del cliente
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                // Si el cliente cierra la conexión, salir del bucle
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                    break;
                }

                // Procesar el mensaje recibido y enviar una respuesta
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
                string responseMessage = $"Mensaje recibido: '{receivedMessage}'";
                var responseBuffer = Encoding.UTF8.GetBytes(responseMessage);
                await webSocket.SendAsync(new ArraySegment<byte>(responseBuffer), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

    }
}
