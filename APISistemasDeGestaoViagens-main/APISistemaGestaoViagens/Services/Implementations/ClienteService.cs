using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APISistemaGestaoViagens.Model.Entities;
using APISistemaGestaoViagens.Repository.Interfaces;
using APISistemaGestaoViagens.Model.DTOs;
using APISistemaGestaoViagens.Services.Interfaces;

namespace APISistemaGestaoViagens.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<IEnumerable<ClienteDTO>> GetAllClientesAsync()
    {
        var clientes = await _clienteRepository.GetAllWithReservasAsync();
        return clientes.Select(c => new ClienteDTO
        {
            ClienteId = c.ClienteId,
            Nome = c.Nome,
            Email = c.Email,
            Telefone = c.Telefone,
            Cpf = c.Cpf,
            Reservas = c.Reservas.Select(r => new ReservaDTO
            {
                ReservaId = r.ReservaId,
                ViagemId = r.ViagemId,
                DataReserva = r.DataReserva,
                StatusPagamento = r.StatusPagamento,
                MetodoPagamento = r.MetodoPagamento,
                CustoTotal = r.CustoTotal,
                Viagem = r.Viagem == null ? null : new ViagemDTO
                {
                    ViagemId = r.Viagem.ViagemId,
                    DestinoId = r.Viagem.DestinoId,
                    DataPartida = r.Viagem.DataPartida,
                    DataRetorno = r.Viagem.DataRetorno,
                    Status = r.Viagem.Status
                }
            }).ToList()
        });
    }

    public async Task<ClienteDTO> GetClienteByIdAsync(int id)
    {
        var cliente = await _clienteRepository.GetByIdWithReservasAsync(id);
        if (cliente == null)
        {
            return null;
        }

        return new ClienteDTO
        {
            ClienteId = cliente.ClienteId,
            Nome = cliente.Nome,
            Email = cliente.Email,
            Telefone = cliente.Telefone,
            Cpf = cliente.Cpf,
            Reservas = cliente.Reservas.Select(r => new ReservaDTO
            {
                ReservaId = r.ReservaId,
                ViagemId = r.ViagemId,
                DataReserva = r.DataReserva,
                StatusPagamento = r.StatusPagamento,
                MetodoPagamento = r.MetodoPagamento,
                CustoTotal = r.CustoTotal,
                Viagem = r.Viagem == null ? null : new ViagemDTO
                {
                    ViagemId = r.Viagem.ViagemId,
                    DestinoId = r.Viagem.DestinoId,
                    DataPartida = r.Viagem.DataPartida,
                    DataRetorno = r.Viagem.DataRetorno,
                    Status = r.Viagem.Status
                }
            }).ToList()
        };
    }

    public async Task<ClienteDTO> CreateClienteAsync(ClienteCreateDTO clienteCreateDto)
    {
        var cliente = new Cliente
        {
            Nome = clienteCreateDto.Nome,
            Email = clienteCreateDto.Email,
            Telefone = clienteCreateDto.Telefone,
            Cpf = clienteCreateDto.Cpf
        };

        await _clienteRepository.AddAsync(cliente);

        return new ClienteDTO
        {
            ClienteId = cliente.ClienteId,
            Nome = cliente.Nome,
            Email = cliente.Email,
            Telefone = cliente.Telefone,
            Cpf = cliente.Cpf,
            Reservas = new List<ReservaDTO>() // Inicialmente sem reservas
        };
    }

    public async Task UpdateClienteAsync(Cliente cliente)
    {
        await _clienteRepository.UpdateAsync(cliente);
    }

    public async Task DeleteClienteAsync(int id)
    {
        await _clienteRepository.DeleteAsync(id);
    }
}