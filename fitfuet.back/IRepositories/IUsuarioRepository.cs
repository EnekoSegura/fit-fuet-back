using fitfuet.back.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace fit_fuet_back.IRepositorios
{
    public interface IUsuarioRepository
    {

        Task Register([FromBody] Usuario usuario);
        Task<bool> Exist([FromBody] Usuario usuario);
        Task<Usuario> Login(string email, string passwd);
        Task<Usuario> GetUser(string email);
        Task<Usuario> GetUser(int idUser);
        Task<bool> ChangePasswd(Usuario usuario, string newPasswd);
        Task<bool> cambiarPasswd(int idUsuario, string nuevaPassword);
        Task<bool> UpdateUsuario(Usuario usuario);
    }
}
