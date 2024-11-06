namespace APISistemaGestaoViagens.Model.Entities;

public class Pacote
{
    public int PacoteId { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public bool IncluiTransporte { get; set; }
    public bool IncluiHospedagem { get; set; }
}