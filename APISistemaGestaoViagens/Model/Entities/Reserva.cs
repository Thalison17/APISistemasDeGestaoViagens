namespace APISistemaGestaoViagens.Model.Entities;

public class Reserva
{
    public int ReservaId { get; set; }
    public int ClienteId { get; set; }
    public int ViagemId { get; set; }
    public DateTime DataReserva { get; set; }
    public string StatusPagamento { get; set; } = "Pendente";
    public string MetodoPagamento { get; set; } 
    public decimal CustoTotal { get; set; }
    public Cliente Cliente { get; set; }
    public Viagem Viagem { get; set; }
    public int DestinoId { get; set; }
    public int DuracaoDias { get; set; }
}
