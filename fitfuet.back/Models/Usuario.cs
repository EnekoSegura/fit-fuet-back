using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace fitfuet.back.Models
{
    public class Usuario : EntidadBase
    {

        [Required]
        [Column(TypeName = "varchar(9)")]
        public string Dni { get; set; }
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Nombre { get; set; }
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Apellido { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")] //del front llega un string de máximo 20 caracteres
        public string Passwd { get; set; }
    }
}
