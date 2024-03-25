using fitfuet.back.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace fit_fuet_back.IRepositorios
{
    public interface IUsuarioRepository
    {

        Task Register([FromBody] Usuario usuario);
        Task<bool> Exist([FromBody] Usuario usuario);
        Task<Usuario> Login(string email, string passwd);
        Task<Usuario> GetUser(string email);
        Task<Usuario> GetUser(int idUser);
        Task<bool> ChangePasswd(Usuario usuario, string newPasswd);
        Task<bool> cambiarPasswd(int idUsuario, string nuevaPassword);
        Task<bool> UpdateUsuario(Usuario usuario);
        Task<Usuario> ActualizarDatosUsuario(UsuarioActualizado _usuarioActualizado);
        Task<ActionResult<List<Tuple<int, float, float, DateTime, float>>>> obtenerDatosCorporales(int idUsuario);
        Task<List<Tuple<float, float, DateTime, float>>> obtenerUltimosDatosCorporales(int idUsuario);
        Task<Tuple<float, float, DateTime>> obtenerUltimoDato(int idUsuario);
        Task<int> agregarDato(DatosUsuariosInsertar datoUsuario);
        Task<bool> editarDato(DatosUsuario datosUsuario);
        Task<DatosUsuario> buscarDatosUsuario(int idDatosUsuario, DatosUsuariosInsertar datos);
        Task<bool> buscarFechaDatoCorporal(DatosUsuario datosUsuario);
        Task<bool> buscarFechaDatoCorporal(DatosUsuariosInsertar datosUsuario);
        Task<bool> eliminarDatoCorporal(int idDatoCorporal);
        Task<float> obtenerUltimaAltura(int idUsuario);
        Task<bool> addSuenio(Suenio suenio);
        Task<int> obtenerModo(int idUsuario);
        Task<bool> cambiarModo(Usuario usuario, int nuevoModo);
    }
}
