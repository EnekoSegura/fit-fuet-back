using fitfuet.back.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace fitfuet.back.IControllers
{
    public interface IUsuarioController
    {
        Task<ActionResult<Usuario>> crearUsuario([FromBody] Usuario usuario);
        Task<ActionResult<Usuario>> login(string email, string passwd);
    }
}
