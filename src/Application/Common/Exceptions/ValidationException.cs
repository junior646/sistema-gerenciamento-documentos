using FluentValidation.Results;

namespace Application.Common.Exceptions;

public class ValidationException : Exception
{
    public ValidationException()
        : base("Falha em uma ou mais validações.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(string propriedade, string erro)
        : base("Falha em uma ou mais validações.")
    {
        Errors = new Dictionary<string, string[]>
        {
            { propriedade, new[] { erro } }
        };
    }

    public ValidationException(string propriedade, string[] erros)
        : base("Falha em uma ou mais validações.")
    {
        Errors = new Dictionary<string, string[]>
        {
            { propriedade, erros }
        };
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }
}