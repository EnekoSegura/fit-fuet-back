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
using System.Linq;
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

        [HttpPost("enviar")]
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

        [HttpPost("olvidar-contraseña")]
        public async Task<ActionResult<string>> enviarMail([FromQuery] string Email)
        {
            var usuario = await _usuarioService.GetPassword(Email);
            var newPasswd = GenerateRandomPassword();
            var message = new MailMessage()
            {
                From = new MailAddress(email),
                Subject = "Recuperación de contraseña para el usuario: " + usuario.Nombre + " " + usuario.Apellido,
                IsBodyHtml = true,
                Body = @"
                        <html>
                        <head>
                            <style>
                                body {
                                    font-family: Arial, sans-serif;
                                    background-color: #f5f5f5;
                                    margin: 0;
                                    padding: 0;
                                }
                                .container {
                                    width: 80%;
                                    margin: 20px auto;
                                    background-color: #fff;
                                    padding: 20px;
                                    border-radius: 10px;
                                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                                }
                                h2 {
                                    color: #333;
                                }
                                p {
                                    color: #666;
                                }
                            </style>
                        </head>
                        <body>
                            <div class='container'>
                                <h2>Recuperación de contraseña</h2>
                                <p>Tu nueva contraseña es la siguiente: <strong>" + newPasswd + @"</strong></p>
                                <p>Por favor, cambia la contraseña cuanto antes por razones de seguridad.</p>
                            </div>
                        </body>
                        </html>"
            };

            message.To.Add(new MailAddress(Email));

            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(email, _config["ClaveAplicacion"]),
                EnableSsl = true,
            };

            smtp.Send(message);

            var check = await _usuarioService.ChangePasswd(usuario, newPasswd);

            if (check)
                return Ok("Correcto");
            else
                return BadRequest();
        }

        private string GenerateRandomPassword()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, 8)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
