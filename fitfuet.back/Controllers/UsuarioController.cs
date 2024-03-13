using fit_fuet_back.IRepositorios;
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
using System.Collections.Generic;
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
        private const string EMAIL = "fitfuet@gmail.com";

        public UsuarioController(IUsuarioService usuarioService, IConfiguration config)
        {
            _usuarioService = usuarioService;
            _config = config;
        }

        [HttpPost("crear")]
        public async Task<ActionResult<int>> crearUsuario([FromBody] Usuario usuario)
        {
            var tryRegister = await _usuarioService.Register(usuario);
            if (tryRegister == 0)
            {
                return Ok(0);
            }
            else if(tryRegister == 1)
            {
                return BadRequest(1);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("login")]
        public async Task<ActionResult<string>> login([FromQuery] string email, [FromQuery] string passwd)
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

        [HttpPost("passwd-recovery")]
        public async Task<ActionResult<string>> enviarMail([FromQuery] string Email)
        {
            var usuario = await _usuarioService.GetUser(Email);
            if (usuario != null)
            {
                var newPasswd = GenerateRandomPassword();
                var message = new MailMessage()
                {
                    From = new MailAddress(EMAIL),
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
                    Credentials = new NetworkCredential(EMAIL, _config["ClaveAplicacion"]),
                    EnableSsl = true,
                };

                smtp.Send(message);

                var check = await _usuarioService.ChangePasswd(usuario, Encriptar.EncriptarPassword(newPasswd));

                if (check)
                    return Ok();
                else
                    return BadRequest("No se pudo enviar correo electrónico de recuperación");
            }
            return BadRequest("El correo electrónico indicado no existe");
        }

        [HttpPost("change-password")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> cambiarPasswd([FromQuery] int idUsuario, [FromQuery] string nuevaPassword, [FromQuery] string email, [FromQuery] string antiguaPassword)
        {
            var loginCorrecto = await _usuarioService.Login(email, antiguaPassword);

            if(loginCorrecto != null)
            {
                var check = await _usuarioService.cambiarPasswd(idUsuario, nuevaPassword);
                if (check)
                    return Ok();
                else
                    return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }
        //TODO: En el front hay que hacer un formulario para introducir el email, la contraseña actual y la nueva (2 veces)
        //si el email y contraseña coinciden, es decir, si llamando al login se encuentra un usuario, se accederá al metodo cambiarPasswd

        [HttpPost("eliminar-cuenta")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<string>> eliminarCuenta([FromQuery] string email, [FromQuery] string passwd)
        {
            // Verificar que el usuario existe y la contraseña es correcta
            var usuario = await _usuarioService.GetUser(email);
            if (usuario != null)
            {
                // Cambiar el estado de cuenta a 1 (inactivo)
                var check = await _usuarioService.CambiarEstadoCuenta(usuario, 1);

                if (check)
                    return Ok();
                else
                    return BadRequest("No se pudo cambiar el estado de la cuenta");
            }
            else
            {
                return BadRequest("Usuario no encontrado o contraseña incorrecta");
            }
        }

        [HttpGet("foto")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<string>> obtenerImagenUsuario([FromQuery] int idUsuario)
        {
            var imagen = await _usuarioService.obtenerFotoUsuario(idUsuario);
            var respuesta = new { imagen };
            return Ok(respuesta);
        }

        private string GenerateRandomPassword()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, 8)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [HttpPut("actualizar-datos")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<string>> actualizarDatosUsuario([FromBody] UsuarioActualizado _usuario)
        {
            var usuario = await _usuarioService.actualizarDatosUsuario(_usuario);
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

        [HttpGet("obtener-todos-los-datos")]
        public async Task<ActionResult<List<Tuple<int,float, float, DateTime, float>>>> obtenerDatosCorporales([FromQuery] int idUsuario)
        {
            try
            {
                var datosUsuario = await _usuarioService.obtenerDatosCorporales(idUsuario);

                if (datosUsuario != null)
                {
                    return Ok(new {datosUsuario});
                }

                return BadRequest("Usuario no encontrado");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("obtener-ultimos-datos")]
        public async Task<ActionResult<List<Tuple<float, float, DateTime, float>>>> obtenerUltimosDatosCorporales([FromQuery] int idUsuario)
        {
            try
            {
                var datosUsuario = await _usuarioService.obtenerUltimosDatosCorporales(idUsuario);

                if (datosUsuario != null)
                {
                    return Ok(new { datosUsuario });
                }

                return BadRequest("Usuario no encontrado");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("obtener-ultimo-dato")]
        public async Task<ActionResult<Tuple<float, float, DateTime, float>>> obtenerUltimoDato(int idUsuario)
        {
            try
            {
                var datosUsuario = await _usuarioService.obtenerUltimoDato(idUsuario);

                if (datosUsuario != null)
                {
                    var resultado = new Tuple<float, float, DateTime>(datosUsuario.Item1, datosUsuario.Item2, datosUsuario.Item3);
                    return Ok(resultado);
                }

                return BadRequest("Usuario no encontrado");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("agregar-dato")]
        public async Task<ActionResult> agregarDato([FromBody] DatosUsuariosInsertar datoUsuario)
        {
            try
            {

                if (await _usuarioService.agregarDato(datoUsuario))
                {
                    return Ok();
                }

                return BadRequest("Usuario no encontrado");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
