using fitfuet.back.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace fit_fuet_back.IServicios
{
    public interface IUsuarioService
    {

        Task<int> Register([FromBody] Usuario usuario);
        Task<bool> Login(string email, string passwd);

    }
}
