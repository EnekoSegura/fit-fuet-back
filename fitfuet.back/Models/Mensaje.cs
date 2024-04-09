using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace fitfuet.back.Models
{
    public class Mensaje: EntidadBase
    {
        [Required]
        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }
        [Required]
        public string MensajeUsuario { get; set; }
        [Required]
        public string FechaMensaje { get; set; }
    }
}
