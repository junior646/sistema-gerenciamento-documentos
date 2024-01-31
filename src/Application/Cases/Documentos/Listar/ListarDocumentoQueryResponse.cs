using Domain.Entities;

namespace Application.Cases.Documentos.Listar;
public record ListarDocumentoQueryResponse(IReadOnlyCollection<Documento> Documentos);