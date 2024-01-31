using Application.Cases.Documentos.Atualizar;
using Application.Cases.Documentos.Listar;
using Application.Cases.Documentos.Obter;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebSite.Controllers;

namespace UnitTest.WebSiteTest.Controllers;
internal class DocumentoControllerTest : ControllerTesting<DocumentoController>
{
    [Test]
    public void RetornadoListaDeDocumentosComSucesso()
    {
        var taskResponse = Task.FromResult(ListarDocumentoQueryResponseMock).Result;

        Sender.Setup(s => s.Send(It.IsAny<ListarDocumentoQuery>(), It.IsAny<CancellationToken>())).Returns(taskResponse);
        var responseController = Controller.Index(CancellationToken.None).Result;

        Assert.True(responseController is ViewResult);

        var result = (ViewResult)responseController;

        Assert.True(result.Model is IEnumerable<WebSite.Models.Documentos.Documento>);

        var response = (IEnumerable<WebSite.Models.Documentos.Documento>)result.Model!;

        foreach (var item in response)
        {
            item.Id.Should().NotBeEmpty();
            item.Descricao.Should().NotBeEmpty();
            item.Status.Should().NotBeEmpty();
            item.ListaStatus.Should().HaveCountGreaterThan(1);
            item.Criacao.Should().NotBe(DateTime.MinValue);
            item.Atualizacao.Should().NotBe(DateTime.MinValue);
            item.DadosArquivo.Should().NotBeNull();
        }
    }

    [Test]
    public void RetornadoUnicoDocumentoComSucesso()
    {
        var taskResponse = Task.FromResult(ObterDocumentoQueryResponseMock).Result;

        Sender.Setup(s => s.Send(It.IsAny<ObterDocumentoQuery>(), It.IsAny<CancellationToken>())).Returns(taskResponse);
        var responseController = Controller.Details(Guid.NewGuid(), CancellationToken.None).Result;

        Assert.True(responseController is ViewResult);

        var result = (ViewResult)responseController;

        Assert.True(result.Model is WebSite.Models.Documentos.Documento);

        var response = (WebSite.Models.Documentos.Documento)result.Model!;

        response.Id.Should().NotBeEmpty();
        response.Descricao.Should().NotBeEmpty();
        response.Status.Should().NotBeEmpty();
        response.ListaStatus.Should().HaveCountGreaterThan(1);
        response.Criacao.Should().NotBe(DateTime.MinValue);
        response.Atualizacao.Should().NotBe(DateTime.MinValue);
        response.DadosArquivo.Should().NotBeNull();
    }

    [Test]
    public void AtualizarDocumentoComSucesso()
    {
        var taskResponseDocumento = Task.FromResult(ObterDocumentoQueryResponseMock).Result;
        var taskResponseAtualizacao = Task.FromResult(AtualizarDocumentoCommandResponseMock).Result;
        var documento = MockDocumentosModel(1).Data!.First();

        Sender.Setup(s => s.Send(It.IsAny<ObterDocumentoQuery>(), It.IsAny<CancellationToken>())).Returns(taskResponseDocumento);
        Sender.Setup(s => s.Send(It.IsAny<AtualizarDocumentoCommand>(), It.IsAny<CancellationToken>())).Returns(taskResponseAtualizacao);
        var responseController = Controller.Edit(documento.Id, documento, CancellationToken.None).Result;

        Assert.True(responseController is RedirectToActionResult);

        var response = (RedirectToActionResult)responseController;

        response.ActionName.Should().Be("Index");
    }

    [Test]
    public void AtualizarDocumentoComErroId()
    {
        var taskResponseDocumento = Task.FromResult(ObterDocumentoQueryResponseMock).Result;
        var taskResponseAtualizacao = Task.FromResult(AtualizarDocumentoCommandResponseMock).Result;
        var documento = MockDocumentosModel(1).Data!.First();

        Sender.Setup(s => s.Send(It.IsAny<ObterDocumentoQuery>(), It.IsAny<CancellationToken>())).Returns(taskResponseDocumento);
        Sender.Setup(s => s.Send(It.IsAny<AtualizarDocumentoCommand>(), It.IsAny<CancellationToken>())).Returns(taskResponseAtualizacao);
        var responseController = Controller.Edit(Guid.NewGuid(), documento, CancellationToken.None).Result;

        Assert.True(responseController is BadRequestResult);
    }

    private async Task<Domain.Result<ListarDocumentoQueryResponse>> ListarDocumentoQueryResponseMock() =>
        await Task.FromResult(new ListarDocumentoQueryResponse(MockDocumentosEntity(5).Data!));
    private async Task<Domain.Result<ObterDocumentoQueryResponse>> ObterDocumentoQueryResponseMock() =>
        await Task.FromResult(new ObterDocumentoQueryResponse(MockDocumentosEntity(1).Data!.First()));
    private async Task<Domain.Result<AtualizarDocumentoCommandResponse>> AtualizarDocumentoCommandResponseMock() =>
        await Task.FromResult(new AtualizarDocumentoCommandResponse());

    private Domain.Result<IReadOnlyCollection<WebSite.Models.Documentos.Documento>> MockDocumentosModel(int quantidade)
    {
        var fakerArquivo = new Faker<WebSite.Models.Documentos.DadosArquivo>()
             .RuleFor(p => p.Id, f => f.Random.Guid())
             .RuleFor(p => p.Nome, f => f.Random.String())
             .RuleFor(p => p.Extensao, f => f.Random.String())
         ;

        var fakerDocumento = new Faker<WebSite.Models.Documentos.Documento>()
            .RuleFor(p => p.Id, f => f.Random.Guid())
            .RuleFor(p => p.Descricao, f => f.Random.String())
            .RuleFor(p => p.Status, f => f.PickRandom(new string[] { "Aprovado", "Pendente", "Reprovado" }))
            .RuleFor(p => p.Ativo, f => true)
            .RuleFor(p => p.Criacao, f => new DateTime(f.Random.UInt()))
            .RuleFor(p => p.Atualizacao, f => new DateTime(f.Random.UInt()))
            .RuleFor(p => p.Arquivo, f => UnitTest.SeedTesting.MockIFormFile())
            .RuleFor(p => p.DadosArquivo, f => fakerArquivo.Generate())
        ;

        return fakerDocumento.Generate(quantidade);
    }

    private static Domain.Result<IReadOnlyCollection<Domain.Entities.Documento>> MockDocumentosEntity(int quantidade)
    {
        var fakerArquivo = new Faker<Domain.Entities.Arquivo>()
             .RuleFor(p => p.Id, f => f.Random.Guid())
             .RuleFor(p => p.Nome, f => f.Random.String())
             .RuleFor(p => p.Extensao, f => f.Random.String())
         ;

        var fakerDocumento = new Faker<Domain.Entities.Documento>()
            .RuleFor(p => p.Id, f => f.Random.Guid())
            .RuleFor(p => p.Descricao, f => f.Random.String())
            .RuleFor(p => p.Status, f => f.PickRandom(new Domain.Enum.Status[]
            {
                    Domain.Enum.Status.Aprovado, Domain.Enum.Status.Pendente, Domain.Enum.Status.Reprovado
            }))
            .RuleFor(p => p.Ativo, f => true)
            .RuleFor(p => p.Criacao, f => new DateTime(f.Random.UInt()))
            .RuleFor(p => p.Atualizacao, f => new DateTime(f.Random.UInt()))
            .RuleFor(p => p.Arquivo, f => fakerArquivo.Generate())
        ;

        return fakerDocumento.Generate(quantidade);
    }

}