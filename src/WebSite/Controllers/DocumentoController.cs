using Application.Cases.Documentos.Atualizar;
using Application.Cases.Documentos.Excluir;
using Application.Cases.Documentos.Inserir;
using Application.Cases.Documentos.Listar;
using Application.Cases.Documentos.Obter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebSite.Models.Documentos;

namespace WebSite.Controllers
{
    public class DocumentoController : BaseController
    {
        public DocumentoController(ISender sender) : base(sender) { }

        // GET: Documento
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new ListarDocumentoQuery(), cancellationToken);
            IEnumerable<Documento> documentos = new List<Documento>();

            if (result.Status == Domain.ResultStatus.Success)
                documentos = result.Data!.Documentos.Select(ConverterDocumentoEntidadeEmDocumentoModel);

            return View(documentos);
        }

        // GET: Documento/Details/5
        public async Task<IActionResult> Details(Guid? id, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new ObterDocumentoQuery(id.GetValueOrDefault()), cancellationToken);

            if (result.Status != Domain.ResultStatus.Success || result.Data!.Documento == null)
            {
                return NotFound();
            }

            var documento = ConverterDocumentoEntidadeEmDocumentoModel(result.Data.Documento);

            return View(documento);
        }

        // GET: Documento/Create
        public IActionResult Create() => View(Documento.Criar());

        // POST: Documento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descricao,Status,Arquivo")] Documento documento, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _sender.Send(new InserirDocumentoCommand(documento.Descricao, documento.Status, documento.Arquivo), cancellationToken);

                    if (result.Status != Domain.ResultStatus.Success)
                    {
                        return NotFound();
                    }
                }
                catch (Application.Common.Exceptions.ValidationException failures)
                {
                    failures.Errors.ToList().ForEach(erro => ModelState.TryAddModelError(erro.Key, erro.Value.LastOrDefault()!.ToString()!));
                    return View(documento);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(documento);
        }

        // GET: Documento/Edit/5
        public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new ObterDocumentoQuery(id), cancellationToken);

            if (result.Status != Domain.ResultStatus.Success || result.Data!.Documento == null)
            {
                return NotFound();
            }

            var documento = ConverterDocumentoEntidadeEmDocumentoModel(result.Data.Documento);

            return View(documento);
        }

        // POST: Documento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Descricao,Status")] Documento documento, CancellationToken cancellationToken)
        {
            if (id != documento.Id)
            {
                return BadRequest();
            }

            ModelState.Remove(nameof(documento.Arquivo));

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _sender.Send(new AtualizarDocumentoCommand(documento.Id, documento.Descricao, documento.Status), cancellationToken);

                    if (result.Status != Domain.ResultStatus.Success)
                    {
                        return NotFound();
                    }
                }
                catch (Application.Common.Exceptions.ValidationException failures)
                {
                    failures.Errors.ToList().ForEach(erro => ModelState.TryAddModelError(erro.Key, erro.Value.LastOrDefault()!.ToString()!));
                    return View(documento);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(documento);
        }

        // GET: Documento/Delete/5
        public async Task<IActionResult> Delete(Guid? id, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new ObterDocumentoQuery(id.GetValueOrDefault()), cancellationToken);

            if (result.Status != Domain.ResultStatus.Success || result.Data!.Documento == null)
            {
                return NotFound();
            }

            var documento = ConverterDocumentoEntidadeEmDocumentoModel(result.Data.Documento);

            return View(documento);
        }

        // POST: Documento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new ExcluirDocumentoCommand(id), cancellationToken);

            if (result.Status != Domain.ResultStatus.Success)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        private static Documento ConverterDocumentoEntidadeEmDocumentoModel(Domain.Entities.Documento entidadeDocumento)
        {
            var entityDocumento = entidadeDocumento;

            var arquivo = new DadosArquivo()
            {
                Id = entityDocumento.Arquivo.Id,
                Nome = entityDocumento.Arquivo.Nome,
                Extensao = entityDocumento.Arquivo.Extensao
            };
            var documento = new Documento()
            {
                Id = entityDocumento.Id,
                Descricao = entityDocumento.Descricao,
                Atualizacao = entityDocumento.Atualizacao,
                Criacao = entityDocumento.Criacao,
                Status = entityDocumento.Status.ToString(),
                Ativo = entityDocumento.Ativo,
                DadosArquivo = arquivo
            };
            return documento;
        }
    }
}