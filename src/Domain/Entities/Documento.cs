using Domain.Enum;

namespace Domain.Entities;
public sealed class Documento
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public bool Ativo { get; set; }
    public DateTime Criacao { get; set; }
    public DateTime Atualizacao { get; set; }
    public Status Status { get; set; }
    public Arquivo Arquivo { get; set; } = new Arquivo();
    public static Documento Criar(string descricao, Status status, Arquivo arquivo) =>
         new()
         {
             Id = Guid.NewGuid(),
             Nome = "",
             Descricao = descricao,
             Ativo = true,
             Criacao = DateTime.Now,
             Atualizacao = DateTime.Now,
             Status = status,
             Arquivo = arquivo
         };
}