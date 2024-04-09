using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
using fitfuet.back.IServices;
using System.Collections.Generic;
using fit_fuet_back.IServicios;
using fitfuet.back.Models;
using fitfuet.back.IRepositories;

namespace fitfuet.back.Services
{
    public class ChatService: IChatService
    {

        private readonly IUsuarioService _usuarioService;
        private readonly IChatStateService _cs;
        private readonly IChatRepository _chatRepository;

        public ChatService(IUsuarioService usuarioService, IChatStateService cs, IChatRepository chatRepository)
        {
            _usuarioService = usuarioService;
            _cs = cs;
            _chatRepository = chatRepository;
        }

        public async Task HandleWebSocket(HttpContext context, int userId)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string username = await _usuarioService.GetUsername(userId);
                Tuple<WebSocket, string> tuplaUsuario = new Tuple<WebSocket, string>(webSocket, username);
                _cs.ConnectedUsers.Add(userId, tuplaUsuario);

                await EnviarListaUsuariosConectados();

                await Echo(context, webSocket, userId, username);
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        }

        private async Task EnviarListaUsuariosConectados()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var kvp in _cs.ConnectedUsers)
            {
                stringBuilder.Append(kvp.Value.Item2); // Agregar el string al StringBuilder
                stringBuilder.Append(","); // Separador
            }
            // Eliminar la coma adicional al final, si es necesario
            if (stringBuilder.Length > 0)
            {
                stringBuilder.Length--; // Eliminar el último carácter (coma)
            }
            // Obtener la cadena resultante
            string usersAsString = stringBuilder.ToString();

            foreach (var user in _cs.ConnectedUsers)
            {
                var userWebSocket = user.Value.Item1;

                // Enviar mensaje especial para actualizar lista de usuarios conectados
                var message = $"{usersAsString}";
                var responseBuffer = Encoding.UTF8.GetBytes(message);
                await userWebSocket.SendAsync(new ArraySegment<byte>(responseBuffer), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        private async Task Echo(HttpContext context, WebSocket webSocket, int userId, string username)
        {
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
                    await EnviarListaUsuariosConectados();
                    break;
                }

                // Procesar el mensaje recibido y enviar una respuesta
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
                DateTime currentTime = DateTime.Now;
                string currentTimeString = currentTime.ToString("dd/MM/yyyy HH:mm:ss");

                //Agregamos el mensaje a la base de datos
                await InsertarMensaje(userId, receivedMessage, currentTimeString);

                var responseBuffer = Encoding.UTF8.GetBytes($"{userId}||{username}||{receivedMessage}||{currentTimeString}");
                foreach (var user in _cs.ConnectedUsers)
                {
                    var userWebSocket = user.Value.Item1; // Obtener el WebSocket del usuario desde el diccionario

                    // Enviar el mensaje al WebSocket del usuario
                    await userWebSocket.SendAsync(new ArraySegment<byte>(responseBuffer), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }

        public async Task<List<Mensaje>> GetMensajes()
        {
            return await _chatRepository.GetMensajes();
        }

        public async Task InsertarMensaje(int idUsuario, string mensaje, string fechaMensaje)
        {
            await _chatRepository.InsertarMensaje(idUsuario, mensaje, fechaMensaje);
        }
    }
}
