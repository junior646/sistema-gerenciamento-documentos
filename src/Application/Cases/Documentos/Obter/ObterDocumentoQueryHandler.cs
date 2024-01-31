using Domain;

namespace Application.Cases.Documentos.Obter;
public class ObterDocumentoQueryHandler : IRequestHandler<ObterDocumentoQuery, Result<ObterDocumentoQueryResponse>>
{
    private readonly IDocumentoReadOnlyRepository _documentoReadOnlyRepository;

    public ObterDocumentoQueryHandler(IDocumentoReadOnlyRepository documentoReadOnlyRepository)
    {
        _documentoReadOnlyRepository = documentoReadOnlyRepository;
    }
    public async Task<Result<ObterDocumentoQueryResponse>> Handle(ObterDocumentoQuery request, CancellationToken cancellationToken)
    {
        var documentos = await _documentoReadOnlyRepository.ObterPorId(request.Id, cancellationToken);
        if (documentos.Status != ResultStatus.Success)
            return Result<ObterDocumentoQueryResponse>.EntityNotFound("Documento", request.Id, "Documento não encontrado");

        return new ObterDocumentoQueryResponse(documentos.Data!);
    }
}