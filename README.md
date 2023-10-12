# DDD-DOTNET-API

* Dotnet 7.0
* EF.Core
* PostgreSQL
* AutoMapper

API simples com um CRUD de usuários e autenticação JWT (tentando) seguir os padrões de DDD, tudo gira em torno da utilização de classes bases, sendo estas herdadas pelas classes específicas da aplicação.

## Comando para criar uma migration
dotnet ef --startup-project src/DDD.Web/DDD.Web.csproj --project src/DDD.Infra/DDD.Infra.csproj migrations add nomedamigration

## Comando para atualizar o banco de dados de acordo com a migration criada
dotnet ef --startup-project src/DDD.Web/DDD.Web.csproj --project src/DDD.Infra/DDD.Infra.csproj database update