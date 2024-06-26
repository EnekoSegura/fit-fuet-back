﻿using fitfuet.back.Controllers;
using fitfuet.back.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fit_fuet_back.IServicios
{
    public interface IEjercicioServicio
    {

        Task<List<EjercicioObjeto>> obtenerListaEjercios();
        Task<bool> insertarRutina(Rutina[] rutina);
        Task<Rutina[]> obtenerRutina(int idUsuario, DateTime fecha);
        Task<List<Tuple<int, string, string>>> obtenerNombreEjercicios();
        Task<Ejercicio> obtenerDescripcionEjercicio(int idEjercicio);
        Task<List<Tuple<DateTime, bool, bool>>> obtenerTodasRutinas(int idUsuario);
        Task<List<Rutina>> obtenerRutinaDiaria(int idUsuario, string fecha);
        Task<Rutina> obtenerRutinaEjercicio(int idRutina);
        Task<bool> eliminarEjercicioRutina(int idRutina);
        Task<bool> updateEjercicioRutina(Rutina rutina);
    }
}
