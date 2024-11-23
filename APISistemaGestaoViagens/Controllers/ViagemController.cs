using Microsoft.AspNetCore.Mvc;
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
            ViagemId = v.ViagemId,
            DestinoId = v.DestinoId,
            DataPartida = v.DataPartida,
            DataRetorno = v.DataRetorno,
            Status = v.Status
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
            ViagemId = viagem.ViagemId,
            DestinoId = viagem.DestinoId,
            DataPartida = viagem.DataPartida,
            DataRetorno = viagem.DataRetorno,
            Status = viagem.Status
        };

        return Ok(viagemDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, ViagemDTO viagemDto)
    {
        if (id != viagemDto.ViagemId) 
            return BadRequest("ID da viagem não corresponde.");

        var viagem = await _repository.GetByIdAsync(id);
        if (viagem == null) 
            return NotFound("Viagem não encontrada.");

        viagem.Status = viagemDto.Status;

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
