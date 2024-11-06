namespace APISistemaGestaoViagens.Model.Entities;

public class Cliente
{
    public int ClienteId { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Cpf { get; set; }
    // public Endereco Endereco { get; set; }
    public ICollection<Reserva> Reservas { get; set; }
}