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

        public AlimentoController(IAlimentoServicio alimentoServicio)
        {
            _alimentoServicio = alimentoServicio;
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
    }
}
