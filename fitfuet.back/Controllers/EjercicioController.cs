using fit_fuet_back.IServicios;
using fit_fuet_back.Servicios;
using fitfuet.back.IControllers;
using fitfuet.back.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fitfuet.back.Controllers
{
    public class  EjercicioObjeto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Musculo { get; set; }
        public string TipoEjercicio { get; set; }

        public EjercicioObjeto (int id, string nombre, string musculo, string tipoEjercicio)
        {
            Id = id;
            Nombre = nombre;
            Musculo = musculo;
            TipoEjercicio = tipoEjercicio;
        }   
    }

    [Route("api/[controller]")]
    [ApiController]
    public class EjercicioController : Controller, IEjercicioController
    {
        private readonly IEjercicioServicio _ejercicioServicio;

        public EjercicioController(IEjercicioServicio ejercicioServicio)
        {
            _ejercicioServicio = ejercicioServicio;
        }

        [HttpGet("obtener-lista-ejercicios")]
        public async Task<ActionResult<List<EjercicioObjeto>>> obtenerListaEjercios()
        {
            var lista = await _ejercicioServicio.obtenerListaEjercios();
            return lista;
        }

        [HttpPost("guardar-rutina")]
        public async Task<ActionResult<string>> insertarRutina(Rutina[] rutina)
        {
            if(await _ejercicioServicio.insertarRutina(rutina))
                return Ok(new { message = "Rutina guardada exitósamente" });
            return BadRequest("Error al guardar la rutina");
        }

        [HttpGet("obtener-rutina")]
        public async Task<ActionResult<Rutina[]>> obtenerRutina([FromQuery] int idUsuario, [FromQuery] DateTime fecha)
        {
            try
            {
                var rutinas = await _ejercicioServicio.obtenerRutina(idUsuario, fecha);

                var rutinasProyectadas = rutinas.Select(r => new
                {
                    r.Id,
                    r.IdUsuario,
                    r.IdEjercicio,
                    r.Series,
                    r.Repeticionesc,
                    r.Fecha
                }).ToArray();

                return Ok(new { rutina = rutinasProyectadas });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
