Projeto: Sistema de Gerenciamento de Documentos
Tecnologias: C#, MVC Core 7.0, Docker, NUnit, EF Core, SQL Server Memory Data Base
Template: Clean Archtecture

Objetivo: Sistema Web com gerenciamento de documentos, aonde podem ser inputados documentos e gerenciados.
O documentos em questão trata-se de um arquivo qualquer, por exemplo, PDF, que será salvo em diretório, mas pode ser refatorado para o uso de serviços de Storage, a exemplo do Amazon S3, One Drive, SharePoint, Google Drive e afins.

O template Clean Archtecture, visa restringir seus artefatos de acordo com suas responsabilidades, por camadas, aonde a DOMAIN é a camada representativa pela regras de negócio do projeto, a Application as regras da aplicação, Infrastructure por todo o contexto de infraestrutura, WebSite pela parte FrontEnd, ou camada de interação inicial:
WebSite, Application, Domain e Infrastructure

Também esta disponível o uso de Migrations, para facilitar a configuração e execução do projeto.

Execute a seguinte linha no Console de Nugget ou Terminal para a configuração do banco de dados (pode ser necessário o ajuste na connection string dentro do arquivo appsettings.json):

Visual Studio:
#update-database

CLI
#dotnet ef database update

Link referência: https://learn.microsoft.com/pt-br/aspnet/core/tutorials/first-mvc-app/adding-model?view=aspnetcore-7.0&tabs=visual-studio-code

O projeto pode ser executado em um dos profiles pré-configurados: http, https, IIS Express, Docker ou WSL, utilize aquele que se sentir mais confortável.

Outro recurso disponível são os testes de unidade com as tecnologias: NUnit, MOQ, AutoBogus.

Pode ser executado para facilitar o entendimento de estruturação do projeto.
