using Microsoft.AspNetCore.Http;

namespace Application.Cases.Documentos.Inserir;
public class InserirDocumentoCommandValidator : AbstractValidator<InserirDocumentoCommand>
{
    public InserirDocumentoCommandValidator()
    {
        RuleFor(x => x.Descricao).NotNull().WithMessage("O descrição do documento deve ser informado");
        
        RuleFor(x => x.Status)
            .NotNull().WithMessage("O status do documento deve ser informado")
            .Must(x => Enum.IsDefined(typeof(Domain.Enum.Status), x)).WithMessage("O status do documento deve ser informado");

        RuleFor(x => x.Arquivo)
            .NotNull()
            .NotEmpty()
            .Must(x => ValidarArquivo(x, true)).WithMessage("O arquivo informado esta ausente ou em formato inválido. Apenas PDF é permitido");

    }
    private static bool ValidarArquivo(IFormFile formFile, bool validarContentTypePDF)
    {
        if (formFile == null || formFile?.Length < 1)
        {
            return false;
        }

        bool? contentValido = true;
        if (validarContentTypePDF)
            contentValido = formFile?.ContentType.Equals("application/pdf", StringComparison.InvariantCultureIgnoreCase);

        return contentValido.GetValueOrDefault();
    }
}