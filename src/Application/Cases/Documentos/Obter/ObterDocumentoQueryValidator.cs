namespace Application.Cases.Documentos.Obter;
public class ObterDocumentoQueryValidator : AbstractValidator<ObterDocumentoQuery>
{
    public ObterDocumentoQueryValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("O id do Documento deve ser informado");
    }
}