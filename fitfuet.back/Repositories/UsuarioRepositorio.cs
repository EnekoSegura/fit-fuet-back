using fit_fuet_back.Context;
using fit_fuet_back.IRepositorios;
using fitfuet.back.Models;
using fitfuet.back.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace fit_fuet_back.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepository
    {

        private readonly AplicationDbContext _context;

        public UsuarioRepositorio(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task Register([FromBody] Usuario usuario)
        {
            await _context.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exist([FromBody] Usuario usuario)
        {
            var validateExistence = await _context.Usuario.AnyAsync(x => x.Dni == usuario.Dni && usuario.CuentaActiva == 1 
                                || x.Email == usuario.Email && usuario.CuentaActiva == 1);
            return validateExistence;
        }

        public async Task<Usuario> Login(string email, string passwd)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.Email == email && x.Passwd == passwd 
                        && x.CuentaActiva == 0);
            return usuario;
        }

        public async Task<Usuario> GetUser(string email)
        {
            var validateExistence = await _context.Usuario.FirstOrDefaultAsync(x => x.Email == email && x.CuentaActiva == 0);
            return validateExistence;
        }

        public async Task<Usuario> GetUser(int idUser)
        {
            var validateExistence = await _context.Usuario.FirstOrDefaultAsync(x => x.Id == idUser && x.CuentaActiva == 0);
            return validateExistence;
        }

        //Cuando olvidas la contraseña
        public async Task<bool> ChangePasswd(Usuario usuario, string newPasswd)
        {
            var userExists = await _context.Usuario.AnyAsync(x => x.Dni == usuario.Dni && x.Email == usuario.Email);

            if (userExists)
            {
                usuario.Passwd = newPasswd;
                _context.Usuario.Update(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        //Cuando sabes la contraseña
        public async Task<bool> cambiarPasswd(int idUsuario, string nuevaPassword)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.Id == idUsuario);
            if(usuario != null)
            {
                usuario.Passwd = nuevaPassword;
                _context.Usuario.Update(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateUsuario(Usuario usuario)
        {
            try
            {
                _context.Usuario.Update(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
