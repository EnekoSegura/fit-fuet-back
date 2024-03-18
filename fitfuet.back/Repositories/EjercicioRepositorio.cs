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

        public async Task<List<EjercicioObjeto>> obtenerListaEjercios()
        {
            var ejercicios = await _context.Set<Ejercicio>()
                        .OrderBy(e => e.MusculoEjercitado)
                        .Select(e => new EjercicioObjeto(
                            e.Id,
                            e.Nombre,
                            e.MusculoEjercitado,
                            e.Tipo.ToString()))
                        .ToListAsync();
            return ejercicios;
        }

        public async Task<bool> insertarRutina(Rutina[] rutina)
        {
            for(int i = 0; i < rutina.Length; i++)
                await _context.Rutina.AddAsync(rutina[i]);   
            await _context.SaveChangesAsync();

                return true;
        }

        public async Task<Rutina[]> obtenerRutina(int idUsuario, DateTime fecha)
        {
            //DateTime fechaSinHora = fecha.Date;

            var rutinas = await _context.Rutina
                .Where(r => r.IdUsuario == idUsuario &&
                    r.Fecha == fecha.Date)
                    .ToArrayAsync();

            return rutinas;
        }

        public async Task<List<Tuple<int, string, string>>> obtenerNombreEjercicios()
        {
            var ejercicios = await _context.Set<Ejercicio>()
                .Select(e => new Tuple<int, string, string>(
                    e.Id,
                    e.Nombre,
                    e.Tipo.ToString()
                )).ToListAsync();

            return ejercicios;
        }

        public async Task<Ejercicio> obtenerDescripcionEjercicio(int idEjercicio)
        {
            var ejercicio = await _context.Ejercicio.FirstOrDefaultAsync(x => x.Id == idEjercicio);
            return ejercicio;
        }
    }
}
