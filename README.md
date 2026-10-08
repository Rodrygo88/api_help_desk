## Status

Em desenvolvimento.

O projeto continuará sendo evoluído conforme novos conceitos e funcionalidades forem estudados e implementados.


# HelpDesk API

API REST de um sistema de Help Desk desenvolvida em **C# e ASP.NET Core**.

O projeto está sendo desenvolvido como forma de prática em desenvolvimento backend, com foco em organização de código, separação de responsabilidades, persistência de dados e aplicação de padrões de arquitetura utilizados no ecossistema .NET.

## Tecnologias

* C#
* .NET
* ASP.NET Core
* Entity Framework Core
* PostgreSQL (Inicialmente com SQLite)
* REST API

## Arquitetura e conceitos

O projeto utiliza uma arquitetura em camadas, separando as responsabilidades entre Controllers, Services, Repositories e a camada de acesso aos dados.

```text
Controller
    ↓
Service
    ↓
Interface do Repository
    ↓
Repository
    ↓
DbContext
    ↓
Banco de Dados
```

Durante o desenvolvimento, estão sendo aplicados conceitos como:

* Injeção de Dependência
* Interfaces
* Repository Pattern
* Entity Framework Core
* DTOs (Data Transfer Objects)
* Exceptions personalizadas
* Operações CRUD
* Relacionamentos entre entidades
* Separação de responsabilidades
* Desenvolvimento de APIs REST

## Estrutura do projeto

```text
helpDesk/
│
├── Controllers/
├── Data/
├── Dtos/
├── Exceptions/
├── Models/
├── Repositories/
├── Services/
└── Program.cs
```

### Controllers

Responsáveis por receber as requisições HTTP e retornar as respostas da API.

### Services

Responsáveis pela lógica da aplicação e pelas regras de negócio. Também coordenam operações que envolvem diferentes repositories.

### Repositories

Responsáveis pelo acesso aos dados e pela comunicação com o banco de dados através do Entity Framework Core.

Os repositories são acessados através de interfaces, permitindo que a camada de Service dependa de abstrações em vez de implementações concretas.

### DTOs

Utilizados para definir os dados recebidos e enviados pela API, evitando a necessidade de expor diretamente as entidades da aplicação.

### Models

Representam as entidades utilizadas pela aplicação e seus relacionamentos.

### Exceptions

Contém exceptions personalizadas utilizadas para representar erros específicos da aplicação, como recursos não encontrados.

### Data

Contém o `AppDbContext`, responsável pelo gerenciamento da comunicação entre a aplicação e o banco de dados através do Entity Framework Core.

## Injeção de Dependência

Os repositories e services são registrados utilizando o sistema de Injeção de Dependência nativo do ASP.NET Core.

Exemplo:

```csharp
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
```

Dessa forma, os services podem depender das interfaces dos repositories em vez de depender diretamente de suas implementações.

## Fluxo de uma requisição

Por exemplo, uma requisição para criação de um comentário segue aproximadamente o seguinte fluxo:

```text
Requisição HTTP
       ↓
CommentsController
       ↓
CommentsServices
       ↓
ICommentRepository
       ↓
CommentRepository
       ↓
AppDbContext
       ↓
PostgreSQL
```

Durante a criação de um comentário, o `CommentsServices` também pode utilizar outros repositories para validar entidades relacionadas:

```text
CommentsServices
    ├── ICommentRepository
    ├── IUserRepository
    └── ITicketRepository
```

Dessa forma, o service pode verificar, por exemplo, se o usuário e o chamado associados ao comentário existem antes de realizar a criação.

## Funcionalidades atuais

* Gerenciamento de usuários
* Gerenciamento de chamados
* Gerenciamento de comentários
* Operações CRUD
* Relacionamento entre entidades
* Utilização de DTOs
* Exceptions personalizadas
* Repository Pattern
* Injeção de Dependência
* Persistência de dados com Entity Framework Core

## Objetivo do projeto

O projeto tem como objetivo consolidar conhecimentos em desenvolvimento backend com **C# e .NET**, aplicando na prática conceitos de APIs REST, Entity Framework Core, arquitetura em camadas, padrões de projeto e boas práticas de desenvolvimento.

## Status

Em desenvolvimento.

O projeto continuará sendo evoluído conforme novos conceitos e funcionalidades forem estudados e implementados.
