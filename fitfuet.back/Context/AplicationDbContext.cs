﻿using fitfuet.back.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace fit_fuet_back.Context
{
    public class AplicationDbContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<DatosUsuario> DatosUsuario { get; set; }

        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }
    }
}
