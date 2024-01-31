using Domain;

namespace Application.Cases.Documentos.Listar;
public class ListarDocumentoQueryHandler : IRequestHandler<ListarDocumentoQuery, Result<ListarDocumentoQueryResponse>>
{
    private readonly IDocumentoReadOnlyRepository _documentoReadOnlyRepository;

    public ListarDocumentoQueryHandler(IDocumentoReadOnlyRepository documentoReadOnlyRepository)
    {
        _documentoReadOnlyRepository = documentoReadOnlyRepository;
    }
    public async Task<Result<ListarDocumentoQueryResponse>> Handle(ListarDocumentoQuery request, CancellationToken cancellationToken)
    {
        var documentos = await _documentoReadOnlyRepository.ListarDocumento(cancellationToken);

        return new ListarDocumentoQueryResponse(documentos);
    }
}