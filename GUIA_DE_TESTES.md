# Guia de Testes - FitLife API

Este guia contém exemplos de requisições para testar todos os endpoints da API.

## Como Testar

1. Execute a aplicação: `dotnet run`
2. Acesse o Swagger: `http://localhost:5000`
3. Use os exemplos abaixo para testar cada endpoint

## 1. Treinos

### 1.1 Criar Treino de Cardio
**Endpoint:** `POST /api/treinos/cardio`

**Body (JSON):**
```json
{
  "usuarioId": 1,
  "titulo": "Corrida Matinal",
  "descricao": "Corrida leve no parque",
  "duracaoMinutos": 30,
  "intensidade": "moderada",
  "distanciaKm": 5.0,
  "frequenciaCardiacaMedia": 145,
  "tipoCardio": "corrida",
  "exercicios": ["Aquecimento", "Corrida", "Alongamento"]
}
```

### 1.2 Criar Treino de Musculação
**Endpoint:** `POST /api/treinos/musculacao`

**Body (JSON):**
```json
{
  "usuarioId": 1,
  "titulo": "Treino de Peito e Tríceps",
  "descricao": "Treino focado em peito e tríceps",
  "duracaoMinutos": 60,
  "intensidade": "alta",
  "series": 4,
  "repeticoes": 12,
  "cargaKg": 50.0,
  "gruposMusculares": ["Peitoral", "Tríceps"],
  "exercicios": ["Supino", "Crucifixo", "Tríceps Testa", "Tríceps Corda"]
}
```

### 1.3 Criar Treino Funcional
**Endpoint:** `POST /api/treinos/funcional`

**Body (JSON):**
```json
{
  "usuarioId": 1,
  "titulo": "HIIT Intenso",
  "descricao": "Treino intervalado de alta intensidade",
  "duracaoMinutos": 45,
  "intensidade": "alta",
  "tipoFuncional": "HIIT",
  "numeroExercicios": 8,
  "usaEquipamento": true,
  "exercicios": ["Burpees", "Jump Squats", "Mountain Climbers", "Kettlebell Swings"]
}
```

### 1.4 Listar Todos os Treinos
**Endpoint:** `GET /api/treinos`

### 1.5 Buscar Treino por ID
**Endpoint:** `GET /api/treinos/1`

### 1.6 Listar Treinos por Usuário
**Endpoint:** `GET /api/treinos/usuario/1`

### 1.7 Filtrar por Tipo (Demonstra Polimorfismo)
**Endpoint:** `GET /api/treinos/tipo/cardio`  
**Endpoint:** `GET /api/treinos/tipo/musculacao`  
**Endpoint:** `GET /api/treinos/tipo/funcional`

### 1.8 Filtrar por Intensidade (Demonstra LINQ)
**Endpoint:** `GET /api/treinos/intensidade/alta`

### 1.9 Marcar Treino como Concluído (Demonstra Polimorfismo)
**Endpoint:** `PUT /api/treinos/1/concluir`

*Este endpoint demonstra polimorfismo: cada tipo de treino calcula calorias de forma diferente!*

### 1.10 Obter Estatísticas (Demonstra LINQ Avançado)
**Endpoint:** `GET /api/treinos/estatisticas/1`

### 1.11 Deletar Treino
**Endpoint:** `DELETE /api/treinos/1`

## 2. Alimentação

### 2.1 Criar Alimentação
**Endpoint:** `POST /api/alimentacao`

**Body (JSON):**
```json
{
  "usuarioId": 1,
  "refeicao": "cafe_manha",
  "descricao": "Ovos mexidos com pão integral e frutas",
  "calorias": 450,
  "proteinas": 25.0,
  "carboidratos": 50.0,
  "gorduras": 15.0
}
```

### 2.2 Listar Alimentações por Usuário
**Endpoint:** `GET /api/alimentacao/usuario/1`

### 2.3 Filtrar por Refeição (Demonstra LINQ)
**Endpoint:** `GET /api/alimentacao/refeicao/almoco`

### 2.4 Obter Estatísticas Nutricionais (Demonstra LINQ)
**Endpoint:** `GET /api/alimentacao/estatisticas/1`

## 3. Hábitos

### 3.1 Criar Hábito
**Endpoint:** `POST /api/habitos`

**Body (JSON):**
```json
{
  "usuarioId": 1,
  "titulo": "Beber 2L de água por dia",
  "descricao": "Manter hidratação adequada",
  "categoria": "hidratacao",
  "frequencia": "diaria"
}
```

### 3.2 Listar Hábitos Ativos (Demonstra LINQ)
**Endpoint:** `GET /api/habitos/usuario/1/ativos`

### 3.3 Registrar Conclusão de Hábito
**Endpoint:** `POST /api/habitos/1/registrar?concluido=true`

### 3.4 Obter Estatísticas de Hábitos (Demonstra LINQ)
**Endpoint:** `GET /api/habitos/estatisticas/1`

## 4. Ranking

### 4.1 Ranking Geral (Demonstra LINQ: Join, GroupBy, OrderBy)
**Endpoint:** `GET /api/ranking?limite=10`

### 4.2 Ranking por Tipo de Treino (Demonstra LINQ + Polimorfismo)
**Endpoint:** `GET /api/ranking/tipo/cardio`  
**Endpoint:** `GET /api/ranking/tipo/musculacao`

### 4.3 Ranking por Calorias (Demonstra LINQ: Sum, Average)
**Endpoint:** `GET /api/ranking/calorias?limite=10`

### 4.4 Posição de um Usuário (Demonstra LINQ: FindIndex)
**Endpoint:** `GET /api/ranking/usuario/1`

### 4.5 Ranking de Hábitos (Demonstra LINQ Avançado)
**Endpoint:** `GET /api/ranking/habitos?limite=10`

## Conceitos Demonstrados

### POO (Programação Orientada a Objetos)
✅ **Interface**: `IAtividade` define contrato para atividades  
✅ **Classe Abstrata**: `Treino` é base para todos os treinos  
✅ **Herança**: `TreinoCardio`, `TreinoMusculacao`, `TreinoFuncional` herdam de `Treino`  
✅ **Polimorfismo**: Método `CalcularCalorias()` implementado diferentemente em cada classe  

**Teste Polimorfismo:**
1. Crie um treino de cada tipo (cardio, musculação, funcional)
2. Marque todos como concluídos (`PUT /api/treinos/{id}/concluir`)
3. Observe que cada um calcula calorias de forma diferente!

### LINQ (Language Integrated Query)
✅ **Where**: Filtros (por tipo, intensidade, usuário)  
✅ **OrderBy**: Ordenação (por data, pontuação)  
✅ **GroupBy**: Agrupamento (estatísticas por intensidade, categoria)  
✅ **Select**: Projeção (transformação de dados)  
✅ **Sum/Average/Count**: Agregações (total de calorias, média)  
✅ **OfType**: Filtro por tipo (polimorfismo com LINQ)  

**Teste LINQ:**
1. Crie vários treinos de tipos diferentes
2. Use os endpoints de filtro e estatísticas
3. Observe como LINQ facilita consultas complexas!

### API REST
✅ **GET**: Consultar dados  
✅ **POST**: Criar novos registros  
✅ **PUT**: Atualizar registros  
✅ **DELETE**: Deletar registros  

## Fluxo de Teste Completo

### Cenário: Usuário completa um dia de treino

1. **Criar treino de cardio** (manhã)
```
POST /api/treinos/cardio
```

2. **Registrar café da manhã**
```
POST /api/alimentacao
```

3. **Criar hábito de hidratação**
```
POST /api/habitos
```

4. **Registrar conclusão do hábito**
```
POST /api/habitos/1/registrar
```

5. **Criar treino de musculação** (tarde)
```
POST /api/treinos/musculacao
```

6. **Marcar treinos como concluídos**
```
PUT /api/treinos/1/concluir
PUT /api/treinos/2/concluir
```

7. **Ver estatísticas do dia**
```
GET /api/treinos/estatisticas/1
GET /api/alimentacao/estatisticas/1
GET /api/habitos/estatisticas/1
```

8. **Verificar posição no ranking**
```
GET /api/ranking/usuario/1
```

## Validação dos Requisitos

### ✅ POO
- Interface implementada: `IAtividade`
- Classe abstrata: `Treino`
- Herança: 3 classes derivadas
- Polimorfismo: `CalcularCalorias()` diferente em cada classe

### ✅ LINQ
- Filtros: `Where`
- Ordenação: `OrderBy`, `OrderByDescending`
- Agrupamento: `GroupBy`
- Agregações: `Sum`, `Average`, `Count`
- Projeção: `Select`
- Polimorfismo: `OfType`

### ✅ Persistência
- Arquivo JSON: `Data/data.json`
- CRUD completo em todos os controllers

### ✅ Swagger
- Documentação automática: `http://localhost:5000`
- Comentários XML incluídos

### ✅ README
- Documentação completa do projeto

### ✅ UML
- Diagrama de classes
- Diagrama de fluxos

### ✅ Pesquisa de Mercado
- PDF com análise de apps fitness com IA

---

**Dica:** Use o Swagger UI para testar de forma interativa! Acesse `http://localhost:5000` e explore todos os endpoints.
