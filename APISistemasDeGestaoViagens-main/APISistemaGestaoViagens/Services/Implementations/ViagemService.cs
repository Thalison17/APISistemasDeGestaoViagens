using APISistemaGestaoViagens.Model.Entities;
using APISistemaGestaoViagens.Model.DTOs;
using APISistemaGestaoViagens.Repository.Interfaces;
using APISistemaGestaoViagens.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APISistemaGestaoViagens.Services
{
    public class ViagemService : IViagemService
    {
        private readonly IGenericRepository<Viagem> _viagemRepository;

        public ViagemService(IGenericRepository<Viagem> viagemRepository)
        {
            _viagemRepository = viagemRepository;
        }

        public async Task<IEnumerable<ViagemDTO>> GetAllAsync()
        {
            var viagens = await _viagemRepository.GetAllAsync();
            return viagens.Select(v => new ViagemDTO
            {
                ViagemId = v.ViagemId,
                DestinoId = v.DestinoId,
                DataPartida = v.DataPartida,
                DataRetorno = v.DataRetorno,
                Status = v.Status
            });
        }

        public async Task<ViagemDTO?> GetByIdAsync(int id)
        {
            var viagem = await _viagemRepository.GetByIdAsync(id);
            if (viagem == null) return null;

            return new ViagemDTO
            {
                ViagemId = viagem.ViagemId,
                DestinoId = viagem.DestinoId,
                DataPartida = viagem.DataPartida,
                DataRetorno = viagem.DataRetorno,
                Status = viagem.Status
            };
        }
    }
}