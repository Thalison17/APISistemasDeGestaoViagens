# Sistema de Gestão de Viagens

## Objetivo Estratégico do Projeto
“Automatizar e melhorar a gestão de viagens para eficiência e satisfação do cliente.”

---

## Resumo do Projeto
### Minimundo Sistema de Gestão de Viagens
O Sistema de Gestão de Viagens foi desenvolvido para atender às necessidades de controle e organização de viagens, abrangendo tanto clientes quanto destinos e itinerários. No sistema, cada cliente é identificado por um código exclusivo e possui informações como nome, CPF, endereço, e-mail e telefone. Os destinos, por sua vez, são cadastrados com um código único, descrição detalhada, localização e preço por dia.

As viagens planejadas para os clientes são registradas no sistema, sendo vinculadas a um cliente específico e a um destino. Para cada viagem, registra-se o código único e a data de início e término. Todas as viagens podem ser complementadas com informações sobre os serviços contratados, como hospedagens, transportes e atividades, detalhando o nome do serviço, seu fornecedor, custo e prazo de execução.

No sistema, também é possível acompanhar as reservas realizadas pelos clientes. Cada reserva possui um código, está associada a um cliente e uma viagem e contém informações como data de criação, status do pagamento (“EXEMPLO DE QUAIS PODEM SER AS VARIÁVEIS: pago ou pendente”, método de pagamento e o custo total da reserva.

Além das viagens e reservas, o sistema suporta o controle de pagamentos realizados pelos clientes. Cada pagamento é vinculado a uma reserva ou viagem, registra o valor, método utilizado (“EXEMPLO DE QUAIS PODEM SER AS VARIÁVEIS: cartão de crédito, transferência bancária, etc.”), a data e o status (“EXEMPLO DE QUAIS PODEM SER AS VARIÁVEIS: pendente, pago ou cancelado”).
Relatórios gerenciais é uma funcionalidade que permite o acesso certas informações, como listagem de viagens por cliente, destinos mais procurados em determinado período e análise de faturamento mensal. Além disso, é possível calcular a média de gastos por cliente, o tempo médio das viagens realizadas e o número de reservas canceladas em um intervalo de tempo.

---

## Imersão
A Imersão tem como objetivo entender o problema e, assim, determinar o Produto Mínimo e Viável (MVP, do inglês, Minimum Viable Product). Essa atividade ajuda no desenvolvimento do planejamento do projeto. Ao fim dessa atividade, a equipe sabe as funcionalidades mínimas que devem ser produzidas para atender as necessidades do público alvo do produto.

### Problemas que o projeto visa resolver
- Dificuldade de conseguir reservar viagens e hospedagens com facilidade
  - Acesso dificultado às informações.
  - Reservas presenciais nada práticas.
- Dificuldade dos gestores de agência de viagens em controlar as reservas.

### Pessoas que o projeto visa ajudar
O projeto tem como objetivo atender os seguintes tipos de pessoas:
- Clientes de agências de viagens.
- Gestores de agências.

### Benefícios do Sistema:
- **Visibilidade:** Acompanhamento centralizado de viagens e reservas.
- **Eficiência:** Automação de processos manuais.
- **Organização:** Gerenciamento simplificado para agências.

### Produtos semelhantes
- Decolar.com
- CVC
- ViajaNet

### Personas 
- Pâmella, 23 anos. Gosta de viajar economicamente, compra com antecedência e pesquisa em múltiplas fontes, comparando preços, até chegar em um resultado.
- Marcelo, 36 anos. Trabalhador incessante, Marcelo, quando não aguenta mais sua rotina, precisa extravasar, então compra uma passagem para os próximos dias.
- Francisca, 67 anos. Senhora aposentada que frequentemente viaja para ver a família.

### Soluções encontradas
- **Plataforma Centralizada para Reservas**  
O sistema unifica a gestão de viagens, hospedagens e reservas em uma única plataforma, permitindo que clientes realizem todo o processo de forma online e intuitiva, eliminando a necessidade de interações presenciais ou múltiplos canais.
- **Gestão Eficiente para Agências**  
Um painel administrativo fornece aos gestores um controle completo das reservas, clientes e destinos, ajudando a organizar o fluxo de trabalho e melhorar a eficiência operacional.
- **Automação de Processos**  
A automação reduz erros manuais e simplifica atividades como confirmação de reservas, cálculos de custos totais e acompanhamento de status de pagamento.

### Diferencial do projeto
- **Apoio ao Planejamento de Viagens**  
Além das reservas, o sistema inclui funcionalidades que ajudam os clientes a planejar suas viagens de forma mais eficiente, como cálculo automático de custos e exibição de informações detalhadas dos destinos.
- **Modelo Escalável e Customizável**  
A arquitetura foi desenvolvida para ser escalável, permitindo que novas funcionalidades sejam incorporadas à medida que as necessidades dos usuários evoluem.
- **Gestão Facilitada para Agências de Viagem**  
O projeto não apenas beneficia os clientes, mas também resolve desafios específicos dos gestores, oferecendo ferramentas avançadas para controle e organização.

---

### Backlog do projeto

TABELA AQUI

---

## Construção

### Tecnologias Utilizadas
- **C#:**
Linguagem principal, escolhida por sua robustez e sintaxe clara.
- **ASP.NET Core 8.0:**
Framework para criação de APIs RESTful modernas e eficientes.
- **Swagger:**
Ferramenta para documentação interativa e validação de endpoints.
- **Banco de Dados em Memória:**
Usado para testes e prototipagem com EF Core.

## Diagrama de projeto

```mermaid
classDiagram
```

##  Diagrama de classes

```mermaid
classDiagram
```

## Diagrama de caso de uso

![casodeuso](https://github.com/user-attachments/assets/7bc6f2f0-364c-48e5-8547-84587dbf35d9)

---

