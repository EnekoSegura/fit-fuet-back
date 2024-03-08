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
    public class EjercicicoServicio : IEjercicioServicio
    {
        private readonly IEjercicioRepositorio _ejercicicoRepositorio;

        public EjercicicoServicio(IEjercicioRepositorio ejercicicoRepositorio)
        {
            _ejercicicoRepositorio = ejercicicoRepositorio;
        }

        public async Task<List<EjercicioObjeto>> obtenerListaEjercios()
        {
            var lista = await _ejercicicoRepositorio.obtenerListaEjercios();
            return lista;
        }

        public async Task<bool> insertarRutina(Rutina[] rutina)
        {
            if(await _ejercicicoRepositorio.insertarRutina(rutina))
                return true;
            return false;
        }

        public async Task<Rutina[]> obtenerRutina(int idUsuario, DateTime fecha)
        {
            return await _ejercicicoRepositorio.obtenerRutina(idUsuario, fecha);
        }

        public async Task<List<Tuple<int, string>>> obtenerNombreEjercicios()
        {
            return await _ejercicicoRepositorio.obtenerNombreEjercicios();
        }
    }
}
