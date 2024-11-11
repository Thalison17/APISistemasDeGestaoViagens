using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using APISistemaGestaoViagens.Model.Entities;
using APISistemaGestaoViagens.Repository.Interfaces;
using APISistemaGestaoViagens.Model.DTOs;

namespace APISistemaGestaoViagens.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClienteController : ControllerBase
{
    private readonly IGenericRepository<Cliente> _repository;

    public ClienteController(IGenericRepository<Cliente> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetAll()
    {
        try
        {
            var clientes = await _repository.GetAllAsync();
            var clientesDto = clientes.Select(c => new ClienteDTO
            {
                ClienteId = c.ClienteId,
                Nome = c.Nome,
                Email = c.Email,
                Telefone = c.Telefone
            });
            return Ok(clientes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao obter cliente: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClienteDTO>> GetById(int id)
    {
        try
        {
            var cliente = await _repository.GetByIdAsync(id);
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
    public async Task<ActionResult> Create(ClienteDTO clienteDto)
    {
        try
        {
            var cliente = new Cliente
            {
                Nome = clienteDto.Nome,
                Email = clienteDto.Email,
                Telefone = clienteDto.Telefone,
                Cpf = clienteDto.Cpf
            };

            await _repository.AddAsync(cliente);
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
        var existingCliente = await _repository.GetByIdAsync(id);
        if (existingCliente == null) return NotFound("Cliente não encontrado.");
        await _repository.UpdateAsync(cliente);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}