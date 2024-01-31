using Domain;
using Microsoft.AspNetCore.Http;

namespace Application.Cases.Documentos.Inserir;
public record InserirDocumentoCommand(string Descricao,
                                      string Status,
                                      IFormFile Arquivo) : IRequest<Result<InserirDocumentoCommandResponse>>;