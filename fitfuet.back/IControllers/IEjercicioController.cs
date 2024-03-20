using fitfuet.back.Controllers;
using fitfuet.back.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fitfuet.back.IControllers
{
    public interface IEjercicioController
    {
        Task<ActionResult<List<EjercicioObjeto>>> obtenerListaEjercios();
        Task<ActionResult<string>> insertarRutina(Rutina[] rutina);
        Task<ActionResult<Rutina[]>> obtenerRutina([FromQuery] int idUsuario, [FromQuery] DateTime fecha);
        Task<ActionResult<List<Tuple<int, string, string>>>> obtenerNombreEjercicios();
        Task<ActionResult<Ejercicio>> obtenerDescripcionEjercicio([FromQuery] int idEjercicio);
        Task<ActionResult<List<Tuple<DateTime, bool, bool>>>> obtenerTodasRutinas([FromQuery] int idUsuario);
        Task<ActionResult<List<Rutina>>> obtenerRutinaDiaria([FromQuery] int idUsuario, [FromQuery] string fecha);
    }
}
