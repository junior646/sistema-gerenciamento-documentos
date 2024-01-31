using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebSite.Models.Documentos;
public class Documento
{
    public Documento() => ListaStatus = new List<SelectListItem>()
        {
            new SelectListItem { Value = "Aprovado", Text = "Aprovado" },
            new SelectListItem { Value = "Pendente", Text = "Pendente" },
            new SelectListItem { Value = "Reprovado", Text = "Reprovado" }
        };
    public Guid Id { get; set; }
    [MaxLength(250)]
    public required string Descricao { get; set; } = string.Empty;
    public required string Status { get; set; } = string.Empty;
    public List<SelectListItem> ListaStatus { get; }
    public bool Ativo { get; set; }
    public DateTime Criacao { get; set; }
    public DateTime Atualizacao { get; set; }
    public DadosArquivo DadosArquivo { get; set; } = new DadosArquivo();
    [Display(Name = "File")]
    public IFormFile Arquivo { get; set; }

    public static Documento Criar() =>
        new() { Ativo = true, Descricao = string.Empty, Status = Domain.Enum.Status.Pendente.ToString() };
}

public class DadosArquivo
{
    public Guid Id { get; set; }
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;
    [MaxLength(10)]
    public string Extensao { get; set; } = string.Empty;
}