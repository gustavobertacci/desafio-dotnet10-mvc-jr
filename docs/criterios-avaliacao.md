# Critérios de Avaliação

## Pesos

```text
Funcionalidades:      25%
MVC/Razor:            25%
Banco de Dados:       15%
Qualidade do Código:  15%
Git:                  10%
Documentação:         10%
```

---

## O que olhamos em cada critério

### Funcionalidades — 25%

- As cinco operações do CRUD funcionam de ponta a ponta
- As validações (nome obrigatório, 3–100 caracteres, preço > 0) são aplicadas no servidor
- A busca por nome retorna resultados parciais corretamente
- As ordenações por nome e por preço funcionam nos dois sentidos
- Busca e ordenação funcionam combinadas
- A funcionalidade adicional (diferencial) está implementada e funcional

### MVC / Razor — 25%

- Controllers finos: sem regra de negócio ou SQL espalhado nas actions
- Uso correto dos verbos HTTP (`GET` para exibir, `POST` para alterar estado)
- Views usam tag helpers, model binding e `asp-validation-*` em vez de HTML manual
- Reaproveitamento por partials ou layout quando faz sentido
- Nada de lógica de acesso a dados dentro das views

### Banco de Dados — 15%

- Modelagem adequada dos tipos (especialmente `decimal` para preço)
- Script `create_database.sql` presente, correto e executável
- Consultas legíveis e sem risco de SQL Injection (parâmetros, nunca concatenação)
- Coerência entre o schema e o model da aplicação

### Qualidade do Código — 15%

- Nomes claros em português ou inglês, mas consistentes
- Sem código morto, comentários obsoletos ou arquivos não utilizados
- Tratamento de erros onde é razoável (registro inexistente, entrada inválida)
- Ausência de duplicação evidente
- Complexidade proporcional ao problema — abstração sem uso conta contra

### Git — 10%

- Commits incrementais, não um único "primeiro commit" com tudo
- Mensagens que descrevem a intenção, não o arquivo alterado
- Ausência de `bin/`, `obj/`, `.vs/` e arquivos de banco versionados

### Documentação — 10%

- README explica como executar sem que o avaliador precise adivinhar
- Decisões técnicas justificadas, não apenas listadas
- As quatro perguntas obrigatórias de `entrega.md` respondidas
- Uso de IA declarado com clareza

---

## Sinais positivos

Não valem pontos extras na tabela, mas pesam na recomendação final:

- Reconhecer uma limitação da própria entrega antes de sermos nós a apontá-la
- Justificar uma decisão simples em vez de adotar um padrão por inércia
- Mensagens de erro pensadas para quem usa o sistema
- README que um colega novo conseguiria seguir sozinho

## Sinais de alerta

- Código que o candidato não consegue explicar
- Arquitetura elaborada sem propósito no contexto do desafio
- Concatenação de strings para montar SQL
- Divergência entre o que o README afirma e o que o código faz
- Aplicação que não compila ou não sobe
