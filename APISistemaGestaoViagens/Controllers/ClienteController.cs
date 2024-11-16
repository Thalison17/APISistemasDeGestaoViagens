using Microsoft.AspNetCore.Mvc;
using APISistemaGestaoViagens.Model.Entities;
using APISistemaGestaoViagens.Repository.Interfaces;
using APISistemaGestaoViagens.Model.DTOs;

namespace APISistemaGestaoViagens.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClienteController : ControllerBase
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteController(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetAll()
    {
        try
        {
            var clientes = await _clienteRepository.GetAllWithReservasAsync();

            var clientesDto = clientes.Select(c => new ClienteDTO
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

            return Ok(clientesDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao obter clientes: {ex.Message}");
        }
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<ClienteDTO>> GetById(int id)
    {
        try
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente == null) return NotFound("Cliente não encontrado.");

            var clienteDto = new ClienteDTO
            {
                ClienteId = cliente.ClienteId,
                Nome = cliente.Nome,
                Email = cliente.Email,
                Telefone = cliente.Telefone
            };

            return Ok(clienteDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao obter cliente: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create(ClienteCreateDTO clienteCreateDto)
    {
        try
        {
            var cliente = new Cliente
            {
                Nome = clienteCreateDto.Nome,
                Email = clienteCreateDto.Email,
                Telefone = clienteCreateDto.Telefone,
                Cpf = clienteCreateDto.Cpf
            };

            await _clienteRepository.AddAsync(cliente);
            return CreatedAtAction(nameof(GetById), new { id = cliente.ClienteId }, cliente);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao criar cliente: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, Cliente cliente)
    {
        if (id != cliente.ClienteId) return BadRequest("ID do cliente não corresponde.");
        var existingCliente = await _clienteRepository.GetByIdAsync(id);
        if (existingCliente == null) return NotFound("Cliente não encontrado.");
        await _clienteRepository.UpdateAsync(cliente);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _clienteRepository.DeleteAsync(id);
        return NoContent();
    }
}