using Domain;
using Domain.Entities;
using Application.Cases.Documentos;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Documentos;
internal class DocumentoRepository : IDocumentoReadOnlyRepository, IDocumentoWriteRepository
{
    private readonly DocumentoDbContext _context;
    public DocumentoRepository(DocumentoDbContext context)
    {
        _context = context;
    }

    public async Task<Result> AtualizarDocumento(Documento documento, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Update(documento);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!(_context.Documento?.Any(e => e.Id == documento.Id)).GetValueOrDefault())
            {
                return Result.EntityAlreadyExists("Documento", documento.Id, "Documento não cadastrado");
            }
            else
            {
                throw;
            }
        }

        return Result.Success();
    }

    public async Task<Result> ExcluirDocumento(Documento documento, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Documento.FindAsync(documento.Id);

        if (entity != null)
        {
            _context.Documento.Remove(entity);
        }

        await _context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<Guid>> InserirDocumento(Documento documento, CancellationToken cancellationToken = default)
    {
        var entity = Documento.Criar(documento.Descricao, documento.Status, documento.Arquivo);
        _context.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(entity.Id);
    }

    public async Task<IReadOnlyCollection<Documento>> ListarDocumento(CancellationToken cancellationToken = default)
    {
        var documentos = await _context.Documento.ToListAsync(cancellationToken);
       
        return documentos;
    }

    public async Task<Result<Documento>> ObterPorId(Guid id, CancellationToken cancellationToken = default)
    {
        var documento = await _context.Documento.FirstOrDefaultAsync(m => m.Id == id);

        return Result<Documento>.Success(documento!);
    }
}