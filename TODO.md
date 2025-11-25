# FitLife API - TODO do Projeto

## Requisitos do Projeto Final (P2)
- [x] Backend com ASP.NET Core Web API
- [x] Uso de POO (herança, interfaces e listas)
- [x] Persistência de dados (arquivo JSON)
- [x] Swagger documentando os endpoints)
- [x] README detalhado com instruções
- [x] Pesquisa de mercado (1 página PDF)
- [x] UML de classes e fluxos principais

## Fase 1: Estrutura do Projeto
- [x] Criar projeto ASP.NET Core Web API
- [x] Configurar estrutura de pastas (Models, Controllers, Services, Data)
- [x] Adicionar pacotes necessários (Newtonsoft.Json)

## Fase 2: Models e POO
- [x] Criar interface IAtividade
- [x] Criar classe abstrata Treino
- [x] Criar classes derivadas: TreinoCardio, TreinoMusculacao, TreinoFuncional (Herança)
- [x] Criar classe Usuario
- [x] Criar classe Alimentacao
- [x] Criar classe Habito
- [x] Criar classe ProgressoUsuario

## Fase 3: Persistência de Dados
- [x] Criar classe DataContext para gerenciar JSON
- [x] Implementar métodos de leitura/escrita em arquivo JSON
- [x] Criar arquivo data.json inicial

## Fase 4: Controllers e Endpoints (CRUD)
- [x] TreinosController (GET, POST, PUT, DELETE)
- [x] AlimentacaoController (GET, POST, PUT, DELETE)
- [x] HabitosController (GET, POST, PUT, DELETE)
- [x] ProgressoController (GET)
- [x] RankingController (GET) - usar LINQ

## Fase 5: LINQ e Funcionalidades
- [x] Implementar filtros com LINQ (por data, tipo, intensidade)
- [x] Implementar ordenação com LINQ
- [x] Implementar cálculo de ranking com LINQ
- [x] Implementar estatísticas com LINQ

## Fase 6: Funcionalidade Inteligente (IA)
- [ ] Criar serviço de sugestões de treino
- [ ] Implementar análise de progresso
- [ ] Endpoint para sugestões personalizadas

## Fase 7: Documentação
- [x] Configurar Swagger
- [x] Criar README.md detalhado
- [x] Criar diagramas UML (classes e fluxos)
- [x] Criar pesquisa de mercado PDF)

## Fase 8: Testes e Finalização
- [ ] Testar todos os endpoints
- [ ] Validar POO (herança, polimorfismo, interfaces)
- [ ] Validar uso de LINQ
- [ ] Revisão final do código
