using Domain;

namespace Application.Cases.Documentos.Obter;
public record ObterDocumentoQuery(Guid Id) : IRequest<Result<ObterDocumentoQueryResponse>>;