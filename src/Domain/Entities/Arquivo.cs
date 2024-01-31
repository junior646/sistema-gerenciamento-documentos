namespace Domain.Entities;

public sealed class Arquivo
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Extensao { get; set; } = string.Empty;
    public bool Ativo { get; set; }
    public static Arquivo Criar(string nome, string extensao) =>
        new() { Id = Guid.NewGuid(), Nome = nome, Extensao = extensao, Ativo = true };
}