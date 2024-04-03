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

        public async Task<List<Tuple<int, string, string>>> obtenerNombreEjercicios()
        {
            return await _ejercicicoRepositorio.obtenerNombreEjercicios();
        }

        public async Task<Ejercicio> obtenerDescripcionEjercicio(int idEjercicio)
        {
            return await _ejercicicoRepositorio.obtenerDescripcionEjercicio(idEjercicio);
        }

        public async Task<List<Tuple<DateTime, bool, bool>>> obtenerTodasRutinas(int idUsuario)
        {
            return await _ejercicicoRepositorio.obtenerTodasRutinas(idUsuario);
        }

        public async Task<List<Rutina>> obtenerRutinaDiaria(int idUsuario, string fecha)
        {
            return await _ejercicicoRepositorio.obtenerRutinaDiaria(idUsuario, ConvertirStringToDateTime(fecha));
        }

        public static DateTime ConvertirStringToDateTime(string str)
        {
            string strLimpia = str.Trim('"');
            DateTime dateTime;
            if (DateTime.TryParse(strLimpia, out dateTime))
            {
                return dateTime;
            }
            else
            {
                throw new ArgumentException("La cadena no tiene un formato de fecha y hora válido.");
            }
        }

        public async Task<Rutina> obtenerRutinaEjercicio(int idRutina)
        {
            return await _ejercicicoRepositorio.obtenerRutinaEjercicio(idRutina);
        }

        public async Task<bool> eliminarEjercicioRutina(int idRutina)
        {
            return await _ejercicicoRepositorio.eliminarEjercicioRutina(idRutina);
        }

        public async Task<bool> updateEjercicioRutina(Rutina rutina)
        {
            return await _ejercicicoRepositorio.updateEjercicioRutina(rutina);
        }
    }
}
