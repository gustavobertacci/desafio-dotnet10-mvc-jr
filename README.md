# Desafio Técnico — Desenvolvedor(a) Fullstack .NET Júnior

Desafio prático em **.NET 10 + ASP.NET Core MVC** para avaliação técnica de candidatos.

---

## Sobre o desafio

Você vai desenvolver um **CRUD de Produtos** em ASP.NET Core MVC com Razor Views,
persistindo os dados em um banco relacional.

O repositório já traz um projeto MVC funcional como ponto de partida: o model
`Produto`, o layout com navbar, o `HomeController` e os stubs do
`ProdutoController` estão prontos. **O acesso a dados foi deixado em aberto de
propósito** — a escolha da abordagem é sua, e queremos entender o porquê.

> **Não buscamos a solução mais complexa.** Um CRUD simples, legível e bem
> justificado vale mais do que uma arquitetura cheia de camadas que não se
> sustentam. Prefira clareza a esperteza.

A especificação completa está em **[`docs/desafio.md`](docs/desafio.md)**.

---

## Objetivo

Avaliar, de forma prática:

- C# e ASP.NET Core MVC
- Razor Views e organização de front-end
- SQL e modelagem de dados relacional
- Boas práticas e legibilidade de código
- Organização do projeto
- Uso de Git (histórico de commits)
- Capacidade de tomada de decisão técnica
- Comunicação técnica escrita
- Uso consciente de Inteligência Artificial

---

## Tecnologias obrigatórias

| Item | Versão / Observação |
|---|---|
| .NET | 10 |
| ASP.NET Core MVC | Razor Views (não SPA, não Blazor, não Web API pura) |
| Bootstrap | 5 |
| Banco de dados | Relacional — **SQL Server Express** |
| Acesso a dados | **Sua escolha:** Entity Framework Core, Dapper ou ADO.NET |

O ponto de partida está em [`template-src/`](template-src/). Abra
`template-src/DesafioTecnico.sln` no Visual Studio ou rode:

```bash
cd template-src
dotnet run
```

---

## Como participar

1. **Faça um fork** deste repositório para a sua conta do GitHub.
2. **Desenvolva a solução** a partir de `template-src/`.
3. **Faça commits durante o desenvolvimento** — queremos ver a evolução do
   trabalho, não um único commit com tudo pronto.
4. **Atualize o README** do seu fork conforme [`docs/entrega.md`](docs/entrega.md).
5. **Compartilhe a URL** do seu repositório com quem te enviou o desafio.

Se preferir, você também pode abrir um Pull Request para o repositório original —
o checklist de entrega será carregado automaticamente.

---

## Avaliação

Serão avaliados:

- **Funcionalidades** — o CRUD, a busca e as ordenações funcionam de ponta a ponta?
- **MVC / Razor** — as responsabilidades estão no lugar certo? As views usam bem o Razor?
- **Banco de Dados** — a modelagem faz sentido? O SQL está correto e legível?
- **Organização do Código** — nomes, estrutura de pastas, ausência de código morto.
- **Git** — commits com mensagens claras e granularidade razoável.
- **Documentação** — o README explica como executar e por que as decisões foram tomadas.

Os pesos estão em **[`docs/criterios-avaliacao.md`](docs/criterios-avaliacao.md)**.
Não há pegadinhas: o que está documentado é exatamente o que avaliamos.

---

## Uso de IA

**O uso de Inteligência Artificial é permitido.** Ferramentas de IA fazem parte do
dia a dia da profissão e não faz sentido fingir o contrário.

O que pedimos é **transparência**. No README da sua entrega, informe:

- **Ferramenta utilizada** (ex.: Claude, ChatGPT, GitHub Copilot, Cursor)
- **Como foi utilizada** (gerar código, revisar, explicar conceitos, escrever SQL, testes)
- **Quais partes do código receberam auxílio**

Não há penalização por usar IA — e não há bônus por não usar. O que avaliamos é se
você **entende** o código que entregou. Espere perguntas sobre suas escolhas em uma
conversa técnica posterior.

---

## Estrutura do repositório

```text
desafio-dotnet10-mvc-jr/
├── README.md                     # este arquivo
├── LICENSE
├── .gitignore
├── AVALIACAO_INTERNA.md          # rubrica do avaliador
├── docs/
│   ├── desafio.md                # especificação completa
│   ├── criterios-avaliacao.md    # pesos da avaliação
│   └── entrega.md                # o que enviar
├── database/
│   ├── create_database.sql       # você preenche
│   └── seed.sql                  # opcional
└── template-src/                 # projeto MVC — ponto de partida
```

---

## Aviso

Este desafio foi criado exclusivamente para fins de avaliação técnica.

A reprodução total ou parcial deste conteúdo para utilização em outros processos
seletivos, treinamentos, cursos ou qualquer outra finalidade não é autorizada sem
consentimento prévio do autor.
