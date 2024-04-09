using fit_fuet_back.Context;
using fitfuet.back.IRepositories;
using fitfuet.back.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fitfuet.back.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly AplicationDbContext _context;
        public ChatRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Mensaje>> GetMensajes()
        {
            try
            {
                var mensajesList = await _context.Mensaje
                                    .Include(m => m.Usuario)
                                    .ToListAsync();

                return mensajesList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task InsertarMensaje(int idUsuario, string mensaje, string fechaMensaje)
        {
            Mensaje msg = new Mensaje();
            msg.IdUsuario = idUsuario;
            msg.MensajeUsuario = mensaje;
            msg.FechaMensaje = fechaMensaje;

            _context.Update(msg);
            await _context.SaveChangesAsync();
        }
    }
}
