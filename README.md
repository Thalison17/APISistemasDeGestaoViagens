# Sistema de Gestão de Viagens - Minimal API em C#

Este projeto é uma Minimal API em C# para gerenciamento de reservas de viagem de avião. O sistema permite que clientes façam reservas para destinos especificos.

## Funcionalidades

- Cadastro de Clientes: Gerencia informações dos clientes, como nome, e-mail, telefone e CPF.
- Cadastro de Destinos: Registra destinos para viagens, incluindo dados como nome, localização, país e preço.
- Cadastro de Reservas: Cria, Consulta e Atualiza reservas de viagens para determinados destinos.

## Diagrama de Pacotes da Arquitetura MVC

![diagramaDeProjeto](https://github.com/user-attachments/assets/8e3f4255-8c62-4972-bd6c-e59562a5ed3c)

##  Diagrama de classes por Camadas

### CONTROLLER LAYER

```mermaid
classDiagram
    ClienteController --> IClienteRepository
    DestinoController --> IGenericRepository
    ReservaController --> IGenericRepository
    ViagemController --> IGenericRepository
    ClienteController ..> ClienteDTO : uses
    ClienteController ..> ClienteCreateDTO : uses
    DestinoController ..> DestinoDTO : uses
    DestinoController ..> DestinoCreateDTO : uses
    ReservaController ..> ReservaDTO : uses
    ReservaController ..> ReservaCreateDTO : uses
    ViagemController ..> ViagemDTO : uses
    RelatorioController ..> ReportService : uses
    ClienteDTO --> Cliente : maps to
    ClienteCreateDTO --> Cliente : maps to
    DestinoDTO --> Destino : maps to
    DestinoCreateDTO --> Destino : maps to
    ReservaDTO --> Reserva : maps to
    ReservaCreateDTO --> Reserva : maps to
    ViagemDTO --> Viagem : maps to

class ClienteController {
        - IClienteRepository _clienteRepository
        + GetAll() Task<ActionResult<IEnumerable<ClienteDTO>>>
        + GetById(int) Task<ActionResult<ClienteDTO>>
        + Create(ClienteCreateDTO) Task<ActionResult>
        + Update(int, Cliente)
        + Delete(int) Task<ActionResult>
    }

    class DestinoController {
        - IGenericRepository<Destino> _repository
        + GetAll() Task<ActionResult<IEnumerable<DestinoDTO>>>
        + GetById(int) Task<ActionResult<DestinoDTO>>
        + Create(Destino) Task<ActionResult>
        + Update(int, DestinoDTO) Task<ActionResult>
        + Delete(int) Task<ActionResult>
    }

    class RelatorioController {
        - ReportService _reportService
        + GetReservasPorPeriodo() IActionResult
        + GetDestinosMaisProcurados() IActionResult
        + GetClientesFrequentes() IActionResult
        + GetReceitaPorViagem() IActionResult
    }

    class ReservaController {
        - IGenericRepository<Reserva> _reservaRepository
        - IGenericRepository<Viagem> _viagemRepository
        + GetAll() Task<ActionResult<IEnumerable<ReservaDTO>>>
        + GetById(int) Task<ActionResult<ReservaDTO>>
        + Create([FromBody] ReservaCreateDTO) Task<ActionResult>
        + Update(int, ReservaDTO) Task<ActionResult>
        + Delete(int) Task<ActionResult>
    }

    class ViagemController {
        - IGenericRepository<Viagem> _repository
        + GetAll() Task<ActionResult<IEnumerable<ViagemDTO>>>
        + GetById(int) Task<ActionResult<ViagemDTO>>
        + Update(int, ViagemDTO) Task<ActionResult>
        + Delete(int) Task<ActionResult>
    }

    class ClienteDTO {
        + int ClienteId
        + string Nome
        + string Email
        + string Telefone
        + string Cpf
        + ICollection<ReservaDTO> Reservas
    }

    class ClienteCreateDTO {
        + string Nome
        + string Email
        + decimal Telefone
        + string Cpf
    }

    class DestinoDTO {
        + string Localizacao
        + string Pais
        + decimal PrecoPorDia
    }

    class DestinoCreateDTO {
        + string Localizacao
        + string Pais
        + decimal PrecoPorDia
    }

    class ReservaCreateDTO {
        + int ClienteId
        + DateTime DataReserva
        + string StatusPagamento
        + string MetodoPagamento
    }

    class ReservaDTO {
        + int ReservaId
        + int ClienteId
        + int ViagemId
        + DateTime DataReserva
        + string StatusPagamento
        + string MetodoPagamento
        + decimal CustoTotal
        + ViagemDTO Viagem
    }

    class ViagemDTO {
        + int ViagemId
        + int DestinoId
        + DateTime DataPartida
        + DateTime DataRetorno
        + string Status
    }

    class ReportService {
            -AppDbContext _context
            +ObterReservasPorPeriodo() IEnumerable<string>
            +ObterDestinosMaisProcurados() IEnumerable<string>
            +ObterClientesFrequentes() IEnumerable<string>
            +ObterReceitaPorViagem() IEnumerable<string>
        }
```


### MODEL LAYER

```mermaid
classDiagram
    Cliente"1" -- "*" Reserva
    Reserva"*" -- "1" Viagem
    Cliente"1" -- "*" Viagem
    Destino"1" -- "*" Viagem
    AppDbContext --> Cliente
    AppDbContext --> Destino
    AppDbContext --> Reserva
    AppDbContext --> Viagem

    class Cliente{
        +int ClienteId
        +string Nome
        +string Email
        +string Telefone
        +string Cpf
        +ICollection<Reserva> Reservas
    }

    class Destino{
        +int DestinoId
        +string Localizacao
        +string Pais
        +decimal PrecoPorDia
    }

    class Reserva{
        +int ReservaId
        +int ClienteId
        +int ViagemId
        +DateTime DataReserva
        +string StatusPagamento
        +string MetodoPagamento
        +decimal CustoTotal
        +Ciente Cliente
        +Viagem Viagem
        +int DuracaoDias
    }

    class Viagem{
        +int ViagemId
        +int DestinoId
        +DateTime DataPartida
        +DateTime DataRetorno
        +string Status
        +Cliente Cliente
        +Destino Destino
    }

    class AppDbContext {
        +DbSet<Cliente> Clientes
        +DbSet<Destino> Destino
        +DbSet<Viagem> Viagem
        +DbSet<Reserva> Reservas
        +OnModelCreating(ModelBuilder)
        +OnConfiguring(DbContextOptionsBuilder)
    }
```


### REPOSITORY LAYER

```mermaid
classDiagram
IGenericRepository <|-- GenericRepository
IClienteRepository <|-- ClienteRepository
GenericRepository <|-- ClienteRepository
AppDbContext <-- GenericRepository
AppDbContext <-- ClienteRepository

class IGenericRepository {
  <<interface>>
  + GetAllAsync(Func<IQueryable<T>, IQueryable<T>>) Task<IEnumerable<T>>
  + GetByIdAsync(int, Func<IQueryable<T>, IQueryable<T>>) Task<T?>
  + AddAsync(T) Task
  + UpdateAsync(T) Task
  + DeleteAsync(int) Task
}

class IClienteRepository {
  <<interface>>
  + GetAllWithReservasAsync() Task<IEnumerable<Cliente>>
  + GetByIdWithReservasAsync(int) Task<Cliente>
}

class GenericRepository {
  - AppDbContext _context
  - DbSet<T> _dbSet
  + GetAllAsync(Func<IQueryable<T>, IQueryable<T>>) Task<IEnumerable<T>>
  + GetByIdAsync(int, Func<IQueryable<T>, IQueryable<T>>) Task<T?>
  + AddAsync(T) Task
  + UpdateAsync(T) Task
  + DeleteAsync(int) Task
}

class ClienteRepository {
  - AppDbContext _context
  + GetAllWithReservasAsync() Task<IEnumerable<Cliente>>
  + GetByIdWithReservasAsync(int) Task<Cliente>
}
```


## Estrutura de arquivos e pastas
```shell
📂 ProjetoRaiz 
  ├── 📂 Controllers # Mapeamento dos endpoints para CRUD
  ├── 📂 Data # Classe AppDbContext que mapeia as Classes de Model para o Banco de dados em memória 
  ├── 📂 Models # Classes que representam as entidades (Cliente, Destino, Reserva, Viagem)
  ├── 📂 Repository # Lógica de acesso ao banco de dados e manipulação direta das entidades
  ├── 📂 Services # Implementação das regras de negócio e processamento dos dados.
  └── Program.cs # Configuração da API e mapeamento de endpoints
```
