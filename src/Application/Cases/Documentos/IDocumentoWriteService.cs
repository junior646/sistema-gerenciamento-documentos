using Domain;
using Microsoft.AspNetCore.Http;

namespace Application.Cases.Documentos;

public interface IDocumentoWriteService
{
    Task<Result> ExcluirArquivo(Guid id, CancellationToken cancellationToken = default);
    Task<Result<Guid>> SalvarArquivo(Guid id, IFormFile file, CancellationToken cancellationToken = default);
}