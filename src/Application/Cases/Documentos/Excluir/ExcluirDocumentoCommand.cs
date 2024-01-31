using Domain;

namespace Application.Cases.Documentos.Excluir;
public record ExcluirDocumentoCommand(Guid Id) : IRequest<Result<ExcluirDocumentoCommandResponse>>;