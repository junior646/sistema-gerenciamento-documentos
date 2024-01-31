using Domain;

namespace Application.Cases.Documentos.Atualizar;
public record AtualizarDocumentoCommand(Guid Id,
                                        string Descricao,
                                        string Status) : IRequest<Result<AtualizarDocumentoCommandResponse>>;