using fitfuet.back.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace fit_fuet_back.IServicios
{
    public interface IUsuarioService
    {

        Task<int> Register([FromBody] Usuario usuario);
        Task<Usuario> Login(string email, string passwd);

        Task<Usuario> GetUser(string email);
        Task<bool> ChangePasswd(Usuario usuario, string newPasswd);
        Task<bool> cambiarPasswd(int idUsuario, string nuevaPassword);
    }
}
