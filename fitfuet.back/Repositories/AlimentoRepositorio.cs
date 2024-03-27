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

        public async Task<List<Dieta>> obtenerDietaPorDiaYUsuario(int idUsuario, DateTime fecha)
        {
            var dieta = await _context.Dieta
                .Where(x => x.IdUsuario == idUsuario && x.Fecha == fecha)
                .Include(r => r.Alimento)
                .ToListAsync();
            return dieta;
        }

        public async Task<List<string>> obtenerRecomendacion(double cantidadMacro, string macro = "")
        {
            try
            {
                List<Alimentos> listaAlimentos;
                if (macro.Equals("carbo"))
                {
                    listaAlimentos = await _context.Alimentos
                        .Where(x => x.Carbohidratos > cantidadMacro 
                            && x.TipoAlimento != "Grasas y Aceites" 
                            && x.TipoAlimento != "Bebidas no alcohólicas" 
                            && x.TipoAlimento != "Bebidas alcohólicas" 
                            && x.TipoAlimento != "Condimentos y Salsas"
                            && x.TipoAlimento != "Aperitivos"
                            && x.TipoAlimento != "Dulces"
                            && x.TipoAlimento != "Otros")
                        .ToListAsync();
                }
                else if (macro.Equals("prote"))
                {
                    listaAlimentos = await _context.Alimentos
                        .Where(x => x.Proteinas > cantidadMacro
                            && x.TipoAlimento != "Grasas y Aceites"
                            && x.TipoAlimento != "Bebidas no alcohólicas"
                            && x.TipoAlimento != "Bebidas alcohólicas"
                            && x.TipoAlimento != "Condimentos y Salsas"
                            && x.TipoAlimento != "Aperitivos"
                            && x.TipoAlimento != "Dulces"
                            && x.TipoAlimento != "Otros")
                        .ToListAsync();
                }
                else
                {
                    listaAlimentos = await _context.Alimentos
                        .Where(x => x.Grasas > cantidadMacro
                            && x.TipoAlimento != "Grasas y Aceites"
                            && x.TipoAlimento != "Bebidas no alcohólicas"
                            && x.TipoAlimento != "Bebidas alcohólicas"
                            && x.TipoAlimento != "Condimentos y Salsas"
                            && x.TipoAlimento != "Aperitivos"
                            && x.TipoAlimento != "Dulces"
                            && x.TipoAlimento != "Otros")
                        .ToListAsync();
                }

                var nombreAlimentos = listaAlimentos.Select(x => x.Nombre).ToList();

                return nombreAlimentos;
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }
    }
}
