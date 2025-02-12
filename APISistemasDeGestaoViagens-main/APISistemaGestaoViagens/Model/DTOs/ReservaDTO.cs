namespace APISistemaGestaoViagens.Model.DTOs;

public class ReservaDTO
{
    public int ReservaId { get; set; }
    public int ClienteId { get; set; }
    public int ViagemId { get; set; }
    public DateTime DataReserva { get; set; }
    public string StatusPagamento { get; set; } = "Pendente";
    public string MetodoPagamento { get; set; }
    public decimal CustoTotal { get; set; }
    public ViagemDTO Viagem { get; set; }
}