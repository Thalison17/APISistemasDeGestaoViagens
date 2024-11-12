using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APISistemaGestaoViagens.Model.Entities;
using APISistemaGestaoViagens.Model.DTOs;
using APISistemaGestaoViagens.Repository.Interfaces;

namespace APISistemaGestaoViagens.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ViagemController : ControllerBase
{
    private readonly IGenericRepository<Viagem> _repository;

    public ViagemController(IGenericRepository<Viagem> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ViagemDTO>>> GetAll()
    {
        var viagens = await _repository.GetAllAsync();
        var viagensDto = viagens.Select(v => new ViagemDTO
        {
            ClienteId = v.ClienteId,
            DestinoId = v.DestinoId,
            DataPartida = v.DataPartida,
            DataRetorno = v.dataRetorno,
            CustoTotal = v.CustoTotal,
        });
        return Ok(viagensDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ViagemDTO>> GetById(int id)
    {
        var viagem = await _repository.GetByIdAsync(id);
        if (viagem == null) return NotFound("Viagem não encontrada.");

        var viagemDto = new ViagemDTO
        {
            ClienteId = viagem.ClienteId,
            DestinoId = viagem.DestinoId,
            DataPartida = viagem.DataPartida,
            DataRetorno = viagem.dataRetorno,
            CustoTotal = viagem.CustoTotal
        };

        return Ok(viagemDto);
    }

    [HttpPost]
    public async Task<ActionResult> Create(ViagemDTO viagemDto)
    {
        var viagem = new Viagem
        {
            ClienteId = viagemDto.ClienteId,
            DestinoId = viagemDto.DestinoId,
            DataPartida = viagemDto.DataPartida,
            dataRetorno = viagemDto.DataRetorno,
            CustoTotal = viagemDto.CustoTotal
        };

        await _repository.AddAsync(viagem);
        return CreatedAtAction(nameof(GetById), new { id = viagem.ViagemId }, viagem);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, ViagemDTO viagemDto)
    {
        if (id != viagemDto.ViagemId) return BadRequest("ID da viagem não corresponde.");

        var viagem = new Viagem
        {
            ViagemId = viagemDto.ViagemId,
            ClienteId = viagemDto.ClienteId,
            DestinoId = viagemDto.DestinoId,
            DataPartida = viagemDto.DataPartida,
            dataRetorno = viagemDto.DataRetorno,
            CustoTotal = viagemDto.CustoTotal
        };

        await _repository.UpdateAsync(viagem);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
