using fitfuet.back.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace fitfuet.back.IControllers
{
    public interface IUsuarioController
    {
        Task<ActionResult<int>> crearUsuario([FromBody] Usuario usuario);
        Task<ActionResult<string>> login(string email, string passwd);
        Task<ActionResult<string>> enviarMail([FromQuery] string Email);
        Task<ActionResult> cambiarPasswd([FromQuery] int idUsuario, [FromQuery] string nuevaPassword, [FromQuery] string email, [FromQuery] string antiguaPassword);
        Task<ActionResult<string>> eliminarCuenta([FromQuery] string email, [FromQuery] string passwd);
        Task<ActionResult<string>> obtenerImagenUsuario([FromQuery] int idUsuario);
        Task<ActionResult<string>> actualizarDatosUsuario([FromBody] UsuarioActualizado _usuario);
        Task<ActionResult<List<Tuple<int, float, float, DateTime, float>>>> obtenerDatosCorporales([FromQuery] int idUsuario);
        Task<ActionResult<List<Tuple<float, float, DateTime, float>>>> obtenerUltimosDatosCorporales([FromQuery] int idUsuario);
        Task<ActionResult<Tuple<float, float, DateTime, float>>> obtenerUltimoDato(int idUsuario);
        Task<ActionResult<string>> agregarDato([FromBody] DatosUsuariosInsertar datoUsuario);
        Task<ActionResult<string>> editarDato(int idDatoCorporal, DatosUsuariosInsertar datoUsuario);
        Task<ActionResult> eliminarDatoCorporal([FromQuery] int idDatoCorporal);
        Task<ActionResult<float>> obtenerUltimaAltura([FromQuery] int idUsuario);
    }
}
