# Entrega

## O que enviar

A **URL do seu fork** no GitHub, com o código e o README atualizados.

Se preferir, abra um Pull Request para o repositório original — o checklist de
entrega será carregado automaticamente.

---

## Atualize o README do seu fork

O README da sua entrega deve conter as seções abaixo. Pode substituir o README
original ou criar um `SOLUCAO.md` e referenciá-lo — só não deixe o avaliador
procurando.

### 1. Como executar

Passo a passo real, do clone até a aplicação no ar: pré-requisitos, como criar o
banco, como rodar as migrations (se houver) e qual comando executar. 
Assuma que quem vai ler nunca viu o seu projeto.

### 2. Arquitetura

Como o projeto está organizado e por quê. Quais pastas existem, o que vive em cada
uma, como uma requisição atravessa a aplicação.

### 3. Tecnologias utilizadas

Frameworks, bibliotecas e pacotes NuGet, com a versão de cada um.

### 4. Banco escolhido

SQL Server Express — Inclua o schema ou aponte para o script.

### 5. Decisões técnicas

A seção mais importante. Entity Framework, Dapper ou ADO.NET, e por quê? Usou
ViewModels ou passou o model direto para a view? Onde colocou o acesso a dados e o
que motivou essa escolha? O que você deliberadamente decidiu **não** fazer?

### 6. Melhorias futuras

O que ficou de fora e o que você faria em seguida. Reconhecer limitações conta a
seu favor.

### 7. Uso de IA

- Ferramenta utilizada
- Como foi utilizada
- Quais partes do código receberam auxílio

---

## Perguntas obrigatórias

Responda diretamente no README, em texto corrido. Não há tamanho mínimo — há
clareza mínima.

**1. Quais foram suas principais decisões técnicas?**

**2. O que faria diferente com mais tempo?**

**3. Qual foi a maior dificuldade encontrada?**

**4. Utilizou IA? Como?**

---

## Checklist final

Antes de enviar, confirme:

- [ ] A aplicação compila e executa a partir de um clone limpo
- [ ] `database/create_database.sql` está preenchido e funciona
- [ ] O README contém as sete seções acima
- [ ] As quatro perguntas obrigatórias estão respondidas
- [ ] O uso de IA está informado
- [ ] A funcionalidade adicional está documentada (motivo, valor, alternativas)
- [ ] `bin/`, `obj/`, `.vs/` e arquivos `.db` **não** estão versionados
- [ ] O histórico de commits mostra a evolução do trabalho

> **Dica:** clone o seu próprio fork em uma pasta nova e siga o seu README do zero.
> É o teste mais rápido para descobrir o que ficou faltando.
