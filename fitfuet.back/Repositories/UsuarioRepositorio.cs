using fit_fuet_back.Context;
using fit_fuet_back.IRepositorios;
using fitfuet.back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> Login(string email, string passwd)
        {
            var validateExistence = await _context.Usuario.AnyAsync(x => x.Email == email && x.Passwd == passwd);
            return validateExistence;
        }
    }
}
