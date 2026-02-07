# Aula24 EF Core API

Uma API REST para gerenciamento de hamburgueria, desenvolvida em **ASP.NET Core 10** com **Entity Framework Core** e banco de dados **PostgreSQL**.

## 📋 Descrição

Sistema completo de gerenciamento de pedidos de hamburgueria com autenticação de usuários, controle de pedidos, produtos e avaliações. A aplicação segue a arquitetura em camadas (Controller → Service → Repository) para garantir separação de responsabilidades e manutenibilidade do código.

---

## 🛠️ Tecnologias Utilizadas

- **.NET 10** - Framework principal
- **ASP.NET Core** - Framework web
- **Entity Framework Core 10** - ORM para banco de dados
- **PostgreSQL 15+** - Banco de dados relacional
- **Swagger/Swashbuckle** - Documentação de API
- **BCrypt.Net-Next** - Hash seguro de senhas
- **Npgsql** - Driver PostgreSQL para .NET

---

## 📦 Dependências do Projeto

```xml
<ItemGroup>
    <PackageReference Include="BCrypt.Net-Next" Version="4.0.3" />
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="10.0.2" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="10.0.2" />
    <PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="10.0.0" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="10.1.2" />
</ItemGroup>
```

---

## 🚀 Como Instalar e Rodar

### Pré-requisitos

- **.NET SDK 10.0** ou superior instalado
- **PostgreSQL 15+** instalado e rodando
- **Git** para clonar o repositório

### Passo 1: Clonar o Repositório

```bash
git clone <seu-repositorio>
cd Aula24EfCore
```

### Passo 2: Restaurar Dependências

```bash
dotnet restore
```

### Passo 3: Configurar o Banco de Dados

Edite o arquivo `appsettings.json` com as credenciais do seu PostgreSQL:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost:5432;Database=pedidos;Username=postgres;Password=root"
  }
}
```

> **Nota:** Substitua `localhost`, `5432`, `pedidos`, `postgres` e `root` pelos valores do seu PostgreSQL.

### Passo 4: Executar Migrations (se aplicável)

Se houver migrations pendentes:

```bash
dotnet ef database update
```

### Passo 5: Rodar a Aplicação

```bash
dotnet run
```

A API será executada em `https://localhost:5001` (HTTPS) ou `http://localhost:5000` (HTTP).

---

## 📚 Rotas Principais da API

### Usuários

| Método | Rota | Descrição | Status |
|--------|------|-----------|--------|
| `GET` | `/user` | Lista todos os usuários | 200 OK |
| `GET` | `/user/orders` | Lista usuários com seus pedidos | 200 OK |
| `POST` | `/user` | Cria um novo usuário | 201 Created |
| `PATCH` | `/user/{id}` | Atualiza senha do usuário | 200 OK |

### Exemplos de Requisição

#### GET - Listar todos os usuários
```bash
curl -X GET "http://localhost:5000/user"
```

**Resposta (200):**
```json
[
  {
    "id": "550e8400-e29b-41d4-a716-446655440000",
    "email": "joao@exemplo.com",
    "nome": "João Silva",
    "createdAt": "2026-02-06T10:30:00Z"
  }
]
```

#### GET - Listar usuários com pedidos
```bash
curl -X GET "http://localhost:5000/user/orders"
```

**Resposta (200):**
```json
[
  {
    "id": "550e8400-e29b-41d4-a716-446655440000",
    "email": "joao@exemplo.com",
    "nome": "João Silva",
    "createdAt": "2026-02-06T10:30:00Z",
    "pedidos": [
      {
        "id": "660e8400-e29b-41d4-a716-446655440001",
        "createdAt": "2026-02-06T11:00:00Z",
        "paymentType": "Crédito",
        "total": 45.50
      }
    ]
  }
]
```

#### POST - Criar novo usuário
```bash
curl -X POST "http://localhost:5000/user" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Maria Santos",
    "email": "maria@exemplo.com",
    "password": "Senha123"
  }'
```

**Resposta (201 Created):**
```json
{
  "id": "770e8400-e29b-41d4-a716-446655440002",
  "email": "maria@exemplo.com",
  "nome": "Maria Santos",
  "createdAt": "2026-02-06T11:00:00Z"
}
```

**Resposta (409 Conflict) - Email já existe:**
```json
{
  "status": 409,
  "title": "Conflito",
  "detail": "Email 'maria@exemplo.com' já está cadastrado",
  "type": "https://httpwg.org/specs/rfc9110.html#status.409"
}
```

#### PATCH - Atualizar senha
```bash
curl -X PATCH "http://localhost:5000/user/770e8400-e29b-41d4-a716-446655440002" \
  -H "Content-Type: application/json" \
  -d '{
    "senha": "NovaSenha456"
  }'
```

---

## 📁 Estrutura de Pastas

```
Aula24EfCore/
├── Controllers/                 # Endpoints da API
│   └── UserController.cs       # Controlador de usuários
├── Services/                    # Lógica de negócio
│   ├── UserService.cs          # Serviço de usuários
│   └── interface/
│       └── IUserService.cs     # Interface do serviço
├── Repositories/                # Acesso ao banco de dados
│   ├── UserRepository.cs       # Repositório de usuários
│   └── interfaces/
│       └── IUserRepository.cs  # Interface do repositório
├── Models/                      # Modelos do EF Core
│   ├── PedidosContext.cs       # DbContext principal
│   ├── TbUser.cs               # Modelo de Usuário
│   ├── TbOrder.cs              # Modelo de Pedido
│   ├── TbProduct.cs            # Modelo de Produto
│   ├── TbOrderProduct.cs       # Modelo de Pedido-Produto
│   └── TbRating.cs             # Modelo de Avaliação
├── DTO/                         # Data Transfer Objects
│   └── UserResponseDTO.cs      # DTOs de resposta/requisição
├── Properties/
│   └── launchSettings.json     # Configurações de launch
├── appsettings.json            # Configurações da aplicação
├── appsettings.Development.json # Configurações de desenvolvimento
├── Program.cs                  # Configuração da aplicação
├── Aula24EfCore.csproj        # Arquivo do projeto
├── Aula24EfCore.sln           # Solution do Visual Studio
└── README.md                   # Este arquivo
```

---

## 🏗️ Arquitetura em Camadas

O projeto segue o padrão de **arquitetura em 3 camadas**:

### 1️⃣ **Controller (Camada de Apresentação)**
- Recebe requisições HTTP
- Valida dados de entrada
- Retorna respostas HTTP
- Delega lógica para o Service

```csharp
[HttpPost]
public async Task<IActionResult> Create([FromBody] CreateUserRequestDTO request)
{
    var createdUser = await _service.CreateUserAsync(request);
    return CreatedAtAction(nameof(Get), new { id = createdUser.Id }, createdUser);
}
```

### 2️⃣ **Service (Camada de Negócio)**
- Implementa regras de negócio
- Valida dados (ex: email já existe?)
- Aplica transformações (ex: hash de senha)
- Coordena chamadas ao Repository

```csharp
public async Task<UserResponseDTO> CreateUserAsync(CreateUserRequestDTO request)
{
    var existingUser = await _repository.GetUserByEmailAsync(request.Email);
    if (existingUser != null)
        throw new InvalidOperationException($"Email já cadastrado");
    
    var newUser = new TbUser
    {
        Password = BCrypt.HashPassword(request.Password)
        // ... outros campos
    };
    
    return await _repository.CreateUserAsync(newUser);
}
```

### 3️⃣ **Repository (Camada de Dados)**
- Acessa o banco de dados via EF Core
- Abstrai consultas SQL
- Oferece métodos reutilizáveis

```csharp
public async Task<TbUser> CreateUserAsync(TbUser user)
{
    dbContext.TbUsers.Add(user);
    await dbContext.SaveChangesAsync();
    return user;
}
```

### Fluxo de Requisição

```
Requisição HTTP
      ↓
  Controller (validação inicial)
      ↓
  Service (regras de negócio)
      ↓
  Repository (acesso BD)
      ↓
  EntityFramework Core
      ↓
  PostgreSQL
```

---

## 🗄️ Configuração do Banco de Dados

### 1. Criar o Banco de Dados PostgreSQL

```sql
CREATE DATABASE pedidos;
```

### 2. Connection String

Arquivo: `appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost:5432;Database=pedidos;Username=postgres;Password=sua_senha"
  }
}
```

### Parâmetros:
- **Host**: Endereço do servidor PostgreSQL (padrão: `localhost`)
- **Port**: Porta PostgreSQL (padrão: `5432`)
- **Database**: Nome do banco de dados
- **Username**: Usuário PostgreSQL
- **Password**: Senha do usuário

### 3. Aplicar Migrations (se houver)

```bash
dotnet ef database update
```

### 4. Scaffold do Banco Existente

Se o banco já existe e você quer gerar os modelos:

```bash
dotnet ef dbcontext scaffold "Host=localhost:5432;Database=pedidos;Username=postgres;Password=root" Npgsql.EntityFrameworkCore.PostgreSQL --output-dir Models
```

---

## 🔐 Segurança

### Hash de Senhas com BCrypt

As senhas são armazenadas com hash BCrypt, é feito em `UserService.cs`:

```csharp
user.Password = BCrypt.HashPassword(request.Password);
```

**Nunca** retorne senhas nas DTOs:
```csharp
// ✅ Correto - sem password
public class UserResponseDTO
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Nome { get; set; }
}
```

### Validação de Entrada

Use `DataAnnotations` para validar DTOs:

```csharp
public class CreateUserRequestDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; }
}
```

---

## 📖 Swagger/OpenAPI

A documentação interativa está disponível durante o desenvolvimento:

**URL:** `http://localhost:5000`

No arquivo `Program.cs`:

```csharp
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

---

## 🧪 Testando a API

### Com cURL

```bash
# Criar usuário
curl -X POST http://localhost:5000/user \
  -H "Content-Type: application/json" \
  -d '{"name":"João","email":"joao@test.com","password":"123456"}'

# Listar usuários
curl -X GET http://localhost:5000/user

# Listar com pedidos
curl -X GET http://localhost:5000/user/orders
```

### Com Postman

1. Importe as rotas do Swagger: `http://localhost:5000/swagger/v1/swagger.json`
2. Ou crie requests manualmente

### Com VS Code REST Client

Crie um arquivo `test.http`:

```http
### Criar usuário
POST http://localhost:5000/user
Content-Type: application/json

{
  "name": "João Silva",
  "email": "joao@exemplo.com",
  "password": "Senha123"
}

### Listar usuários
GET http://localhost:5000/user

### Listar com pedidos
GET http://localhost:5000/user/orders
```

---

## ⚠️ Possíveis Erros e Soluções

### Erro: "Connection refused"
**Solução:** Certifique-se que PostgreSQL está rodando na porta 5432.

```bash
# Windows
pg_ctl -D "C:\Program Files\PostgreSQL\15\data" start

# Linux
sudo service postgresql start
```

### Erro: "Authentication failed"
**Solução:** Verifique as credenciais no `appsettings.json`.

### Erro: "Database does not exist"
**Solução:** Crie o banco:
```sql
CREATE DATABASE pedidos;
```

### Erro ao fazer POST: "A possible object cycle was detected"
**Solução:** Use DTOs sem referências circulares (já implementado neste projeto).

---

## 📝 Variáveis de Ambiente (Opcional)

Para produção, use variáveis de ambiente em vez de appsettings.json:

**Windows PowerShell:**
```powershell
$env:ConnectionStrings__DefaultConnection = "Host=servidor;Database=pedidos;Username=user;Password=pass"
```

**Linux/Mac:**
```bash
export ConnectionStrings__DefaultConnection="Host=servidor;Database=pedidos;Username=user;Password=pass"
```

---

## 📚 Referências Úteis

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [PostgreSQL Documentation](https://www.postgresql.org/docs/)
- [Swagger/OpenAPI](https://swagger.io/)
- [BCrypt.Net](https://github.com/BcryptNet/bcrypt.net)

---

## 📄 Licença

Este projeto é fornecido como material educacional.

---

## 👥 Autor

Desenvolvido como projeto de aula em ASP.NET Core com Entity Framework Core.

---

**Última atualização:** Fevereiro de 2026