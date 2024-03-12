﻿using fit_fuet_back.IRepositorios;
using fit_fuet_back.IServicios;
using fitfuet.back.Models;
using fitfuet.back.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fit_fuet_back.Servicios
{
    public class UsuarioServicio : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioServicio(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<int> Register(Usuario usuario)
        {
            if (await _usuarioRepository.Exist(usuario))
                return 1;
            await _usuarioRepository.Register(usuario);
            return 0;
        }

        public async Task<Usuario> Login(string email, string passwd)
        {
            var usuario = await _usuarioRepository.Login(email, passwd);
            return usuario;
        }

        public async Task<Usuario> GetUser(string email)
        {
            var usuario = await _usuarioRepository.GetUser(email);
            return usuario;
        }

        //Cuando olvidas la contraseña
        public async Task<bool> ChangePasswd(Usuario usuario, string newPasswd)
        {
            var check = await _usuarioRepository.ChangePasswd(usuario, newPasswd);
            return check;
        }

        //Cuando sabes la contraseña
        public async Task<bool> cambiarPasswd(int idUsuario, string nuevaPassword)
        {
            var check = await _usuarioRepository.cambiarPasswd(idUsuario, nuevaPassword);
            return check;
        }

        public async Task<bool> CambiarEstadoCuenta(Usuario usuario, int nuevoEstado)
        {
            try
            {
                usuario.CuentaActiva = nuevoEstado;
                var check = await _usuarioRepository.UpdateUsuario(usuario);
                return check;
            }
            catch (Exception)
            {
                // Manejar excepciones según tus necesidades
                return false;
            }
        }

        public async Task<string> obtenerFotoUsuario(int idUsuario)
        {
            var usuario = await _usuarioRepository.GetUser(idUsuario);
            return usuario.Foto;
        }
        public async Task<Usuario> actualizarDatosUsuario(UsuarioActualizado _usuario)
        {
            var usuario = await _usuarioRepository.ActualizarDatosUsuario(_usuario);
            return usuario;
        }

        public async Task<List<Tuple<float, float, DateTime, float>>> obtenerDatosCorporales(int idUsuario)
        {
            return await _usuarioRepository.obtenerDatosCorporales(idUsuario);
        }

        public async Task<List<Tuple<float, float, DateTime, float>>> obtenerUltimosDatosCorporales(int idUsuario)
        {
            return await _usuarioRepository.obtenerUltimosDatosCorporales(idUsuario);
        }
    }
}
