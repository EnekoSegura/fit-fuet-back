using fit_fuet_back.Context;
using fit_fuet_back.IRepositorios;
using fitfuet.back.Models;
using fitfuet.back.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var validateExistence = await _context.Usuario.AnyAsync(x => x.Dni == usuario.Dni || x.Email == usuario.Email);
            return validateExistence;
        }

        public async Task<Usuario> Login(string email, string passwd)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.Email == email && x.Passwd == passwd);

            return usuario;
        }

        public async Task<Usuario> GetUser(string email)
        {
            var validateExistence = await _context.Usuario.FirstOrDefaultAsync(x => x.Email == email);
            return validateExistence;
        }

        public async Task<bool> ChangePasswd(Usuario usuario, string newPasswd)
        {
            var userExists = await _context.Usuario.AnyAsync(x => x.Dni == usuario.Dni && x.Email == usuario.Email);

            if (userExists)
            {
                usuario.Passwd = Encriptar.EncriptarPassword(newPasswd);
                _context.Usuario.Update(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

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
    }
}
