using System.Collections.Generic;
using System.Threading.Tasks;
using APISistemaGestaoViagens.Model.DTOs;
using APISistemaGestaoViagens.Model.Entities;

namespace APISistemaGestaoViagens.Services.Interfaces;

public interface IClienteService
{
    Task<IEnumerable<ClienteDTO>> GetAllClientesAsync();
    Task<ClienteDTO> GetClienteByIdAsync(int id);
    Task<ClienteDTO> CreateClienteAsync(ClienteCreateDTO clienteCreateDto);
    Task UpdateClienteAsync(Cliente cliente);
    Task DeleteClienteAsync(int id);
}