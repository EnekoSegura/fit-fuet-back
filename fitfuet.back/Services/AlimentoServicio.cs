using fit_fuet_back.IRepositorios;
using fit_fuet_back.IServicios;
using fitfuet.back.Controllers;
using fitfuet.back.Models;
using fitfuet.back.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fit_fuet_back.Servicios
{
    public class AlimentoServicio : IAlimentoServicio
    {
        private readonly IAlimentoRepositorio _alimentoRepositorio;

        public AlimentoServicio(IAlimentoRepositorio alimentoRepositorio)
        {
            _alimentoRepositorio = alimentoRepositorio;
        }

        public async Task<List<Alimentos>> obtenerTodosAlimentos()
        {
            return await _alimentoRepositorio.obtenerTodosAlimentos();
        }

        public async Task<bool> insertarAlimentacion(Dieta dieta)
        {
            return await _alimentoRepositorio.insertarAlimentacion(dieta);
        }
    }
}
