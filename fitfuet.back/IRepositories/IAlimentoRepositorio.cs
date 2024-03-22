using fitfuet.back.Controllers;
using fitfuet.back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fit_fuet_back.IRepositorios
{
    public interface IAlimentoRepositorio
    {
        Task<List<Alimentos>> obtenerTodosAlimentos();
        Task<bool> insertarAlimentacion(Dieta dieta);
        Task<List<Dieta>> obtenerDietaPorDiaYUsuario(int idUsuario, DateTime fecha);
    }
}
