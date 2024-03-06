﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace fitfuet.back.Models
{
    public class Rutina: EntidadBase
    {
        [Required]
        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }
        [Required]
        public int IdEjercicio { get; set; }
        [ForeignKey("IdEjercicio")]
        public Ejercicio Ejercicio { get; set; }
        [Required]
        public int Series { get; set; }
        [Required]
        public int Repeticionesc { get; set; }
        [Required]
        public double Peso { get; set; }
        [Required]
        public DateTime Fecha { get; set; }

    }
}
