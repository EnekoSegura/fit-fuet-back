using fit_fuet_back.IRepositorios;
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

        public async Task<string> GetUsername(int idUsuario)
        {
            var usuario = await _usuarioRepository.GetUser(idUsuario);
            return usuario.Nombre + " " + usuario.Apellido;
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

        public async Task<ActionResult<List<Tuple<int, float, float, DateTime, float>>>> obtenerDatosCorporales(int idUsuario)
        {
            return await _usuarioRepository.obtenerDatosCorporales(idUsuario);
        }

        public async Task<List<Tuple<float, float, DateTime, float>>> obtenerUltimosDatosCorporales(int idUsuario)
        {
            return await _usuarioRepository.obtenerUltimosDatosCorporales(idUsuario);
        }

        public async Task<Tuple<float, float, DateTime>> obtenerUltimoDato(int idUsuario)
        {
            return await _usuarioRepository.obtenerUltimoDato(idUsuario);
        }

        public async Task<int> agregarDato(DatosUsuariosInsertar datoUsuario)
        {
            if(await _usuarioRepository.buscarFechaDatoCorporal(datoUsuario))
                return 2;
            return await _usuarioRepository.agregarDato(datoUsuario);
        }

        public async Task<int> editarDato(int idDatoCorporal, DatosUsuariosInsertar datoUsuario)
        {
            var dato = await _usuarioRepository.buscarDatosUsuario(idDatoCorporal, datoUsuario);
            if (dato != null)
            {
                if (await _usuarioRepository.buscarFechaDatoCorporal(dato))
                    return 2;
                if (await _usuarioRepository.editarDato(dato))
                    return 0;
                return 3;
            }
            return 1;
        }

        public async Task<bool> eliminarDatoCorporal(int idDatoCorporal)
        {
            return await _usuarioRepository.eliminarDatoCorporal(idDatoCorporal);
        }

        public async Task<float> obtenerUltimaAltura(int idUsuario)
        {
            return await _usuarioRepository.obtenerUltimaAltura(idUsuario);
        }

        public async Task<bool> addSuenio(Suenio suenio)
        {
            var existe = await _usuarioRepository.existeDiaLevantar(suenio.IdUsuario, suenio.HoraLevantar);

            if (existe)
                return false;

            return await _usuarioRepository.addSuenio(suenio);
        }

        public async Task<int> obtenerModo(int idUsuario)
        {
            return await _usuarioRepository.obtenerModo(idUsuario);
        }

        public async Task<bool> cambiarModo(int idUsuario, int nuevoModo)
        {
            var usuario = await _usuarioRepository.GetUser(idUsuario);

            if (usuario != null)
            {
                if(await _usuarioRepository.cambiarModo(usuario, nuevoModo))
                    return true;
                return false;
            }
            return false;
        }

        public async Task<List<Suenio>> obtenerListaSuenio(int idUsuario)
        {
            return await _usuarioRepository.obtenerListaSuenio(idUsuario);
        }

        public async Task<Suenio> obtenerSuenio(int idUsuario, DateTime horaLevantar)
        {
            return await _usuarioRepository.obtenerSuenio(idUsuario, horaLevantar);
        }

        public async Task<bool> updateSuenio(Suenio suenio)
        {
           return await _usuarioRepository.updateSuenio(suenio);
        }

    }
}
