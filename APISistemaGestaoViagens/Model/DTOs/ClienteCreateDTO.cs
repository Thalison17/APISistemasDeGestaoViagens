namespace APISistemaGestaoViagens.Model.DTOs;

public class ClienteCreateDTO
{
    public int ClienteId { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Cpf { get; set; }
}