using Application.Cases.Documentos;
using Infrastructure.Repositories.Documentos;
using Infrastructure.Services.Documentos;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IDocumentoReadOnlyRepository, DocumentoRepository>();
        services.AddScoped<IDocumentoWriteRepository, DocumentoRepository>();
        services.AddScoped<IDocumentoWriteService, DocumentoService>();

        return services;
    }
}