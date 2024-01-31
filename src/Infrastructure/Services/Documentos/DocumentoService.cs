using Domain;
using Microsoft.AspNetCore.Http;
using Application.Cases.Documentos;
using Microsoft.Extensions.Options;
using Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using System.Runtime;

namespace Infrastructure.Services.Documentos
{
    internal class DocumentoService : IDocumentoWriteService
    {
        private readonly ConfiguracaoArmazenagemDocumento _configuracaoArmazenagemDocumento;

        public DocumentoService(/*IOptions<ConfiguracaoArmazenagemDocumento> configuracaoArmazenagemDocumento*/)
        {
            _configuracaoArmazenagemDocumento = new() { DiretorioArmazenagemDocumentos = "C:\\Temp" };
            //_configuracaoArmazenagemDocumento = configuracaoArmazenagemDocumento.Value;
        }

        public Task<Result> ExcluirArquivo(Guid id, CancellationToken cancellationToken = default)
        {
            var diretorio = _configuracaoArmazenagemDocumento.DiretorioArmazenagemDocumentos;

            var nomeArquivo = Path.Combine(diretorio, id.ToString());

            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            File.Delete(nomeArquivo);

            return Task.FromResult(Result.Success());
        }

        public async Task<Result<Guid>> SalvarArquivo(Guid id, IFormFile file, CancellationToken cancellationToken = default)
        {
            var diretorio = _configuracaoArmazenagemDocumento.DiretorioArmazenagemDocumentos;

            var nomeArquivo = Path.Combine(diretorio, id.ToString());

            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            using (Stream fileStream = new FileStream(nomeArquivo, FileMode.Create, FileAccess.ReadWrite))
            {
                await file.CopyToAsync(fileStream, cancellationToken);
            }

            return Result<Guid>.Success(id);
        }
    }
}