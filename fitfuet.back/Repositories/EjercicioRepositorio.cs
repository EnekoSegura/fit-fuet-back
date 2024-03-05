using fit_fuet_back.Context;
using fit_fuet_back.IRepositorios;
using fitfuet.back.Controllers;
using fitfuet.back.Models;
using fitfuet.back.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fit_fuet_back.Repositorios
{
    public class EjercicioRepositorio : IEjercicioRepositorio
    {

        private readonly AplicationDbContext _context;

        public EjercicioRepositorio(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<List<EjercicioObjeto>>> obtenerListaEjercios()
        {
            var ejercicios = await _context.Set<Ejercicio>()
                        .Select(e => new EjercicioObjeto(
                            e.Id,
                            e.Nombre,
                            e.MusculoEjercitado,
                            e.Tipo.ToString()))
                        .ToListAsync();
            return ejercicios;
        }
    }
}
