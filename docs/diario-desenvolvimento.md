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


## Etapa 5 — Busca e ordenação

### ViewModel da listagem

Foi criado o `ProdutoIndexViewModel` para reunir os produtos e as informações
específicas da tela de listagem, como o texto de busca e a ordenação selecionada.

O model `Produto` continua sendo utilizado diretamente nos formulários. O ViewModel
foi usado apenas na listagem porque busca e ordenação são informações da interface e
não fazem parte da tabela de produtos.

O arquivo placeholder `ProdutoViewModel` foi removido para evitar código sem uso.

### Busca por nome

A busca utiliza o método `Contains`, permitindo encontrar produtos por parte do nome.

O texto informado é tratado com `Trim` para remover espaços no começo e no final.
O formulário utiliza GET, permitindo que a busca apareça na URL e possa ser repetida
ou compartilhada.

### Ordenação

Foram implementadas quatro opções:

- Nome de A até Z.
- Nome de Z até A.
- Preço do menor para o maior.
- Preço do maior para o menor.

Somente valores de ordenação conhecidos são aceitos pelo controller. Quando o valor
recebido é inválido ou não informado, a aplicação utiliza nome de A até Z.

A busca é aplicada antes da ordenação e a consulta só é executada no `ToListAsync`.
Assim, o filtro e a ordenação são processados juntos pelo SQL Server.

### Decisão de interface

Inicialmente, a ordenação foi implementada por meio de links nos cabeçalhos e setas.
Durante o teste, foi percebido que esse comportamento poderia não ser claro para
todos os usuários.

A interface foi alterada para usar um campo chamado `Ordenar por`, com as quatro
opções escritas por extenso. Essa solução ficou mais simples de entender e demonstrar.

### Testes realizados

- Busca utilizando parte do nome.
- Busca sem resultados.
- Limpeza da busca.
- Nome crescente e decrescente.
- Preço crescente e decrescente.
- Busca e ordenação utilizadas ao mesmo tempo.
- Envio de um valor de ordenação inválido pela URL.


## Etapa 6 — Funcionalidade adicional: paginação

### Motivo da escolha

A paginação foi escolhida porque a quantidade de produtos pode crescer com o uso do
sistema. Exibir todos os registros de uma só vez deixaria a página mais extensa e
poderia aumentar o volume de dados consultados no banco.

Também foi uma escolha que combinou naturalmente com a busca e a ordenação já
implementadas.

### Valor agregado

A paginação limita a listagem a cinco produtos por página, tornando a navegação mais
organizada e evitando que todos os registros sejam carregados ao mesmo tempo.

Os controles mostram a página atual e permitem navegar pelas páginas anterior,
seguinte ou por número.

A busca e a ordenação são preservadas durante a navegação.

### Implementação

O total de produtos filtrados é calculado com `CountAsync`.

O método `Skip` ignora os registros das páginas anteriores e o método `Take` limita
a consulta a cinco registros.

A quantidade de páginas é calculada a partir do total de produtos e do tamanho da
página. Valores de página inválidos, como zero, números negativos ou números maiores
que o total, são ajustados para uma página válida.

A ordenação recebe critérios adicionais com `ThenBy` para manter resultados estáveis
entre as páginas quando dois produtos possuem o mesmo nome ou preço.

### Alternativas consideradas

Foram consideradas outras funcionalidades adicionais:

- Exportação para CSV, que seria útil para relatórios, mas não melhoraria diretamente
  a navegação diária da listagem.
- Categorias de produtos, que exigiriam novas tabelas, relacionamento e alterações
  maiores no escopo.
- Filtro por faixa de preço, que seria útil, mas agregaria menos valor com uma
  quantidade pequena de campos.
- Testes automatizados, que continuam registrados como uma melhoria futura.

A paginação foi escolhida por entregar valor visível ao usuário sem aumentar
desnecessariamente a complexidade do projeto.

### Testes realizados

- Primeira página limitada a cinco produtos.
- Navegação para a segunda página.
- Botões anterior e próxima habilitados e desabilitados corretamente.
- Página atual destacada.
- Busca junto com paginação.
- Ordenação junto com paginação.
- Busca, ordenação e paginação usadas ao mesmo tempo.
- Página zero corrigida para a primeira página.
- Página acima do total corrigida para a última página.


## Etapa 7 — Revisão e dados de demonstração

Foi realizada uma revisão dos principais arquivos publicados no GitHub.

Durante a revisão, foram removidos comentários repetidos no `ProdutoController` e
melhorada a descrição do `ApplicationDbContext`.

O `.gitignore` foi atualizado para ignorar arquivos com extensão `.db`, atendendo
explicitamente ao checklist da entrega, mesmo que a aplicação utilize SQL Server.

O arquivo `database/seed.sql` foi preenchido com 12 produtos de exemplo. Os dados
permitem testar rapidamente a busca, as ordenações e a paginação.

O script verifica se a tabela já possui registros antes da inserção. Isso evita a
duplicação dos produtos caso ele seja executado mais de uma vez.

## Revisão final e teste da entrega

Foi realizado um teste a partir de um novo clone do repositório para simular a execução feita pelo avaliador.

Durante o teste, o comando `dotnet restore` apresentou ambiguidade porque a pasta contém o arquivo da solução e o arquivo do projeto. A instrução do README foi corrigida para:

```powershell
dotnet restore .\DesafioTecnico.sln
Depois da correção, foram validados:

- Restauração dos pacotes.
- Conexão com o SQL Server.
- Execução das migrations.
- Compilação da solução.
- Inicialização da aplicação.
- Listagem, busca, ordenação e paginação.
- Exibição de valores e datas na cultura brasileira.

A página inicial também foi atualizada. O conteúdo original ainda apresentava o projeto como um ponto de partida, então ele foi substituído por uma apresentação da solução concluída, com um resumo das funcionalidades e um botão de acesso à gestão de produtos.