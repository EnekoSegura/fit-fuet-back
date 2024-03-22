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
    public class AlimentoRepositorio : IAlimentoRepositorio
    {

        private readonly AplicationDbContext _context;

        public AlimentoRepositorio(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Alimentos>> obtenerTodosAlimentos()
        {
            var alimentos = await _context.Set<Alimentos>()
                        .ToListAsync();
            return alimentos;
        }

        public async Task<bool> insertarAlimentacion(Dieta dieta)
        {
            var annadido = await _context.AddAsync(dieta);
            await _context.SaveChangesAsync();
            if (annadido != null)
                return true;
            return false;
        }
    }
}
