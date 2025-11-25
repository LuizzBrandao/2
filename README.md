# FitLife API - Plataforma de Treinos e Hábitos Saudáveis

## 📋 Sobre o Projeto

**FitLife** é uma API REST desenvolvida em **ASP.NET Core** (C#) para gerenciar treinos, alimentação e hábitos saudáveis. O projeto foi desenvolvido como trabalho final da disciplina de Programação em C# e API, demonstrando conceitos de **Programação Orientada a Objetos (POO)**, **LINQ**, **persistência de dados** e **documentação com Swagger**.

## 🎯 Funcionalidades

### Gerenciamento de Treinos
- Criar, listar, atualizar e deletar treinos
- Três tipos de treino com **herança e polimorfismo**:
  - **Treino Cardio**: corrida, ciclismo, natação
  - **Treino Musculação**: séries, repetições, carga
  - **Treino Funcional**: HIIT, CrossFit, calistenia
- Cálculo automático de calorias (cada tipo calcula de forma diferente)
- Filtros por tipo, intensidade e usuário
- Estatísticas detalhadas

### Gerenciamento de Alimentação
- Registrar refeições com macronutrientes
- Calcular percentuais de proteínas, carboidratos e gorduras
- Filtros por tipo de refeição
- Estatísticas nutricionais

### Gerenciamento de Hábitos
- Criar e acompanhar hábitos saudáveis
- Registrar conclusões diárias
- Calcular sequência de dias consecutivos
- Taxa de conclusão
- Estatísticas por categoria

### Ranking
- Ranking geral de usuários por pontuação
- Ranking por tipo de treino
- Ranking por calorias queimadas
- Ranking de hábitos mais mantidos
- Uso intensivo de **LINQ**

## 🏗️ Arquitetura e Conceitos de POO

### Interface
```csharp
public interface IAtividade
{
    int CalcularCalorias();
    int ObterDuracao();
    bool Validar();
}
```

### Classe Abstrata (Herança)
```csharp
public abstract class Treino : IAtividade
{
    // Propriedades comuns
    public abstract int CalcularCalorias(); // Método abstrato
}
```

### Classes Derivadas (Polimorfismo)
```csharp
public class TreinoCardio : Treino
{
    public override int CalcularCalorias()
    {
        // Implementação específica para cardio
    }
}

public class TreinoMusculacao : Treino
{
    public override int CalcularCalorias()
    {
        // Implementação específica para musculação
    }
}

public class TreinoFuncional : Treino
{
    public override int CalcularCalorias()
    {
        // Implementação específica para funcional
    }
}
```

## 📊 Uso de LINQ

O projeto faz uso extensivo de LINQ para consultas e manipulação de dados:

### Exemplos de LINQ

**Filtrar e ordenar treinos:**
```csharp
var treinos = _context.Treinos
    .Where(t => t.UsuarioId == usuarioId)
    .OrderByDescending(t => t.Data)
    .ToList();
```

**Agrupar e calcular estatísticas:**
```csharp
var estatisticas = treinos
    .GroupBy(t => t.Intensidade)
    .Select(g => new { 
        Intensidade = g.Key, 
        Quantidade = g.Count() 
    })
    .ToList();
```

**Ranking com Join e OrderBy:**
```csharp
var ranking = _context.Treinos
    .Where(t => t.Status == "concluido")
    .GroupBy(t => t.UsuarioId)
    .Select(g => new {
        UsuarioId = g.Key,
        Pontuacao = g.Sum(t => t.CaloriasQueimadas) + (g.Count() * 100)
    })
    .OrderByDescending(r => r.Pontuacao)
    .ToList();
```

**Polimorfismo com OfType:**
```csharp
var treinosCardio = _context.Treinos
    .OfType<TreinoCardio>()
    .ToList();
```

## 🗂️ Estrutura do Projeto

```
FitLifeAPI/
├── Controllers/          # Controladores da API
│   ├── TreinosController.cs
│   ├── AlimentacaoController.cs
│   ├── HabitosController.cs
│   └── RankingController.cs
├── Models/              # Modelos de dados (POO)
│   ├── IAtividade.cs           # Interface
│   ├── Treino.cs               # Classe abstrata
│   ├── TreinoCardio.cs         # Herança
│   ├── TreinoMusculacao.cs     # Herança
│   ├── TreinoFuncional.cs      # Herança
│   ├── Usuario.cs
│   ├── Alimentacao.cs
│   └── Habito.cs
├── Data/                # Persistência de dados
│   ├── DataContext.cs          # Gerenciamento JSON
│   └── data.json               # Arquivo de dados
├── diagrams/            # Diagramas UML
│   ├── class_diagram.mmd
│   ├── flow_diagram.mmd
│   └── output/
│       ├── class_diagram.png
│       └── flow_diagram.png
├── Program.cs           # Configuração da aplicação
├── README.md            # Este arquivo
├── TODO.md              # Lista de tarefas
└── research_findings.md # Pesquisa de mercado

```

## 🚀 Como Executar

### Pré-requisitos
- .NET 8.0 SDK ou superior

### Passos

1. **Clone ou baixe o projeto**

2. **Navegue até a pasta do projeto**
```bash
cd FitLifeAPI
```

3. **Execute a aplicação**
```bash
dotnet run
```

4. **Acesse o Swagger**
Abra o navegador em: `http://localhost:5000`

## 📖 Documentação da API (Swagger)

A API possui documentação interativa completa através do **Swagger UI**, acessível em `http://localhost:5000` quando a aplicação está rodando.

### Principais Endpoints

#### Treinos
- `GET /api/treinos` - Lista todos os treinos
- `GET /api/treinos/{id}` - Busca treino por ID
- `GET /api/treinos/usuario/{usuarioId}` - Lista treinos de um usuário
- `GET /api/treinos/tipo/{tipo}` - Filtra por tipo (cardio, musculacao, funcional)
- `GET /api/treinos/intensidade/{intensidade}` - Filtra por intensidade
- `POST /api/treinos/cardio` - Cria treino de cardio
- `POST /api/treinos/musculacao` - Cria treino de musculação
- `POST /api/treinos/funcional` - Cria treino funcional
- `PUT /api/treinos/{id}/concluir` - Marca treino como concluído
- `PUT /api/treinos/{id}` - Atualiza treino
- `DELETE /api/treinos/{id}` - Deleta treino
- `GET /api/treinos/estatisticas/{usuarioId}` - Estatísticas de treinos

#### Alimentação
- `GET /api/alimentacao` - Lista todas as alimentações
- `GET /api/alimentacao/{id}` - Busca alimentação por ID
- `GET /api/alimentacao/usuario/{usuarioId}` - Lista alimentações de um usuário
- `POST /api/alimentacao` - Cria nova alimentação
- `PUT /api/alimentacao/{id}` - Atualiza alimentação
- `DELETE /api/alimentacao/{id}` - Deleta alimentação
- `GET /api/alimentacao/estatisticas/{usuarioId}` - Estatísticas nutricionais

#### Hábitos
- `GET /api/habitos` - Lista todos os hábitos
- `GET /api/habitos/{id}` - Busca hábito por ID
- `GET /api/habitos/usuario/{usuarioId}` - Lista hábitos de um usuário
- `GET /api/habitos/usuario/{usuarioId}/ativos` - Lista hábitos ativos
- `POST /api/habitos` - Cria novo hábito
- `POST /api/habitos/{id}/registrar` - Registra conclusão de hábito
- `PUT /api/habitos/{id}` - Atualiza hábito
- `DELETE /api/habitos/{id}` - Deleta hábito
- `GET /api/habitos/estatisticas/{usuarioId}` - Estatísticas de hábitos

#### Ranking
- `GET /api/ranking` - Ranking geral de usuários
- `GET /api/ranking/tipo/{tipo}` - Ranking por tipo de treino
- `GET /api/ranking/calorias` - Ranking por calorias
- `GET /api/ranking/usuario/{usuarioId}` - Posição de um usuário
- `GET /api/ranking/habitos` - Ranking de hábitos

## 💾 Persistência de Dados

O projeto utiliza **arquivo JSON** para persistência de dados, tornando-o simples e didático. Os dados são salvos em `Data/data.json`.

### Estrutura do JSON
```json
{
  "Usuarios": [],
  "Treinos": [],
  "Alimentacoes": [],
  "Habitos": []
}
```

## 🎨 Diagramas UML

Os diagramas UML estão disponíveis na pasta `diagrams/`:

- **Diagrama de Classes** (`class_diagram.png`): Mostra a estrutura de classes, herança e relacionamentos
- **Diagrama de Fluxos** (`flow_diagram.png`): Mostra os fluxos principais do sistema

## 📚 Conceitos Demonstrados

### Programação Orientada a Objetos (POO)
✅ **Interface**: `IAtividade`  
✅ **Classe Abstrata**: `Treino`  
✅ **Herança**: `TreinoCardio`, `TreinoMusculacao`, `TreinoFuncional`  
✅ **Polimorfismo**: Método `CalcularCalorias()` implementado diferentemente em cada classe  
✅ **Encapsulamento**: Propriedades e métodos bem definidos  

### LINQ (Language Integrated Query)
✅ **Where**: Filtros  
✅ **OrderBy/OrderByDescending**: Ordenação  
✅ **GroupBy**: Agrupamento  
✅ **Select**: Projeção  
✅ **Sum/Average/Count**: Agregações  
✅ **OfType**: Filtro por tipo (polimorfismo)  
✅ **Join**: Junção de dados  

### API REST
✅ **GET**: Consultar dados  
✅ **POST**: Criar dados  
✅ **PUT**: Atualizar dados  
✅ **DELETE**: Deletar dados  
✅ **Códigos HTTP**: 200 OK, 201 Created, 404 Not Found, 400 Bad Request  

### Documentação
✅ **Swagger**: Documentação interativa automática  
✅ **Comentários XML**: Documentação inline no código  
✅ **README**: Documentação completa do projeto  

## 🔍 Pesquisa de Mercado

A pesquisa de mercado sobre apps fitness com IA está disponível no arquivo `research_findings.md` e inclui análise de:
- **Fitbod**: Progressive overload e machine learning
- **Freeletics**: IA com 56M+ usuários e personalização
- **FitnessAI**: Performance intelligence

### Funcionalidade Inovadora Proposta
**Smart Training Assistant**: Assistente baseado em IA que analisa histórico de treinos, alimentação e hábitos para fornecer recomendações personalizadas.

## 👨‍💻 Autor

Projeto desenvolvido como trabalho final da disciplina de Programação em C# e API.

## 📄 Licença

Este projeto é de código aberto e está disponível para fins educacionais.

---

**FitLife API** - Transformando hábitos em resultados! 💪
