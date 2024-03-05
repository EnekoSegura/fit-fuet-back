﻿using fit_fuet_back.IRepositorios;
using fit_fuet_back.IServicios;
using fitfuet.back.Controllers;
using fitfuet.back.Models;
using fitfuet.back.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fit_fuet_back.Servicios
{
    public class EjercicicoServicio : IEjercicioServicio
    {
        private readonly IEjercicioRepositorio _ejercicicoRepositorio;

        public EjercicicoServicio(IEjercicioRepositorio ejercicicoRepositorio)
        {
            _ejercicicoRepositorio = ejercicicoRepositorio;
        }

        public async Task<ActionResult<List<EjercicioObjeto>>> obtenerListaEjercios()
        {
            var lista = await _ejercicicoRepositorio.obtenerListaEjercios();
            return lista;
        }
    }
}