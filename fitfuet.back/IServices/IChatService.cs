﻿using fitfuet.back.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fitfuet.back.IServices
{
    public interface IChatService
    {
        Task HandleWebSocket(HttpContext context, int userId);
        Task<List<Mensaje>> GetMensajes();
        Task InsertarMensaje(int idUsuario, string mensaje, string fechaMensaje);
    }
}