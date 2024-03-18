using System.ComponentModel.DataAnnotations.Schema;
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
        public int Series { get; set; }
        public int Repeticiones { get; set; }
        public double Peso { get; set; }
        public double Tiempo { get; set; }
        [Required]
        public DateTime Fecha { get; set; }

    }
}
