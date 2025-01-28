using System.ComponentModel.DataAnnotations;

namespace APISistemaGestaoViagens.Model.DTOs;

public class ClienteUpdateDTO
{
    [Required]
    public string Nome { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [Phone]
    public string Telefone { get; set; }
    [Required]
    public string Cpf { get; set; }
}