﻿using fitfuet.back.Controllers;
using fitfuet.back.Models;
using Microsoft.AspNetCore.Mvc;
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
    }
}
