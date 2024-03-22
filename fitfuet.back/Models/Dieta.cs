using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace fitfuet.back.Models
{
    public class Dieta: EntidadBase
    {
        [Required]
        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }
        [Required]
        public int IdAlimento { get; set; }
        [ForeignKey("IdEjercicio")]
        public Alimentos Alimento { get; set; }
        [Required]
        public double Cantidad { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
    }
}
