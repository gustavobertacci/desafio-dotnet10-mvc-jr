# Especificação do Desafio

## Contexto

Você foi contratado(a) para construir a tela de gestão de produtos de um sistema
interno. O escopo é pequeno de propósito: o que nos interessa é **como** você
resolve, não o tamanho do que você entrega.

---

## Funcionalidades obrigatórias

### CRUD de Produtos

| Operação | Descrição |
|---|---|
| **Listar** | Exibir todos os produtos cadastrados em uma tabela |
| **Cadastrar** | Formulário para criar um novo produto |
| **Editar** | Formulário para alterar um produto existente |
| **Detalhar** | Página com os dados completos de um produto |
| **Excluir** | Remoção com confirmação antes de efetivar |

### Campos

| Campo | Tipo | Obrigatório |
|---|---|---|
| Nome | texto | Sim |
| Descrição | texto | Não |
| Preço | decimal | Sim |
| Data de Cadastro | data/hora | Sim |

O model `Produto` já existe em `template-src/Models/Produto.cs`. Você pode ajustá-lo
se precisar — basta justificar no README.

### Validações

- **Nome** é obrigatório
- **Nome** deve ter entre **3 e 100 caracteres**
- **Preço** deve ser **maior que zero**

As validações devem funcionar no servidor. Validação no cliente é bem-vinda, mas
**nunca** substitui a do servidor.

### Busca e ordenação

- **Busca por Nome** — busca parcial, na listagem
- **Ordenação por Nome** — crescente e decrescente
- **Ordenação por Preço** — crescente e decrescente

A busca e a ordenação devem funcionar em conjunto (buscar e ordenar ao mesmo tempo,
sem que um anule o outro).

---

## Banco de dados

**Banco relacional é obrigatório.** Escolha entre:

- **SQLServer Express** — mais simples de rodar e de avaliar; recomendado

Não aceitamos listas em memória, arquivos JSON ou similares como camada de
persistência.

### Acesso a dados

A escolha é sua:

- **Entity Framework Core**
- **Dapper**
- **ADO.NET**

O template **não** inclui `ApplicationDbContext`, Repository Pattern nem Service
Layer — isso é intencional. Monte a estrutura que você considera adequada para um
projeto deste tamanho e **explique o motivo no README**.

> Tanto "usei EF Core porque o CRUD é trivial e o tooling economiza tempo" quanto
> "usei ADO.NET para manter o controle do SQL e evitar dependências" são respostas
> válidas. O que avaliamos é a coerência entre a justificativa e o código.

### Script SQL

O script de criação do banco deve ser entregue em
[`../database/create_database.sql`](../database/create_database.sql).

Isso vale **mesmo se você usar EF Core com Migrations** — nesse caso, gere o script
com:

```bash
dotnet ef migrations script -o ../database/create_database.sql
```

O arquivo [`../database/seed.sql`](../database/seed.sql) é opcional, para carga
inicial de dados.

---

## Diferencial

> **Implemente uma funcionalidade adicional de sua escolha** e descreva no README:
>
> - **Motivo da escolha** — por que essa e não outra?
> - **Valor agregado** — o que ela melhora para quem usa o sistema?
> - **Alternativas consideradas** — o que você pensou em fazer e descartou, e por quê?

Não existe resposta certa aqui. Paginação, exportação para CSV, soft delete,
categorias, filtro por faixa de preço, testes automatizados, logging estruturado —
qualquer coisa serve, desde que você saiba explicar a decisão.

**Uma funcionalidade simples bem justificada vale mais do que três pela metade.**

---

## Fora de escopo

Não é necessário implementar (e não pontua):

- Autenticação e autorização
- API REST / endpoints JSON
- Docker ou pipeline de CI
- Deploy em nuvem
- Front-end SPA (React, Angular, Vue) ou Blazor

Se você implementar algo daqui de propósito, explique o motivo — não é proibido,
apenas não é o foco.

---

## Prazo e esforço

O desafio foi dimensionado para caber em algumas horas de trabalho focado.

Se o tempo apertar, **prefira entregar menos funcionalidades bem-feitas** e
documentar o que ficou de fora na seção "Melhorias futuras". Uma entrega honesta e
incompleta comunica mais do que uma entrega inflada.

---

## Como executar o template

```bash
cd template-src
dotnet restore
dotnet run
```

Ou abra `template-src/DesafioTecnico.sln` no Visual Studio Community 2022+ e pressione `F5`.

Ao abrir a solution você deve ver:

```text
DesafioTecnico
└── Desafio
```

O projeto compila e executa desde o primeiro clone. As views de `Produto` são
placeholders — é a sua parte do trabalho.

---

## Entrega

Consulte [`entrega.md`](entrega.md).
