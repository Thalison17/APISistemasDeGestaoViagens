using APISistemaGestaoViagens.Data;
using APISistemaGestaoViagens.Model.Entities;
using APISistemaGestaoViagens.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APISistemaGestaoViagens.Repository.Implementations;

public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cliente>> GetAllWithReservasAsync()
    {
        return await _context.Clientes
            .Include(c => c.Reservas)
            .ThenInclude(r => r.Viagem)
            .ToListAsync();
    }

    public async Task<Cliente> GetByIdWithReservasAsync(int id)
    {
        return await _context.Clientes
            .Include(c => c.Reservas)
            .ThenInclude(r => r.Viagem)
            .FirstOrDefaultAsync(c => c.ClienteId == id);
    }
}