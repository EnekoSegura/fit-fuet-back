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

    }
}
