using fitfuet.back.IServices;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;

namespace fitfuet.back.Services
{
    public class ChatStateService: IChatStateService
    {
        Dictionary<int, Tuple<WebSocket, string>> IChatStateService.ConnectedUsers { get; set; } = new Dictionary<int, Tuple<WebSocket, string>>();
    }
}
