﻿using fitfuet.back.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace fit_fuet_back.IServicios
{
    public interface IUsuarioService
    {

        Task<int> Register([FromBody] Usuario usuario);
        Task<Usuario> Login(string email, string passwd);
        Task<Usuario> GetUser(string email);
        Task<bool> ChangePasswd(Usuario usuario, string newPasswd);
        Task<bool> cambiarPasswd(int idUsuario, string nuevaPassword);
        Task<bool> CambiarEstadoCuenta(Usuario usuario, int nuevoEstado);
        Task<string> obtenerFotoUsuario(int idUsuario);
        Task<Usuario> actualizarDatosUsuario([FromBody] UsuarioActualizado usuario);
        Task<ActionResult<List<Tuple<int, float, float, DateTime, float>>>> obtenerDatosCorporales(int idUsuario);
        Task<List<Tuple<float, float, DateTime, float>>> obtenerUltimosDatosCorporales(int idUsuario);
        Task<Tuple<float, float, DateTime>> obtenerUltimoDato(int idUsuario);
        Task<int> agregarDato(DatosUsuariosInsertar datoUsuario);
        Task<int> editarDato(int idDatoCorporal, DatosUsuariosInsertar datoUsuario);
        Task<bool> eliminarDatoCorporal(int idDatoCorporal);
        Task<float> obtenerUltimaAltura(int idUsuario);
        Task<bool> addSuenio(Suenio suenio);
        Task<int> obtenerModo(int idUsuario);
        Task<bool> cambiarModo(int idUsuario, int nuevoModo);
        Task<List<Suenio>> obtenerListaSuenio(int idUsuario);
        Task<Suenio> obtenerSuenio(int idUsuario, DateTime horaLevantar);
        Task<bool> updateSuenio(Suenio suenio);
    }
}
