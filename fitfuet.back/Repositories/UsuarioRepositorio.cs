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

        public async Task<int> agregarDato(DatosUsuariosInsertar datoUsuario)
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
                return 0;
            }
            catch (Exception)
            {
                return 3;
            }
        }

        public async Task<DatosUsuario> buscarDatosUsuario(int idDatosUsuario, DatosUsuariosInsertar datos)
        {
            DateTime fecha;
            try
            {
                var datosUsuario = await _context.DatosUsuario.FirstOrDefaultAsync(x => x.Id == idDatosUsuario);
                datosUsuario.Altura = datos.Altura;
                datosUsuario.Peso = datos.Peso;
                if (DateTime.TryParseExact(datos.FechaRegistro, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out fecha))
                {
                    // Parsing successful, you can now use the 'fecha' variable
                    datosUsuario.FechaRegistro = new DateTime(fecha.Year, fecha.Month, fecha.Day);
                }
                datosUsuario.RegistroActivo = 0;
                return datosUsuario;
            } 
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> editarDato(DatosUsuario datosUsuario)
        {
            try
            {
                _context.DatosUsuario.Update(datosUsuario);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> buscarFechaDatoCorporal(DatosUsuario datosUsuario)
        {
            try
            {
                if (await _context.DatosUsuario.FirstOrDefaultAsync(x => x.FechaRegistro == datosUsuario.FechaRegistro && x.Id != datosUsuario.Id && x.IdUsuario == datosUsuario.IdUsuario) != null)
                    return true;
                return false;
            } 
            catch (Exception)
            {
                return true;
            }
        }

        public async Task<bool> buscarFechaDatoCorporal(DatosUsuariosInsertar datosUsuario)
        {

            DateTime fecha;
            try
            {
                DatosUsuario dato = new DatosUsuario();
                if (DateTime.TryParseExact(datosUsuario.FechaRegistro, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out fecha))
                {
                    // Parsing successful, you can now use the 'fechaRegistro' variable
                    dato.FechaRegistro = fecha;
                }
                if (await _context.DatosUsuario.FirstOrDefaultAsync(x => x.FechaRegistro == dato.FechaRegistro && x.IdUsuario == dato.IdUsuario) != null)
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> eliminarDatoCorporal(int idDatoCorporal)
        {
            var dato = await _context.DatosUsuario.FirstOrDefaultAsync(x => x.Id == idDatoCorporal);
            if (dato != null)
            {
                dato.RegistroActivo = 1;
                _context.DatosUsuario.Update(dato);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
