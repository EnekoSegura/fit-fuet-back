using fit_fuet_back.IRepositorios;
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

        public async Task<bool> Login(string email, string passwd)
        {
            var passwdEncriptada = Encriptar.EncriptarPassword(passwd);
            if (await _usuarioRepository.Login(email, passwdEncriptada))
                return true;
            return false;
        }

    }
}
