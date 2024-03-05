using fitfuet.back.Controllers;
using fitfuet.back.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fit_fuet_back.IRepositorios
{
    public interface IEjercicioRepositorio
    {
        Task<ActionResult<List<EjercicioObjeto>>> obtenerListaEjercios();
    }
}
