namespace Application.Cases.Documentos.Atualizar;
public class AtualizarDocumentoCommandValidator : AbstractValidator<AtualizarDocumentoCommand>
{
    public AtualizarDocumentoCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("O id do documento deve ser informado");
        RuleFor(x => x.Descricao).NotNull().WithMessage("O descrição do documento deve ser informado");
        RuleFor(x => x.Status)
            .NotNull().WithMessage("O status do documento deve ser informado")
            .Must(x => Enum.IsDefined(typeof(Domain.Enum.Status), x)).WithMessage("O status do documento deve ser informado");
    }
}