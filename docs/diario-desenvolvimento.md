# Diário de Desenvolvimento

Este documento registra as etapas realizadas durante o desenvolvimento do desafio,
as decisões técnicas tomadas, as validações executadas e os conhecimentos adquiridos.

## Etapa 1 — Preparação do ambiente

### Atividades realizadas

- Instalação e configuração do Visual Studio.
- Instalação do SQL Server Express.
- Instalação e conexão com o SQL Server Management Studio.
- Abertura e execução do projeto inicial.
- Validação do funcionamento inicial da aplicação.
- Criação do primeiro commit do projeto.

### Arquivos ignorados

Foram adicionadas regras para impedir o versionamento de arquivos locais e gerados
pelo Visual Studio, como `.vs/`, `*.user` e `*.suo`.

## Etapa 2 — Configuração da persistência

### Decisão técnica

Foi escolhido o Entity Framework Core como mecanismo de acesso a dados.

A escolha foi feita porque a aplicação possui um escopo pequeno, concentrado em
operações de CRUD. O Entity Framework Core reduz código repetitivo, possui integração
oficial com SQL Server e oferece suporte a migrations e geração de scripts SQL.

Para este projeto, não foi criada uma camada de Repository ou Service. O acesso ao
banco será realizado por meio do `ApplicationDbContext`, injetado no controller.
Essa decisão mantém a solução proporcional ao tamanho do desafio e evita abstrações
sem uma necessidade concreta.

### Pacotes instalados

- `Microsoft.EntityFrameworkCore.SqlServer` — versão 10.0.10
- `Microsoft.EntityFrameworkCore.Tools` — versão 10.0.10
- `Microsoft.EntityFrameworkCore.Design` — versão 10.0.10

### Implementação

Foi criada a classe `ApplicationDbContext` na pasta `Data`. Ela representa o contexto
de acesso ao banco e expõe a coleção `Produtos` por meio de um `DbSet<Produto>`.

O campo `Preco` foi configurado com precisão `decimal(18,2)`, adequada para armazenar
valores monetários com duas casas decimais.

A connection string foi configurada no `appsettings.json` para utilizar:

- SQL Server Express
- Instância `localhost\SQLEXPRESS`
- Banco `DesafioProdutosDb`
- Autenticação integrada do Windows
- Confiança no certificado local de desenvolvimento

O `ApplicationDbContext` foi registrado no contêiner de injeção de dependências no
arquivo `Program.cs`.

### Validações realizadas

- A solução foi compilada sem erros.
- A aplicação foi executada com sucesso.
- A página inicial continuou funcionando depois da configuração do Entity Framework.
- A conexão ainda não foi utilizada porque o banco será criado na próxima etapa.

### Uso de Inteligência Artificial

O ChatGPT foi utilizado nesta etapa para:

- Explicar o funcionamento do Entity Framework Core.
- Auxiliar na escolha da abordagem de acesso a dados.
- Orientar a instalação dos pacotes NuGet.
- Auxiliar na criação do `ApplicationDbContext`.
- Explicar a connection string.
- Auxiliar no registro do contexto no `Program.cs`.
- Revisar a configuração e orientar sua validação.

Todo o código adicionado foi acompanhado de explicações para garantir seu entendimento.

## Etapa 3 — Criação do banco de dados

### O que foi feito

Foi criada a migration inicial do projeto com o comando `Add-Migration InitialCreate`.

A migration contém as instruções necessárias para criar a tabela `Produtos` com os
seguintes campos:

- `Id`: número inteiro, chave primária e preenchido automaticamente.
- `Nome`: texto obrigatório com limite de 100 caracteres.
- `Descricao`: texto opcional.
- `Preco`: decimal obrigatório com duas casas decimais.
- `DataCadastro`: data e hora obrigatórias.

O banco `DesafioProdutosDb` foi criado no SQL Server Express com o comando
`Update-Database`.

O Entity Framework Core também criou a tabela `__EFMigrationsHistory`, utilizada para
registrar quais migrations já foram aplicadas ao banco.

### Script SQL

O arquivo `database/create_database.sql` foi gerado pelo Entity Framework Core por
meio do comando:

`Script-Migration -Output ..\database\create_database.sql`

O script permite criar a estrutura das tabelas sem precisar escrever manualmente
todos os comandos SQL. Ele será entregue junto com o projeto, conforme solicitado
na especificação do desafio.

### Validações realizadas

- A migration foi criada sem erros.
- O banco foi criado no SQL Server Express.
- A tabela `Produtos` foi conferida no SQL Server Management Studio.
- Os tipos e a obrigatoriedade das colunas foram conferidos.
- O script `create_database.sql` foi gerado e seu conteúdo foi revisado.

### Uso de Inteligência Artificial

O ChatGPT foi utilizado nesta etapa para explicar migrations, orientar a execução
dos comandos, revisar a estrutura gerada e ajudar na conferência do script SQL.

## Etapa 4 — Listagem e cadastro de produtos

### Implementação inicial

Foi implementada a injeção do `ApplicationDbContext` no `ProdutoController`.

A ação `Index` consulta os produtos de forma assíncrona e utiliza `AsNoTracking`,
pois os registros são usados somente para leitura na listagem.

Também foi criada a tela de listagem com Razor e Bootstrap. Quando não existem
produtos, a tela apresenta uma mensagem informativa. Quando existem registros,
eles são exibidos em uma tabela.

O cadastro foi implementado com validação no servidor, proteção antifalsificação e
preenchimento da data de cadastro pela própria aplicação.

### Dificuldade com o campo de preço

Durante os testes, foi identificado um problema com o separador decimal. Inicialmente,
o formulário não aceitou o valor `250,00`. Ao utilizar `250.00`, o servidor interpretou
o ponto como separador de milhar e armazenou o valor como `25.000,00`.

Para resolver o problema, a aplicação foi configurada para utilizar a cultura
brasileira `pt-BR`. A validação do campo de preço também foi ajustada para aceitar
vírgula como separador decimal.

Depois da correção, o valor `250,00` foi armazenado e exibido corretamente.

Essa dificuldade mostrou a importância de manter a mesma regra de formato entre a
validação do navegador e o processamento realizado pelo servidor.
### Detalhamento, edição e exclusão

A página de detalhes foi implementada para mostrar todos os dados de um produto.
Quando o ID informado não existe, o controller retorna uma resposta HTTP 404.

A edição carrega primeiro o registro existente no banco. Somente os campos `Nome`,
`Descricao` e `Preco` são atualizados. O `Id` e a `DataCadastro` original são
preservados.

A exclusão foi dividida em duas ações. A primeira exibe uma página de confirmação e
a segunda, acessada por POST, remove o registro. Essa separação evita que um produto
seja excluído apenas ao acessar um endereço no navegador.

As ações que alteram dados utilizam proteção antifalsificação e validação do
`ModelState`.

### Uso do model nas views

O model `Produto` foi usado diretamente nos formulários porque o CRUD é pequeno e os
campos exibidos correspondem aos campos da entidade.

Para evitar alterações indevidas, foi utilizada uma lista de campos permitidos com
`Bind`. Na edição, o produto existente também é carregado antes da atualização, e
somente os campos editáveis recebem novos valores.

Um ViewModel específico para os formulários foi considerado, mas não foi criado
nesta etapa para manter a solução simples e proporcional ao desafio.

### Testes realizados

- Listagem com banco vazio.
- Cadastro com dados válidos.
- Cadastro com nome e preço inválidos.
- Campo de descrição opcional.
- Visualização dos detalhes.
- Consulta de produto inexistente.
- Edição preservando a data de cadastro.
- Cancelamento da exclusão.
- Confirmação da exclusão.
- Exibição das mensagens de sucesso.
- Entrada de preço utilizando vírgula.

### Uso de Inteligência Artificial

O ChatGPT auxiliou na explicação do fluxo MVC, na implementação das ações do
controller, na criação das Razor Views, na revisão das validações e na identificação
do problema de cultura no campo de preço.
