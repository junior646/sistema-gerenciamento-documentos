namespace Application.Cases.Documentos.Excluir;
public class ExcluirDocumentoQueryValidator : AbstractValidator<ExcluirDocumentoCommand>
{
    public ExcluirDocumentoQueryValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("O id do documento deve ser informado");
    }
}