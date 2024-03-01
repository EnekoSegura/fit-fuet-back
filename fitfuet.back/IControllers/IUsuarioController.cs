using fitfuet.back.Models;
using Microsoft.AspNetCore.Mvc;
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
    }
}
