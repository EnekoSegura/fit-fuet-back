using fitfuet.back.IControllers;
using fitfuet.back.IServices;
using fitfuet.back.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fitfuet.back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : Controller, IChatController
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("obtener-mensajes")]
        public async Task<ActionResult<List<Mensaje>>> GetMensajes()
        {
            try
            {
                var mensajes = await _chatService.GetMensajes();

                if(mensajes != null)
                {
                    return Ok(new { mensajes });
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
