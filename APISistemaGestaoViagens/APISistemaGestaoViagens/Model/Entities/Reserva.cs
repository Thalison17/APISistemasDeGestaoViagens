namespace APISistemaGestaoViagens.Model.Entities;

public class Reserva
{
    public int ReservaId { get; set; }
    public int ClienteId { get; set; }
    public int ViagensId { get; set; }
    public DateTime DataReserva { get; set; }
    public string StatusPagamento { get; set; }
    public string MetodoPagamento { get; set; }
    
    public Cliente Cliente { get; set; }
    public Viagem Viagem { get; set; }
}