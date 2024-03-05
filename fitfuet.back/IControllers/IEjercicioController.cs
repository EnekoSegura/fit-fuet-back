using fitfuet.back.Controllers;
using fitfuet.back.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fitfuet.back.IControllers
{
    public interface IEjercicioController
    {
        Task<ActionResult<List<EjercicioObjeto>>> obtenerListaEjercios();
    }
}
