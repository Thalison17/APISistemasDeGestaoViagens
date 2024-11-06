namespace APISistemaGestaoViagens.Model.DTOs;

public class ViagemDTO
{
    public int ClienteId { get; set; }
    public int DestinoId { get; set; }
    public DateTime DataPartida { get; set; }
    public DateTime DataRetorno { get; set; }
    public decimal CustoTotal { get; set; }
}