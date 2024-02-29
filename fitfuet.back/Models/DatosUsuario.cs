using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace fitfuet.back.Models
{
    public class DatosUsuario: EntidadBase
    {
        [Required]
        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }
        [Required]
        public float Peso { get; set; } //peso en kg
        [Required]
        public float Altura { get; set; } //altura en cms
        [Required]
        public DateTime FechaRegistro { get; set; }
        [Required]
        public int RegistroActivo { get; set; } //0 activa, 1 inactiva
    }
}
