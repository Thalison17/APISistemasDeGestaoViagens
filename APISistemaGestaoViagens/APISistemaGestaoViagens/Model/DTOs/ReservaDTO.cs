namespace APISistemaGestaoViagens.Model.DTOs;

public class ReservaDTO
{
    public int ClienteId { get; set; }
    public int ViagemId { get; set; }
    public DateTime DataReserva { get; set; }
    public string StatusPagamento { get; set; }
}