# Help Desk API

API REST desenvolvida em **C# e ASP.NET Core** para gerenciamento de usuários, chamados de suporte e comentários. O projeto tem como objetivo praticar desenvolvimento backend, arquitetura em camadas, autenticação e boas práticas do ecossistema .NET.

**Status:** Em desenvolvimento.

## Tecnologias

- C# e .NET 10
- ASP.NET Core Web API
- Entity Framework Core 10
- SQLite
- JWT Bearer para autenticação e autorização
- BCrypt.Net para hash e verificação de senhas
- Data Annotations para validação de DTOs

## Arquitetura

O projeto utiliza arquitetura em camadas, com separação de responsabilidades entre controllers, services, repositories e acesso aos dados.

```text
Controllers
    ↓
Services
    ↓
Repository Interfaces
    ↓
Repositories
    ↓
AppDbContext
    ↓
SQLite
```

### Estrutura do projeto

```text
helpDesk/
├── Controllers/
├── Data/
├── Dtos/
├── Exceptions/
├── Models/
├── Repositories/
├── Services/
└── Program.cs
```

- **Controllers:** recebem requisições HTTP e retornam respostas.
- **Services:** implementam regras de negócio e coordenam operações entre repositories.
- **Repositories:** abstraem o acesso aos dados utilizando Entity Framework Core.
- **DTOs:** definem os dados recebidos e enviados pela API.
- **Models:** representam as entidades e seus relacionamentos.
- **Exceptions:** tratam erros específicos da aplicação.
- **Data:** contém o `AppDbContext`, responsável pelo acesso ao banco de dados.

## Conceitos aplicados

- Injeção de Dependência e interfaces
- Repository Pattern
- Separação de responsabilidades
- Operações CRUD
- Relacionamentos entre entidades
- DTOs e validação de dados
- Autenticação e autorização com JWT
- Hash de senhas com BCrypt
- Entity Framework Core e migrations
- Desenvolvimento de APIs REST

## Requisitos

- .NET 10 SDK
- Ferramenta `dotnet-ef` compatível com EF Core 10

## Configuração e execução

A chave de assinatura JWT deve ser definida pela variável de ambiente `HELPDESK_JWT_KEY`. Utilize uma chave aleatória de pelo menos 32 bytes e não armazene segredos reais no código ou no repositório.

O banco de dados utiliza o arquivo `helpDesk/helpdesk.db`, ignorado pelo Git.

**Atenção:** a migration `ClearExistingData` apaga os usuários, chamados e comentários existentes quando aplicada. A reversão da migration não recupera esses dados.

## Autenticação

O login é realizado por `POST /api/accounts/login`. Após a autenticação, a API retorna um token JWT válido por **8 horas**.

Nas rotas protegidas, envie o token no cabeçalho:

```text
Authorization: Bearer <token>
```

O token contém o ID, o nome e o perfil do usuário.

| Valor JSON | Perfil |
|---|---|
| `0` | Customer |
| `1` | Admin |
| `2` | Agent |

## Rotas da API

| Método | Rota | Acesso | Descrição |
|---|---|---|---|
| POST | `/api/accounts/login` | Público | Autentica e retorna um JWT |
| POST | `/api/users` | Público | Cadastra usuário |
| GET | `/api/users` | Admin | Lista usuários |
| GET | `/api/users/{id}` | Admin | Busca usuário |
| PUT | `/api/users/{id}` | Admin | Atualiza usuário |
| DELETE | `/api/users/{id}` | Admin | Exclui usuário |
| GET | `/api/tickets` | Autenticado | Lista chamados |
| GET | `/api/tickets/{id}` | Autenticado | Busca chamado |
| POST | `/api/tickets` | Customer ou Admin | Cria chamado |
| PUT | `/api/tickets/{id}` | Autenticado | Atualiza chamado |
| DELETE | `/api/tickets/{id}` | Autenticado | Exclui chamado |
| POST | `/api/tickets/{id}/accept` | Agent ou Admin | Aceita e assume chamado aberto |
| POST | `/api/tickets/{id}/close` | Agent ou Admin | Fecha chamado em andamento |
| GET | `/api/comments` | Autenticado | Lista comentários |
| GET | `/api/comments/{id}` | Autenticado | Busca comentário |
| POST | `/api/comments` | Autenticado | Cria comentário |
| PUT | `/api/comments/{id}` | Autenticado | Atualiza comentário |
| DELETE | `/api/comments/{id}` | Autenticado | Exclui comentário |

As rotas de usuários, chamados e comentários retornam respostas no formato `{ "data": ..., "errors": [] }`, exceto quando não há corpo de resposta. O login retorna somente o token.

## Valores dos chamados

Os enums são enviados como números no JSON padrão do ASP.NET Core.

**Status:**

- `0` — Open
- `1` — InProgress
- `2` — Closed

**Prioridade:**

- `0` — Low
- `1` — Medium
- `2` — High
- `3` — Critical

## Exemplos de requisições

**Cadastro de usuário — `POST /api/users`**

```json
{
  "name": "Maria Silva",
  "email": "maria@example.com",
  "password": "senha-com-8-caracteres",
  "role": 0
}
```

**Login — `POST /api/accounts/login`**

```json
{
  "email": "maria@example.com",
  "password": "senha-com-8-caracteres"
}
```

**Criação de chamado — `POST /api/tickets`**

```json
{
  "title": "Não consigo acessar o sistema",
  "description": "O login retorna uma mensagem de erro.",
  "status": 0,
  "priority": 1,
  "customerId": 1
}
```

**Criação de comentário — `POST /api/comments`**

```json
{
  "content": "Estou analisando o problema.",
  "userId": 2,
  "ticketId": 1
}
```

## Objetivo

Consolidar conhecimentos em desenvolvimento backend com C# e .NET, evoluindo gradualmente a aplicação por meio da implementação de novas funcionalidades, padrões de projeto e boas práticas de desenvolvimento.
