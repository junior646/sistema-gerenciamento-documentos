namespace WebSite.Models.Documentos;

public class AtualizarDocumentoModel
{
    public required string Id { get; set; }
    public required string Descricao { get; set; } = string.Empty;
    public required int Status { get; set; }
}
