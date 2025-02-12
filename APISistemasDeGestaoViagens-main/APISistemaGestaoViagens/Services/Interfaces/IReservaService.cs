using APISistemaGestaoViagens.Model.DTOs;

namespace APISistemaGestaoViagens.Services.Interfaces
{
    public interface IReservaService
    {
        Task<IEnumerable<ReservaDTO>> GetAllAsync();
        Task<ReservaDTO?> GetByIdAsync(int id);
        Task<ReservaDTO> CreateAsync(ReservaCreateDTO reservaCreateDto);
        Task<bool> UpdateAsync(int id, ReservaDTO reservaDto);
        Task<bool> DeleteAsync(int id);
    }
}