using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace fitfuet.back.Models
{
    public class Alimentos: EntidadBase
    {
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Nombre { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string TipoAlimento { get; set; }
        [Required]
        public float Calorias { get; set; }
        [Required]
        public float Carbohidratos { get; set; }
        [Required]
        public float Proteinas { get; set; }
        [Required]
        public float Grasas { get; set; }
        [Required]
        public float Fibra { get; set; }
    }
}
