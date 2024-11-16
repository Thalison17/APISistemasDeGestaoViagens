using APISistemaGestaoViagens.Model.Entities;
using System.Threading.Tasks;

namespace APISistemaGestaoViagens.Repository.Interfaces
{
    public interface IDestinoRepository
    {
        Task<Destino?> GetByIdAsync(int id);
    }
}