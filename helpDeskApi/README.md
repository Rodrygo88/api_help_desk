# Help Desk API

API REST para gerenciamento de usuarios, chamados de suporte e comentarios.

## Tecnologias

- C# e .NET 10
- ASP.NET Core Web API para controllers e rotas HTTP
- Entity Framework Core 10 para persistencia e migrations
- SQLite como banco de dados
- JWT Bearer para autenticacao e autorizacao por perfil
- BCrypt.Net para hash e verificacao de senhas
- Data Annotations para validacao dos DTOs

## Requisitos

- .NET 10 SDK
- Ferramenta `dotnet-ef` compativel com EF Core 10 para executar migrations

## Configuracao e execucao

A chave de assinatura JWT deve ser fornecida pela variavel de ambiente `HELPDESK_JWT_KEY`. Gere uma chave aleatoria de 32 bytes ou mais; nao coloque uma chave real no codigo ou no repositorio.

No PowerShell, na raiz do repositorio:

```powershell
$bytes = New-Object byte[] 32
$rng = [Security.Cryptography.RandomNumberGenerator]::Create()
$rng.GetBytes($bytes)
$env:HELPDESK_JWT_KEY = [Convert]::ToBase64String($bytes)
$rng.Dispose()
dotnet ef database update --project helpDesk
dotnet run --project helpDesk
```

A API usa o arquivo SQLite `helpDesk/helpdesk.db`. Esse arquivo e ignorado pelo Git. A migration `ClearExistingData` apaga usuarios, chamados e comentarios existentes quando aplicada; ela nao consegue restaurar esses dados ao ser revertida.

## Autenticacao

O login e feito em `POST /api/accounts/login`. Envie email e senha; em caso de sucesso, a resposta contem o token JWT como string. Envie o token nas rotas protegidas usando o header:

```text
Authorization: Bearer <token>
```

O token expira em 8 horas e inclui o ID, o nome e o perfil do usuario. Os perfis disponiveis sao:

| Valor JSON | Perfil |
| --- | --- |
| `0` | Customer |
| `1` | Admin |
| `2` | Agent |

## Rotas

As rotas de usuarios, chamados e comentarios retornam dados no formato `{ "data": ..., "errors": [] }`, exceto respostas sem corpo de dados. O login retorna somente o token.

| Metodo | Rota | Acesso | Descricao |
| --- | --- | --- | --- |
| `POST` | `/api/accounts/login` | Publico | Autentica e retorna um JWT |
| `POST` | `/api/users` | Publico | Cadastra usuario |
| `GET` | `/api/users` | Admin | Lista usuarios |
| `GET` | `/api/users/{id}` | Admin | Busca usuario |
| `PUT` | `/api/users/{id}` | Admin | Atualiza usuario |
| `DELETE` | `/api/users/{id}` | Admin | Exclui usuario |
| `GET` | `/api/tickets` | Autenticado | Lista chamados |
| `GET` | `/api/tickets/{id}` | Autenticado | Busca chamado |
| `POST` | `/api/tickets` | Customer ou Admin | Cria chamado |
| `PUT` | `/api/tickets/{id}` | Autenticado | Atualiza chamado |
| `DELETE` | `/api/tickets/{id}` | Autenticado | Exclui chamado |
| `POST` | `/api/tickets/{id}/accept` | Agent ou Admin | Aceita chamado aberto e atribui ao usuario autenticado |
| `POST` | `/api/tickets/{id}/close` | Agent ou Admin | Fecha chamado em andamento; agente deve estar atribuido ao chamado |
| `GET` | `/api/comments` | Autenticado | Lista comentarios |
| `GET` | `/api/comments/{id}` | Autenticado | Busca comentario |
| `POST` | `/api/comments` | Autenticado | Cria comentario |
| `PUT` | `/api/comments/{id}` | Autenticado | Atualiza comentario |
| `DELETE` | `/api/comments/{id}` | Autenticado | Exclui comentario |

## Valores de chamados

Os enums sao enviados como numeros pelo JSON padrao do ASP.NET Core.

**Status:** `0` Open, `1` InProgress, `2` Closed.

**Prioridade:** `0` Low, `1` Medium, `2` High, `3` Critical.

## Exemplos de requisicao

Cadastro de usuario:

```json
{
  "name": "Maria Silva",
  "email": "maria@example.com",
  "password": "senha-com-8-caracteres",
  "role": 0
}
```

Login:

```json
{
  "email": "maria@example.com",
  "password": "senha-com-8-caracteres"
}
```

Criacao de chamado:

```json
{
  "title": "Nao consigo acessar o sistema",
  "description": "O login retorna uma mensagem de erro.",
  "status": 0,
  "priority": 1,
  "customerId": 1
}
```

Criacao de comentario:

```json
{
  "content": "Estou analisando o problema.",
  "userId": 2,
  "ticketId": 1
}
```
