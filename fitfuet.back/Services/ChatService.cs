using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System;
using fitfuet.back.IServices;
using System.Collections.Generic;
using fit_fuet_back.IServicios;

namespace fitfuet.back.Services
{
    public class ChatService: IChatService
    {

        private readonly IUsuarioService _usuarioService;
        private readonly IChatStateService _cs;
        private List<string> connectedUsers = new List<string>();

        public ChatService(IUsuarioService usuarioService, IChatStateService cs)
        {
            _usuarioService = usuarioService;
            _cs = cs;
        }

        public async Task HandleWebSocket(HttpContext context, int userId)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string username = await _usuarioService.GetUsername(userId);
                Tuple<WebSocket, string> tuplaUsuario = new Tuple<WebSocket, string>(webSocket, username);
                _cs.ConnectedUsers.Add(userId, tuplaUsuario);

                await Echo(context, webSocket, userId, username);
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        }

        private async Task Echo(HttpContext context, WebSocket webSocket, int userId, string username)
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
                    _cs.ConnectedUsers.Remove(userId);
                    break;
                }

                // Procesar el mensaje recibido y enviar una respuesta
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
                string responseMessage = $"{receivedMessage}";
                DateTime currentTime = DateTime.Now;
                string currentTimeString = currentTime.ToString("dd/MM/yyyy HH:mm:ss");

                var responseBuffer = Encoding.UTF8.GetBytes($"{username}||{receivedMessage}||{currentTimeString}");
                foreach (var user in _cs.ConnectedUsers)
                {
                    var userWebSocket = user.Value.Item1; // Obtener el WebSocket del usuario desde el diccionario

                    // Enviar el mensaje al WebSocket del usuario
                    await userWebSocket.SendAsync(new ArraySegment<byte>(responseBuffer), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }

    }
}
