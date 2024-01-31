using Domain;
using Domain.Entities;

namespace Application.Cases.Documentos
{
    public interface IDocumentoReadOnlyRepository
    {
        Task<IReadOnlyCollection<Documento>> ListarDocumento(CancellationToken cancellationToken = default);
        Task<Result<Documento>> ObterPorId(Guid id, CancellationToken cancellationToken = default);
    }
}