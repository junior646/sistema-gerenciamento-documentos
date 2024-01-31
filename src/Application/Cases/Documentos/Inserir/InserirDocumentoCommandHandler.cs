using Domain;
using Domain.Entities;

namespace Application.Cases.Documentos.Inserir;
public class InserirDocumentoCommandHandler : IRequestHandler<InserirDocumentoCommand, Result<InserirDocumentoCommandResponse>>
{
    private readonly IDocumentoWriteRepository _documentoWriteRepository;
    private readonly IDocumentoWriteService _documentoWriteService;

    public InserirDocumentoCommandHandler(IDocumentoWriteRepository documentoWriteRepository, IDocumentoWriteService documentoWriteService)
    {
        _documentoWriteRepository = documentoWriteRepository;
        _documentoWriteService = documentoWriteService;
    }
    public async Task<Result<InserirDocumentoCommandResponse>> Handle(InserirDocumentoCommand request, CancellationToken cancellationToken)
    {
        var extensao = Path.GetExtension(request.Arquivo.FileName);
        var arquivo = Arquivo.Criar(request.Arquivo.FileName.Replace(extensao, ""),
                                    Path.GetExtension(request.Arquivo.FileName));

        _ = Enum.TryParse(request.Status, out Domain.Enum.Status statusConvertido);

        var documento = Documento.Criar(request.Descricao, statusConvertido, arquivo);

        var resultRepository = await _documentoWriteRepository.InserirDocumento(documento!, cancellationToken);
        if (resultRepository.Status != ResultStatus.Success) throw new Exception("Erro ao inserir documento");

        var resultService = await _documentoWriteService.SalvarArquivo(arquivo.Id, request.Arquivo, cancellationToken);
        if (resultService.Status != ResultStatus.Success) throw new Exception("Erro ao salvar documento");

        return new Result<InserirDocumentoCommandResponse>();
    }
}