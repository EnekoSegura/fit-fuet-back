using fitfuet.back.Controllers;
using fitfuet.back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fit_fuet_back.IRepositorios
{
    public interface IEjercicioRepositorio
    {
        Task<List<EjercicioObjeto>> obtenerListaEjercios();
        Task<bool> insertarRutina(Rutina[] rutina);
        Task<Rutina[]> obtenerRutina(int idUsuario, DateTime fecha);
        Task<List<Tuple<int, string, string>>> obtenerNombreEjercicios();
        Task<Ejercicio> obtenerDescripcionEjercicio(int idEjercicio);
        Task<List<Tuple<DateTime, bool, bool>>> obtenerTodasRutinas(int idUsuario);
        Task<List<Rutina>> obtenerRutinaDiaria(int idUsuario, DateTime fecha);
        Task<Rutina> obtenerRutinaEjercicio(int idRutina);
        Task<bool> eliminarEjercicioRutina(int idRutina);
        Task<bool> updateEjercicioRutina(Rutina rutina);
    }
}
