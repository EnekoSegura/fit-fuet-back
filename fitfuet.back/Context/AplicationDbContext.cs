using fitfuet.back.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace fit_fuet_back.Context
{
    public class AplicationDbContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<DatosUsuario> DatosUsuario { get; set; }
        public DbSet<Ejercicio> Ejercicio { get; set; }
        public DbSet<Rutina> Rutina { get; set; }
        public DbSet<Alimentos> Alimentos { get; set; }

        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }
    }
}
