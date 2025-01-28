using APISistemaGestaoViagens.Model.Entities;
using APISistemaGestaoViagens.Model.DTOs;
using APISistemaGestaoViagens.Repository.Interfaces;
using APISistemaGestaoViagens.Services.Interfaces;

namespace APISistemaGestaoViagens.Services

{
    public class DestinoService : IDestinoService
    {
        private readonly IGenericRepository<Destino> _repository;

        public DestinoService(IGenericRepository<Destino> repository)
        {
            _repository = repository;
        }


        public async Task<IEnumerable<DestinoDTO>> GetAllAsync()
        {
            var destinos = await _repository.GetAllAsync();
            return destinos.Select(d => new DestinoDTO
            {
                DestinoId = d.DestinoId,
                Localizacao = d.Localizacao,
                Pais = d.Pais,
                PrecoPorDia = d.PrecoPorDia
            });
        }

        public async Task<DestinoDTO?> GetByIdAsync(int id)
        {
            var destino = await _repository.GetByIdAsync(id);
            if (destino == null) return null;

            return new DestinoDTO
            {
                DestinoId = destino.DestinoId,
                Localizacao = destino.Localizacao,
                Pais = destino.Pais,
                PrecoPorDia = destino.PrecoPorDia
            };
        }

        public async Task<DestinoDTO> CreateAsync(DestinoCreateDTO destinoDto)
        {
            var destino = new Destino
            {
                Localizacao = destinoDto.Localizacao,
                Pais = destinoDto.Pais,
                PrecoPorDia = destinoDto.PrecoPorDia
            };

            await _repository.AddAsync(destino);

            return new DestinoDTO
            {
                DestinoId = destino.DestinoId,
                Localizacao = destino.Localizacao,
                Pais = destino.Pais,
                PrecoPorDia = destino.PrecoPorDia
            };
        }

        public async Task<bool> UpdateAsync(int id, DestinoDTO destinoDto)
        {
            var destino = await _repository.GetByIdAsync(id);
            if (destino == null) return false;

            destino.Localizacao = destinoDto.Localizacao;
            destino.Pais = destinoDto.Pais;
            destino.PrecoPorDia = destinoDto.PrecoPorDia;

            await _repository.UpdateAsync(destino);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var destino = await _repository.GetByIdAsync(id);
            if (destino == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}
