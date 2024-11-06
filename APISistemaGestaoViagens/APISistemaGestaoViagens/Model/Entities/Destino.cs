namespace APISistemaGestaoViagens.Model.Entities;

public class Destino
{
    public int DestinoId { get; set; }
    public string Localizacao { get; set; }
    public string Pais { get; set; }
    public string Descricao { get; set; }
    public decimal precoPorDia { get; set; }
}