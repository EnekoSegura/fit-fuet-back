using fit_fuet_back.Context;
using fit_fuet_back.IRepositorios;
using fitfuet.back.Migrations;
using fitfuet.back.Models;
using fitfuet.back.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task Register(Usuario usuario)
        {
            await _context.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exist(Usuario usuario)
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

        public async Task<Usuario> ActualizarDatosUsuario(UsuarioActualizado _usuarioActualizado)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.Email == _usuarioActualizado.Email && x.CuentaActiva == 0);
            usuario.Nombre = _usuarioActualizado.Nombre;
            usuario.Apellido = _usuarioActualizado.Apellido;
            usuario.Email = _usuarioActualizado.Email;
            usuario.Dni = _usuarioActualizado.Dni;
            usuario.Foto = _usuarioActualizado.Foto;
            await UpdateUsuario(usuario);
            return usuario;
        }

        public async Task<ActionResult<List<Tuple<int, float, float, DateTime, float>>>> obtenerDatosCorporales(int idUsuario)
        {
            var datosUsuario = await _context.Set<DatosUsuario>()
                .Where(u => u.IdUsuario == idUsuario && u.RegistroActivo == 0)
                .OrderByDescending(u => u.FechaRegistro)
                .Select(u => new Tuple<int,float, float, DateTime, float>(
                    u.Id,
                    u.Altura,
                    u.Peso,
                    u.FechaRegistro,
                    u.Peso / ((u.Altura / 100) * (u.Altura / 100))
                )).ToListAsync();

            return datosUsuario;
        }

        public async Task<List<Tuple<float, float, DateTime, float>>> obtenerUltimosDatosCorporales(int idUsuario)
        {
            var datosUsuario = await _context.Set<DatosUsuario>()
                .Where(u => u.IdUsuario == idUsuario && u.RegistroActivo == 0)
                .OrderByDescending(u => u.FechaRegistro)
                .Take(7)
                .OrderBy(u => u.FechaRegistro)
                .Select(u => new Tuple<float, float, DateTime, float>(
                    u.Altura,
                    u.Peso,
                    u.FechaRegistro,
                    u.Peso / ((u.Altura/100) * (u.Altura / 100))
                )).ToListAsync();

            return datosUsuario;
        }

        public async Task<Tuple<float, float, DateTime>> obtenerUltimoDato(int idUsuario)
        {
            var datoUsuario = await _context.Set<DatosUsuario>()
                .Where(u => u.IdUsuario == idUsuario && u.RegistroActivo == 0)
                .OrderByDescending(u => u.FechaRegistro)
                .FirstAsync();

            return new Tuple<float, float, DateTime>(
                datoUsuario.Altura,
                datoUsuario.Peso,
                datoUsuario.FechaRegistro
            );
        }

        public async Task<bool> agregarDato(DatosUsuariosInsertar datoUsuario)
        {
            DateTime fecha;
            try
            {
                DatosUsuario dato = new DatosUsuario();
                dato.IdUsuario = datoUsuario.IdUsuario;
                dato.Altura = datoUsuario.Altura;
                dato.Peso = datoUsuario.Peso;
                if (DateTime.TryParseExact(datoUsuario.FechaRegistro, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out fecha))
                {
                    // Parsing successful, you can now use the 'fechaRegistro' variable
                    dato.FechaRegistro = fecha;
                }
                dato.RegistroActivo = datoUsuario.RegistroActivo;
                _context.DatosUsuario.Add(dato);
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
