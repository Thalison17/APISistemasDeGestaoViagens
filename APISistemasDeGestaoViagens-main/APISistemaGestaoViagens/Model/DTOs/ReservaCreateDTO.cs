namespace APISistemaGestaoViagens.Model.DTOs;

public class ReservaCreateDTO
{
    public int ClienteId { get; set; }
    public DateTime DataReserva { get; set; }
    public string StatusPagamento { get; set; }
    public string MetodoPagamento { get; set; }
    public ViagemDTO Viagem { get; set; }
}