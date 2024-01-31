using Domain;

namespace Application.Cases.Documentos.Excluir;
public class ExcluirDocumentoCommandHandler : IRequestHandler<ExcluirDocumentoCommand, Result<ExcluirDocumentoCommandResponse>>
{
    private readonly IDocumentoReadOnlyRepository _documentoReadOnlyRepository;
    private readonly IDocumentoWriteRepository _documentoWriteRepository;
    private readonly IDocumentoWriteService _documentoWriteService;

    public ExcluirDocumentoCommandHandler(IDocumentoReadOnlyRepository documentoReadOnlyRepository,
                                          IDocumentoWriteRepository documentoWriteRepository,
                                          IDocumentoWriteService documentoWriteService)
    {
        _documentoReadOnlyRepository = documentoReadOnlyRepository;
        _documentoWriteRepository = documentoWriteRepository;
        _documentoWriteService = documentoWriteService;
    }
    public async Task<Result<ExcluirDocumentoCommandResponse>> Handle(ExcluirDocumentoCommand request, CancellationToken cancellationToken)
    {
        var documentoReadOnlyResult = await _documentoReadOnlyRepository.ObterPorId(request.Id, cancellationToken);
        if (documentoReadOnlyResult.Status != ResultStatus.Success)
            return Result<ExcluirDocumentoCommandResponse>.EntityNotFound("Documento", request.Id, "Documento não encontrado");

        var documento = documentoReadOnlyResult.Data;

        var result = await _documentoWriteRepository.ExcluirDocumento(documento!, cancellationToken);
        if (result.Status != ResultStatus.Success) throw new Exception("Erro ao excluir documento");

        var resultService = await _documentoWriteService.ExcluirArquivo(documento!.Arquivo.Id, cancellationToken);
        if (resultService.Status != ResultStatus.Success) throw new Exception("Erro ao excluir documento");

        return new Result<ExcluirDocumentoCommandResponse>();
    }
}