using APISistemaGestaoViagens.Data;
using APISistemaGestaoViagens.Model.Entities;
using APISistemaGestaoViagens.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace APISistemaGestaoViagens.Repository.Implementations
{
    public class DestinoRepository : IDestinoRepository
    {
        private readonly AppDbContext _context;

        public DestinoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Destino?> GetByIdAsync(int id)
        {
            return await _context.Destino.FindAsync(id);
        }
    }
}