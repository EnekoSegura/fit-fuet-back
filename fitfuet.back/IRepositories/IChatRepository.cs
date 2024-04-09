using fitfuet.back.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fitfuet.back.IRepositories
{
    public interface IChatRepository
    {
        Task<List<Mensaje>> GetMensajes();
        Task InsertarMensaje(int idUsuario, string mensaje, string fechaMensaje);
    }
}
