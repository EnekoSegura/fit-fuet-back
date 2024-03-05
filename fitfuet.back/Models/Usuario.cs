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
        [Column(TypeName = "varchar(100)")]
        public string Passwd { get; set; }
        [Required]
        public int CuentaActiva { get; set; } //0 activa, 1 inactiva
        [Column(TypeName = "nvarchar(MAX)")]
        public string Foto { get; set; }
    }

    public class UsuarioActualizado : EntidadBase
    {
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Foto { get; set; }
    }
}
