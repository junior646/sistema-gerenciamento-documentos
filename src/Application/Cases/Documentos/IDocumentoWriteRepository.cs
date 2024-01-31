using Domain;
using Domain.Entities;

namespace Application.Cases.Documentos;
public interface IDocumentoWriteRepository
{
    Task<Result> AtualizarDocumento(Documento documento, CancellationToken cancellationToken = default);
    Task<Result<Guid>> InserirDocumento(Documento documento, CancellationToken cancellationToken = default);
    Task<Result> ExcluirDocumento(Documento documento, CancellationToken cancellationToken = default);
}