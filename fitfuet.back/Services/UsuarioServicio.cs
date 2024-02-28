﻿using fit_fuet_back.IRepositorios;
using fit_fuet_back.IServicios;
using fitfuet.back.Models;
using fitfuet.back.Utils;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<int> Register([FromBody] Usuario usuario)
        {
            if (await _usuarioRepository.Exist(usuario))
                return 1;
            usuario.Passwd = Encriptar.EncriptarPassword(usuario.Passwd);
            await _usuarioRepository.Register(usuario);
            return 0;
        }

        public async Task<Usuario> Login(string email, string passwd)
        {
            var passwdEncriptada = Encriptar.EncriptarPassword(passwd);
            var usuario = await _usuarioRepository.Login(email, passwdEncriptada);
            return usuario;
        }

        public async Task<Usuario> GetUser(string email)
        {
            var usuario = await _usuarioRepository.GetUser(email);
            return usuario;
        }

        public async Task<bool> ChangePasswd(Usuario usuario, string newPasswd)
        {
            var check = await _usuarioRepository.ChangePasswd(usuario, newPasswd);
            return check;
        }

        public async Task<bool> cambiarPasswd(int idUsuario, string nuevaPassword)
        {
            var check = await _usuarioRepository.cambiarPasswd(idUsuario, nuevaPassword);
            return check;
        }
    }
}
