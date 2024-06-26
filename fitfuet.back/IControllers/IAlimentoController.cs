﻿using fitfuet.back.Controllers;
using fitfuet.back.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fitfuet.back.IControllers
{
    public interface IAlimentoController

    {
        Task<ActionResult<List<Alimentos>>> obtenerTodosAlimentos();
        Task<ActionResult<string>> insertarAlimentacion([FromBody] Dieta dieta);
        Task<ActionResult<Tuple<List<Dieta>, double, double, double, double>>> obtenerDietaPorDiaYUsuario([FromQuery] int idUsuario, [FromQuery] DateTime fecha);
        Task<ActionResult<string>> obtenerRecomendacion([FromQuery] double porcentajeCarbo, [FromQuery] double porcentajeProte, [FromQuery] double porcentajeGrasa);
    }
}
