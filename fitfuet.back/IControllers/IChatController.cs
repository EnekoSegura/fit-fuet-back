using fitfuet.back.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fitfuet.back.IControllers
{
    public interface IChatController
    {
        Task<ActionResult<List<Mensaje>>> GetMensajes();
    }
}
