namespace APISistemaGestaoViagens.Model.DTOs;

public class DestinoDTO
{
    public int DestinoId { get; set; }
    public string Localizacao { get; set; }
    public string Pais { get; set; }
    public decimal PrecoPorDia { get; set; }
}