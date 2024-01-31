using Domain;

namespace Application.Cases.Documentos.Listar;
public record ListarDocumentoQuery : IRequest<Result<ListarDocumentoQueryResponse>>;