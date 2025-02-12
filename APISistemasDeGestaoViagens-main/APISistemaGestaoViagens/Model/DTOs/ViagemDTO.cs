namespace APISistemaGestaoViagens.Model.DTOs;

public class ViagemDTO
{
    public int ViagemId { get; set; }
    public int DestinoId { get; set; }
    public DateTime DataPartida { get; set; }
    public DateTime DataRetorno { get; set; }
    public string Status { get; set; } = "Pendente";
}
