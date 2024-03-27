using fitfuet.back.Controllers;
using fitfuet.back.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fit_fuet_back.IServicios
{
    public interface IAlimentoServicio
    {
        Task<List<Alimentos>> obtenerTodosAlimentos();
        Task<bool> insertarAlimentacion(Dieta dieta);
        Task<List<Dieta>> obtenerDietaPorDiaYUsuario(int idUsuario, DateTime fecha);
        Task<string> obtenerRecomendacion(double porcentajeCarbo, double porcentajeProte, double porcentajeGrasa);
    }
}
