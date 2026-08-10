# Gestão de Produtos — Desafio .NET

Solução desenvolvida para o desafio técnico de Desenvolvedor(a) Fullstack .NET Júnior.

A aplicação permite cadastrar, listar, detalhar, editar e excluir produtos. A listagem também possui busca por nome, ordenação e paginação.

A especificação original pode ser consultada em [`docs/desafio.md`](docs/desafio.md).

## Funcionalidades

- Listagem de produtos.
- Cadastro de produto.
- Visualização dos detalhes.
- Edição de produto.
- Exclusão com confirmação.
- Busca parcial pelo nome.
- Ordenação por nome, crescente e decrescente.
- Ordenação por preço, crescente e decrescente.
- Validações no servidor e no cliente.
- Paginação integrada à busca e à ordenação.
- Mensagens de sucesso após cadastro, edição e exclusão.
- Formatação de preço utilizando a cultura brasileira.

## 1. Como executar

### Pré-requisitos

Antes de executar, é necessário instalar:

- [.NET SDK 10](https://dotnet.microsoft.com/download/dotnet/10.0)
- SQL Server Express 2022
- Visual Studio com suporte a ASP.NET Core ou outro editor compatível
- Git
- SQL Server Management Studio — opcional, mas recomendado para executar os scripts

### Clonar o repositório

```bash
git clone https://github.com/gustavobertacci/desafio-dotnet10-mvc-jr.git
cd desafio-dotnet10-mvc-jr/template-src
```

### Configurar o SQL Server

A aplicação está configurada para utilizar a seguinte instância:

```text
localhost\SQLEXPRESS
```

A connection string está no arquivo `template-src/appsettings.json`:

```json
"DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=DesafioProdutosDb;Trusted_Connection=True;TrustServerCertificate=True"
```

Caso a sua instância tenha outro nome, altere o valor de `Server`.

### Restaurar os pacotes

Dentro de `template-src`, execute:

```bash
dotnet restore .\DesafioTecnico.sln
```

### Opção 1 — Criar o banco com migrations

Esta é a opção recomendada.

Caso a ferramenta `dotnet-ef` ainda não esteja instalada:

```bash
dotnet tool install --global dotnet-ef --version 10.0.10
```

Depois execute:

```bash
dotnet ef database update
```

O comando cria o banco `DesafioProdutosDb`, a tabela `Produtos` e a tabela de controle de migrations.

No Visual Studio também é possível usar o Console do Gerenciador de Pacotes:

```powershell
Update-Database
```

### Opção 2 — Criar o banco com o script SQL

Não é necessário utilizar esta opção caso as migrations já tenham sido executadas.

Abra o SQL Server Management Studio. Ao iniciar o programa, será exibida a janela **Conectar ao Servidor**. Informe:

- Tipo de servidor: `Mecanismo de Banco de Dados`
- Nome do servidor: `localhost\SQLEXPRESS`
- Autenticação: `Autenticação do Windows`

Clique em **Conectar**.

Caso a janela não apareça automaticamente, no Pesquisador de Objetos selecione **Conectar > Mecanismo de Banco de Dados**.

Depois da conexão, clique em **Nova Consulta**. A consulta pode ser executada com o banco `master` selecionado:

```sql
IF DB_ID(N'DesafioProdutosDb') IS NULL
BEGIN
    CREATE DATABASE [DesafioProdutosDb];
END;
GO
```

Depois da criação, abra no SSMS o arquivo:

```text
database/create_database.sql
```

Na lista de bancos localizada na parte superior da janela de consulta, selecione `DesafioProdutosDb` e clique em **Executar**. Como alternativa, adicione ao início da consulta:

```sql
USE [DesafioProdutosDb];
GO
```

O script cria a tabela `Produtos` e registra a migration na tabela `__EFMigrationsHistory`. Ele foi gerado a partir das migrations do Entity Framework Core.

### Dados de demonstração

Este passo é opcional.

Depois de criar a estrutura, execute:

```text
database/seed.sql
```

O script insere 12 produtos se a tabela estiver vazia. Caso já existam registros, nenhum produto será inserido.

### Executar a aplicação

Dentro de `template-src`, execute:

```bash
dotnet run
```

A aplicação estará disponível em um dos endereços:

```text
https://localhost:7080
http://localhost:5080
```

Caso o certificado HTTPS ainda não esteja confiável:

```bash
dotnet dev-certs https --trust
```

No Visual Studio, também é possível abrir `template-src/DesafioTecnico.sln` e pressionar `F5`.

## 2. Arquitetura

O projeto utiliza a organização padrão do ASP.NET Core MVC:

```text
desafio-dotnet10-mvc-jr/
├── database/
│   ├── create_database.sql
│   └── seed.sql
├── docs/
│   ├── desafio.md
│   ├── entrega.md
│   ├── criterios-avaliacao.md
│   └── diario-desenvolvimento.md
└── template-src/
    ├── Controllers/
    │   └── ProdutoController.cs
    ├── Data/
    │   └── ApplicationDbContext.cs
    ├── Migrations/
    ├── Models/
    │   └── Produto.cs
    ├── ViewModels/
    │   └── ProdutoIndexViewModel.cs
    ├── Views/
    │   └── Produto/
    ├── Program.cs
    └── appsettings.json
```

### Responsabilidades

- `Models`: entidade `Produto` e suas validações.
- `Data`: contexto de acesso ao SQL Server.
- `Controllers`: recebe as requisições e coordena o acesso aos dados.
- `ViewModels`: guarda os dados específicos da tela de listagem.
- `Views`: páginas Razor responsáveis pela interface.
- `Migrations`: histórico das alterações da estrutura do banco.
- `database`: scripts SQL de criação e dados de demonstração.

### Fluxo de uma requisição

Uma requisição da tela de produtos chega ao `ProdutoController`. O controller utiliza o `ApplicationDbContext` para consultar ou alterar dados no SQL Server.

Na listagem, os produtos e as informações de busca, ordenação e paginação são colocados no `ProdutoIndexViewModel`. O ViewModel é enviado para a Razor View, que gera o HTML apresentado ao usuário.

## 3. Tecnologias utilizadas

| Tecnologia | Versão |
|---|---:|
| .NET | 10 |
| ASP.NET Core MVC | 10 |
| C# | Versão incluída no .NET 10 |
| Entity Framework Core SQL Server | 10.0.10 |
| Entity Framework Core Tools | 10.0.10 |
| Entity Framework Core Design | 10.0.10 |
| Bootstrap | 5.3.3 |
| SQL Server Express | 2022 |
| Razor Views | ASP.NET Core 10 |
| Git | Controle de versão |

## 4. Banco escolhido

Foi utilizado o SQL Server Express, com autenticação integrada do Windows.

O banco padrão se chama:

```text
DesafioProdutosDb
```

A tabela `Produtos` possui a seguinte estrutura:

| Coluna | Tipo | Regra |
|---|---|---|
| `Id` | `int` | Chave primária e identidade |
| `Nome` | `nvarchar(100)` | Obrigatório |
| `Descricao` | `nvarchar(max)` | Opcional |
| `Preco` | `decimal(18,2)` | Obrigatório |
| `DataCadastro` | `datetime2` | Obrigatório |

O schema completo está disponível em [`database/create_database.sql`](database/create_database.sql).

O arquivo [`database/seed.sql`](database/seed.sql) contém dados opcionais para demonstração.

## 5. Decisões técnicas

### Entity Framework Core

Escolhi Entity Framework Core porque o projeto é um CRUD pequeno. Ele reduz o código necessário para as operações de banco, possui integração oficial com SQL Server e tem suporte a migrations e geração de scripts SQL.

### Acesso a dados no controller

O `ApplicationDbContext` foi injetado diretamente no `ProdutoController`.

Não criei Repository Pattern nem Service Layer porque o projeto não possui regras de negócio complexas. Para este escopo, essas camadas aumentariam a quantidade de arquivos sem resolver uma necessidade real.

Em uma aplicação maior, com mais regras e integrações, uma camada de serviço seria considerada.

### Models e ViewModels

O model `Produto` é usado diretamente nos formulários de cadastro e edição porque os campos da tela correspondem à entidade.

Para reduzir o risco de alteração indevida, os POSTs possuem uma lista de campos permitidos com `Bind`. Na edição, o registro existente é carregado do banco e somente os campos editáveis são atualizados. A data original de cadastro é preservada.

Na listagem foi utilizado o `ProdutoIndexViewModel`, pois busca, ordenação e paginação são informações da tela e não pertencem à entidade do banco.

### Consultas assíncronas

As operações de banco utilizam `async` e `await`. Consultas somente de leitura utilizam `AsNoTracking`.

### Cultura brasileira

A aplicação utiliza a cultura `pt-BR` para exibir datas e valores monetários. A validação do preço foi ajustada para aceitar vírgula como separador decimal.

### Funcionalidade adicional: paginação

A paginação foi escolhida porque a lista de produtos pode crescer. Exibir todos os registros de uma vez aumentaria a quantidade de dados consultados e deixaria a tela extensa.

Organiza a navegação e exibe cinco produtos por página. Busca, ordenação e página atual funcionam em conjunto.

Foram consideradas outras alternativas:

- Categorias: exigiriam novas tabelas e relacionamentos com os produtos, saindo da proposta do desafio.
- Filtro por faixa de preço.
- Testes automatizados: deixei como melhoria futura.

### Escolhas fora do escopo

Não foram adicionados autenticação, API REST, Docker, pipeline de CI, SPA ou deploy em nuvem porque esses itens não eram o foco do desafio.

## 6. Melhorias futuras

Com mais tempo, eu faria:

- Testes automatizados para verificar as validações e as principais funcionalidades do CRUD.
- Filtro por período de cadastro, utilizando uma data inicial e uma data final.
- Inclusão de um campo de código para identificar cada produto de forma única.
- Restrição de unicidade no banco de dados para impedir o cadastro de códigos duplicados.
- Páginas de erro mais amigáveis para situações como produto não encontrado.

## 7. Uso de IA

A ferramenta utilizada foi o ChatGPT.

A IA foi utilizada para:

- Explicar conceitos de ASP.NET Core MVC e Entity Framework Core.
- Orientar a configuração do ambiente.
- Auxiliar na criação e revisão do `ApplicationDbContext`.
- Apoiar a implementação das ações do controller.
- Auxiliar na criação das Razor Views.
- Apoiar a implementação de busca, ordenação e paginação.
- Revisar código e auxiliar na documentação.



Todas as sugestões foram revisadas, executadas e testadas durante o desenvolvimento. O histórico detalhado está em [`docs/diario-desenvolvimento.md`](docs/diario-desenvolvimento.md).

## Perguntas obrigatórias

### 1. Quais foram suas principais decisões técnicas?

As principais decisões foram utilizar Entity Framework Core com SQL Server Express, injetar o `ApplicationDbContext` diretamente no controller e evitar camadas que não eram necessárias para o tamanho do projeto. Também utilizei o model diretamente nos formulários e um ViewModel específico para a listagem. Como funcionalidade adicional, escolhi a paginação integrada à busca e à ordenação.

### 2. O que faria diferente com mais tempo?

Com mais tempo, eu adicionaria testes automatizados para verificar as validações e as principais funcionalidades do CRUD, também adicionaria filtros por período de cadastro, permitindo consultar os produtos cadastrados entre uma data inicial e uma data final. Outra evolução seria adicionar um campo de código para identificar cada produto de forma única, permitindo detectar duplicações com uma regra confiável e uma restrição única no banco. Preferi não utilizar apenas o nome porque produtos diferentes podem possuir nomes iguais.

### 3. Qual foi a maior dificuldade encontrada?

Como este foi meu primeiro projeto prático com .NET, a principal dificuldade foi o próprio estudo do fluxo do ASP.NET Core MVC e como suas partes se conectam, principalmente controller, Entity Framework Core, e Razor Views. Um exemplo disso foi na aplicação do ApplicationDbContext. No início, eu confundia a criação do contexto com a leitura dos parâmetros de busca da URL. Durante o desenvolvimento, entendi que os parâmetros são recebidos pelo controller por meio do Model Binding, enquanto o contexto é configurado no Program.cs e dado ao controller por injeção de dependência para acessar o banco. Acompanhar o fluxo completo, desde a requisição até a consulta no SQL Server e a criação da View, foi o que me ajudou a conseguir esse entendimento.

### 4. Utilizou IA? Como?

Sim. Como ainda não possuía experiência prática com .NET, utilizei o ChatGPT para estudar conceitos, receber sugestões de implementação, revisar código e organizar a documentação. As partes que receberam auxílio estão descritas na seção “Uso de IA”. Todas as alterações foram testadas e acompanhadas de explicações para que eu pudesse entender o código que estava fazendo.
