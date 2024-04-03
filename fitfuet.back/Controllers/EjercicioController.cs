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
                    r.Repeticiones,
                    r.Fecha
                }).ToArray();

                return Ok(new { rutina = rutinasProyectadas });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        //devuelve una tupla de nombres e ids de los ejercicios
        [HttpGet("obtener-nombre-ejercicios")]
        public async Task<ActionResult<List<Tuple<int, string, string>>>> obtenerNombreEjercicios()
        {
            try
            {
                var listaNombreEjercicios = await _ejercicioServicio.obtenerNombreEjercicios();

                return Ok(new { listaNombreEjercicios });
            } 
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("obtener-descripcion-ejercicios")]
        public async Task<ActionResult<Ejercicio>> obtenerDescripcionEjercicio([FromQuery] int idEjercicio)
        {
            return await _ejercicioServicio.obtenerDescripcionEjercicio(idEjercicio);
        }

        [HttpGet("obtener-todas-rutinas")]
        public async Task<ActionResult<List<Tuple<DateTime, bool, bool>>>> obtenerTodasRutinas([FromQuery] int idUsuario)
        {
            try
            {
                var listaRutinas = await _ejercicioServicio.obtenerTodasRutinas(idUsuario);

                return Ok(new { listaRutinas });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("obtener-rutina-diaria")]
        public async Task<ActionResult<List<Rutina>>> obtenerRutinaDiaria([FromQuery] int idUsuario, [FromQuery] string fecha)
        {
            try
            {
                var rutina = await _ejercicioServicio.obtenerRutinaDiaria(idUsuario, fecha);

                return Ok(new { rutina });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("obtener-rutina-ejercicio")]
        public async Task<ActionResult<Rutina>> obtenerRutinaEjercicio([FromQuery] int idRutina)
        {
            try
            {
                var rutina = await _ejercicioServicio.obtenerRutinaEjercicio(idRutina);

                if(rutina != null)
                    return Ok(new { rutina });
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("borrar-rutina-ejercicio")]
        public async Task<ActionResult<string>> eliminarEjercicioRutina([FromQuery] int idRutina)
        {
            try
            {
                if (await _ejercicioServicio.eliminarEjercicioRutina(idRutina))
                    return Ok();
                return BadRequest("No se pudo eliminar el ejercicio de la rutina");
            }
            catch (Exception)
            {
                return BadRequest("Error a la hora de eliminar el ejercicio de la rutina");
            }
        }

        [HttpPut("update-rutina-ejercicio")]
        public async Task<ActionResult<string>> updateEjercicioRutina([FromBody] Rutina rutina)
        {
            try
            {
                if (await _ejercicioServicio.updateEjercicioRutina(rutina))
                    return Ok();
                return BadRequest("No se pudo actualizar el ejercicio de la rutina");
            }
            catch 
            {
                return BadRequest("Ocurrió un error intentando actualizar el ejercicio");
            }
        }
    }
}
