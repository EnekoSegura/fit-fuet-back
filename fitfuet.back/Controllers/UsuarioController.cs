using fit_fuet_back.IServicios;
using fitfuet.back.IControllers;
using fitfuet.back.Models;
using fitfuet.back.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace fitfuet.back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller, IUsuarioController
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IConfiguration _config;

        public UsuarioController(IUsuarioService usuarioService, IConfiguration config)
        {
            _usuarioService = usuarioService;
            _config = config;
        }

        [HttpPost("crear")]
        public async Task<ActionResult<Usuario>> crearUsuario([FromBody] Usuario usuario)
        {
            var tryRegister = await _usuarioService.Register(usuario);
            if (tryRegister == 0)
            {
                return Ok(usuario);
            }
            else if(tryRegister == 1)
            {
                return BadRequest("El usuario ya existe");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("login")]
        public async Task<ActionResult<Usuario>> login([FromQuery] string email, [FromQuery] string passwd)
        {
            var usuario = await _usuarioService.Login(email, passwd);

            if (usuario != null)
            {
                string tokenString = JwtConfigurator.getToken(usuario, _config);
                return Ok(new { token = tokenString });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<string> prueba()
        {
            return Ok("weka");
        }

        private static string email = "fitfuet@gmail.com";
        private static string destino = "dam3.erlantzgsc@gmail.com";
        //private static string contra = "Fitfuet2024";
        //private static string destino = "dam3.erlantzgsc@gmail.com";

        [HttpGet("enviar")]
        public ActionResult<string> enviarMail()
        {
            var message = new MailMessage()
            {
                From = new MailAddress(email),
                Subject = "Prueba",
                IsBodyHtml = true,
                Body = "Cuerpo de la prueba",
            };

            message.To.Add(new MailAddress(destino));

            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(email, _config["ClaveAplicacion"]),
                EnableSsl = true,
            };

            smtp.Send(message);

            return Ok("Correcto");
        }
    }
}
