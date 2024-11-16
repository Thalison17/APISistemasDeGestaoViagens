namespace APISistemaGestaoViagens.Model.Entities;

public class Viagem
{
    public int ViagemId { get; set; }
    public int DestinoId { get; set; }
    public DateTime DataPartida { get; set; }
    public DateTime DataRetorno { get; set; }
    public string Status { get; set; } = "Pendente";
    
    public Cliente Cliente { get; set; }
    public Destino Destino { get; set; }
}