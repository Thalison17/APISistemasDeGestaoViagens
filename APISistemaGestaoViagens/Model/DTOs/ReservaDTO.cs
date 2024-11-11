namespace APISistemaGestaoViagens.Model.DTOs;

public class ReservaDTO
{
    public int ReservaId { get; set; }
    public int ClienteId { get; set; }
    public DateTime DataReserva { get; set; }
    public string StatusPagamento { get; set; }
    public string MetodoPagamento { get; set; }
}