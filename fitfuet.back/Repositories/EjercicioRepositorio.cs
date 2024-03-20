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

        public async Task<List<Tuple<DateTime, bool, bool>>> obtenerTodasRutinas(int idUsuario)
        {
            List<Tuple<DateTime, bool, bool>> tuplasRutinas = new List<Tuple<DateTime, bool, bool>>();

            var fechas = await _context.Rutina.Where(x => x.IdUsuario == idUsuario)
                .GroupBy(x => x.Fecha)
                .Select(group => group.Key)
                .ToListAsync();

            for (int i = 0; i < fechas.Count; i++)
            {
                var tieneFuerza = false;
                var tieneCardio = false;

                var ejercicios = await _context.Rutina.Where(x => x.Fecha == fechas[i] && x.IdUsuario == idUsuario)
                    .Select(x => x.IdEjercicio).ToListAsync();

                for(int j = 0; j < ejercicios.Count; j++)
                {
                    var tipoEjercicio = await _context.Ejercicio.Where(x => x.Id == ejercicios[j])
                        .Select(x => x.Tipo)
                        .FirstOrDefaultAsync();
                    
                    if(tipoEjercicio == TipoEjercicio.Fuerza)
                        tieneFuerza = true;

                    if (tipoEjercicio == TipoEjercicio.Cardio)
                        tieneCardio = true;

                    if(tieneFuerza == true && tieneCardio == true)
                        break;

                }

                Tuple<DateTime, bool, bool> tupla = new Tuple<DateTime, bool, bool>(fechas[i], tieneFuerza, tieneCardio);

                tuplasRutinas.Add(tupla);
            }

            return tuplasRutinas;
        }

        public async Task<List<Rutina>> obtenerRutinaDiaria(int idUsuario, DateTime fecha)
        {
            var rutina = await _context.Rutina
                .Include(r => r.Ejercicio)
                .Where(x => x.Fecha == fecha && x.IdUsuario == idUsuario)
                .ToListAsync();

            return rutina;
        }
    }
}
