using APISistemaGestaoViagens.Model.Entities;

namespace APISistemaGestaoViagens.Repository.Interfaces;

public interface IClienteRepository : IGenericRepository<Cliente>
{
    Task<IEnumerable<Cliente>> GetAllWithReservasAsync();
    Task<Cliente> GetByIdWithReservasAsync(int id);
}