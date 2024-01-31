using Domain;

namespace Application.Cases.Documentos.Atualizar;
public class AtualizarDocumentoCommandHandler : IRequestHandler<AtualizarDocumentoCommand, Result<AtualizarDocumentoCommandResponse>>
{
    private readonly IDocumentoReadOnlyRepository _documentoReadOnlyRepository;
    private readonly IDocumentoWriteRepository _documentoWriteRepository;

    public AtualizarDocumentoCommandHandler(IDocumentoReadOnlyRepository documentoReadOnlyRepository, IDocumentoWriteRepository documentoWriteRepository)
    {
        _documentoReadOnlyRepository = documentoReadOnlyRepository;
        _documentoWriteRepository = documentoWriteRepository;
    }
    public async Task<Result<AtualizarDocumentoCommandResponse>> Handle(AtualizarDocumentoCommand request, CancellationToken cancellationToken)
    {
        var documentoReadOnlyResult = await _documentoReadOnlyRepository.ObterPorId(request.Id, cancellationToken);

        if (documentoReadOnlyResult.Status != ResultStatus.Success || documentoReadOnlyResult.Data == null)
            return Result<AtualizarDocumentoCommandResponse>.EntityNotFound("Documento", request.Id, "Documento não encontrado");

        _ = Enum.TryParse(request.Status, out Domain.Enum.Status statusConvertido);

        Domain.Entities.Documento documento = documentoReadOnlyResult.Data;
        documento.Atualizacao = DateTime.Now;
        documento.Status = statusConvertido;
        documento.Descricao = request.Descricao;

        var result = await _documentoWriteRepository.AtualizarDocumento(documento!, cancellationToken);
        if (result.Status != ResultStatus.Success) throw new Exception("Erro ao atualizar documento");

        return new Result<AtualizarDocumentoCommandResponse>();
    }
}