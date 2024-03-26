using fit_fuet_back.IServicios;
using fit_fuet_back.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using fitfuet.back.Models;
using fitfuet.back.IControllers;

namespace fitfuet.back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlimentoController : Controller, IAlimentoController
    {
        private readonly IAlimentoServicio _alimentoServicio;
        private readonly IUsuarioService _usuarioService;

        public AlimentoController(IAlimentoServicio alimentoServicio, IUsuarioService usuarioService)
        {
            _alimentoServicio = alimentoServicio;
            _usuarioService = usuarioService;
        }

        [HttpGet("obtener-todos-alimentos")]
        public async Task<ActionResult<List<Alimentos>>> obtenerTodosAlimentos()
        {
            try
            {
                var listaAlimentos = await _alimentoServicio.obtenerTodosAlimentos();

                return Ok(new { listaAlimentos });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("insertar-dieta")]
        public async Task<ActionResult<string>> insertarAlimentacion([FromBody] Dieta dieta)
        {
            try
            {
                if(await _alimentoServicio.insertarAlimentacion(dieta))
                {
                    string msg = "Dieta añadida correctamente";
                    return Ok(new { msg });
                }
                return BadRequest("No se pudo añadir la dieta correctamente");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("obtener-dieta")]
        public async Task<ActionResult<Tuple<List<Dieta>, double, double, double, double>>> obtenerDietaPorDiaYUsuario([FromQuery] int idUsuario, [FromQuery] DateTime fecha)
        {
            try
            {
                var dietaList = await _alimentoServicio.obtenerDietaPorDiaYUsuario(idUsuario, fecha);
                if (dietaList != null)
                {
                    var modoUsuario = await _usuarioService.obtenerModo(idUsuario);
                    var datoUsuario = await _usuarioService.obtenerUltimoDato(idUsuario);

                    var kcalRequerido = (datoUsuario.Item2 * 24) * 1.5;

                    if(modoUsuario == 0)
                    {
                        kcalRequerido = kcalRequerido - 300;
                    } 
                    else if(modoUsuario == 2)
                    {
                        kcalRequerido = kcalRequerido + 300;
                    }

                    var cantidadCarboTotal = (kcalRequerido * 0.5) / 4;
                    var cantidadProteTotal = (kcalRequerido * 0.25) / 4;
                    var cantidadGrasaTotal = (kcalRequerido * 0.25) / 9;

                    Tuple<List<Dieta>, double, double, double, double> tuplaDieta = new Tuple<List<Dieta>, double, double, double, double>(
                            dietaList,
                            kcalRequerido,
                            cantidadCarboTotal,
                            cantidadProteTotal,
                            cantidadGrasaTotal
                        );

                    return Ok(new { tuplaDieta });
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
