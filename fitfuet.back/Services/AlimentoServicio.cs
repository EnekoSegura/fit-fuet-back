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
    public class AlimentoServicio : IAlimentoServicio
    {
        private readonly IAlimentoRepositorio _alimentoRepositorio;

        public AlimentoServicio(IAlimentoRepositorio alimentoRepositorio)
        {
            _alimentoRepositorio = alimentoRepositorio;
        }

        public async Task<List<Alimentos>> obtenerTodosAlimentos()
        {
            return await _alimentoRepositorio.obtenerTodosAlimentos();
        }

        public async Task<bool> insertarAlimentacion(Dieta dieta)
        {
            return await _alimentoRepositorio.insertarAlimentacion(dieta);
        }

        public async Task<List<Dieta>> obtenerDietaPorDiaYUsuario(int idUsuario, DateTime fecha)
        {
            return await _alimentoRepositorio.obtenerDietaPorDiaYUsuario(idUsuario, fecha);
        }

        public async Task<string> obtenerRecomendacion(double porcentajeCarbo, double porcentajeProte, double porcentajeGrasa)
        {
            bool entrarEnCarbo = false;
            bool entrarEnProte = false;

            if(porcentajeCarbo == porcentajeProte)
            {
                Random rnd = new Random();
                if (rnd.Next(0, 1) == 0)
                    entrarEnCarbo = true;
                else
                    entrarEnProte = true;
            } 
            else if(porcentajeCarbo == porcentajeGrasa)
            {
                Random rnd = new Random();
                if (rnd.Next(0, 1) == 0)
                    entrarEnCarbo = true;
            }
            else if (porcentajeProte == porcentajeGrasa)
            {
                Random rnd = new Random();
                if (rnd.Next(0, 1) == 0)
                    entrarEnProte = true;
            }
            else
            {
                entrarEnCarbo = true;
                entrarEnProte = true;
            }

            if (porcentajeCarbo < porcentajeProte && porcentajeCarbo < porcentajeGrasa && entrarEnCarbo)
            {
                var listaNombresAlimentos = await _alimentoRepositorio.obtenerRecomendacion(50, "carbo");

                Random rnd = new Random();
                int indiceAleatorio = rnd.Next(0, listaNombresAlimentos.Count - 1);
                return "Nosotros te recomendamos comer algo de |" + listaNombresAlimentos[indiceAleatorio] + "| debido a que te faltan carbohidratos";
            }
            else if(porcentajeProte < porcentajeGrasa && entrarEnProte)
            {
                var listaNombresAlimentos =  await _alimentoRepositorio.obtenerRecomendacion(20, "prote");

                Random rnd = new Random();
                int indiceAleatorio = rnd.Next(0, listaNombresAlimentos.Count - 1);
                return "Nosotros te recomendamos comer algo de |" + listaNombresAlimentos[indiceAleatorio] + "| debido a que te faltan proteinas";
            }
            else
            {
                var listaNombresAlimentos =  await _alimentoRepositorio.obtenerRecomendacion(20);

                Random rnd = new Random();
                int indiceAleatorio = rnd.Next(0, listaNombresAlimentos.Count - 1);
                return "Nosotros te recomendamos comer algo de |" + listaNombresAlimentos[indiceAleatorio] + "| debido a que te faltan grasas";
            }
        }
    }
}
