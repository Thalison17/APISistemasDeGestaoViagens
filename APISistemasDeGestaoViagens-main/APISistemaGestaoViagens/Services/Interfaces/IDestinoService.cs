using System.Collections.Generic;
using System.Threading.Tasks;
using APISistemaGestaoViagens.Model.DTOs;
using APISistemaGestaoViagens.Model.Entities;

namespace APISistemaGestaoViagens.Services.Interfaces
{
    public interface IDestinoService
    {
        Task<IEnumerable<DestinoDTO>> GetAllAsync();
        Task<DestinoDTO?> GetByIdAsync(int id);
        Task<DestinoDTO> CreateAsync(DestinoCreateDTO destinoDto);
        Task<bool> UpdateAsync(int id, DestinoDTO destinoDto);
        Task<bool> DeleteAsync(int id);
    }
}
