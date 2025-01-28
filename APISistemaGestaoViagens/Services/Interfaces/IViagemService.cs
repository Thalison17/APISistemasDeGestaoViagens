using APISistemaGestaoViagens.Model.DTOs;

namespace APISistemaGestaoViagens.Services.Interfaces
{
    public interface IViagemService
    {
        Task<IEnumerable<ViagemDTO>> GetAllAsync();
        Task<ViagemDTO?> GetByIdAsync(int id);
    }
}