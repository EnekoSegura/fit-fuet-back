using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace fitfuet.back.Models
{
    public class Suenio : EntidadBase
    {
        [Required]
        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }
        [Required]
        public DateTime HoraAcostar { get; set; }
        [Required]
        public DateTime HoraLevantar { get; set; }
        [Required]
        public string Calidad { get; set; }
        [Required]
        public int NumLevantar { get; set; }
    }
}