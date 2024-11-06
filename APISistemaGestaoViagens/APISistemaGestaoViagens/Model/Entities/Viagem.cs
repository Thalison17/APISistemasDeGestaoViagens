namespace APISistemaGestaoViagens.Model.Entities;

public class Viagem
{
    public int ViagemId { get; set; }
    public int ClienteId { get; set; }
    public int DestinoId { get; set; }
    public DateTime DataPartida { get; set; }
    public DateTime dataRetorno { get; set; }
    public decimal CustoTotal { get; set; }
    public string Status { get; set; }
    
    public Cliente Cliente { get; set; }
    public Destino Destino { get; set; }
}