using System;
using System.Collections.Generic;
using System.Net.WebSockets;

namespace fitfuet.back.IServices
{
    public interface IChatStateService
    {
        Dictionary<int, Tuple<WebSocket, string>> ConnectedUsers { get; set; }
    }
}
