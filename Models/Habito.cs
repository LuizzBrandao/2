using System;
using System.Collections.Generic;
using System.Linq;

namespace FitLifeAPI.Models
{
    /// <summary>
    /// Classe que representa um hábito saudável
    /// </summary>
    public class Habito
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty; // saude, exercicio, alimentacao, sono, hidratacao
        public string Frequencia { get; set; } = "diaria"; // diaria, semanal, mensal
        public DateTime DataInicio { get; set; }
        public bool Ativo { get; set; } = true;
        public List<RegistroHabito> Registros { get; set; } = new List<RegistroHabito>();

        /// <summary>
        /// Registra a conclusão do hábito para uma data
        /// </summary>
        public void RegistrarConclusao(DateTime data, bool concluido = true)
        {
            var registro = new RegistroHabito
            {
                HabitoId = Id,
                Data = data,
                Concluido = concluido
            };
            Registros.Add(registro);
        }

        /// <summary>
        /// Calcula a sequência atual de dias consecutivos (usa LINQ)
        /// </summary>
        public int CalcularSequencia()
        {
            if (!Registros.Any()) return 0;

            var registrosOrdenados = Registros
                .Where(r => r.Concluido)
                .OrderByDescending(r => r.Data)
                .ToList();

            if (!registrosOrdenados.Any()) return 0;

            int sequencia = 1;
            DateTime dataAnterior = registrosOrdenados[0].Data.Date;

            for (int i = 1; i < registrosOrdenados.Count; i++)
            {
                DateTime dataAtual = registrosOrdenados[i].Data.Date;
                if ((dataAnterior - dataAtual).Days == 1)
                {
                    sequencia++;
                    dataAnterior = dataAtual;
                }
                else
                {
                    break;
                }
            }

            return sequencia;
        }

        /// <summary>
        /// Calcula a taxa de conclusão do hábito (usa LINQ)
        /// </summary>
        public double CalcularTaxaConclusao()
        {
            if (!Registros.Any()) return 0;

            int totalRegistros = Registros.Count;
            int registrosConcluidos = Registros.Count(r => r.Concluido);

            return Math.Round((double)registrosConcluidos / totalRegistros * 100, 2);
        }
    }

    /// <summary>
    /// Classe que representa um registro de conclusão de hábito
    /// </summary>
    public class RegistroHabito
    {
        public int Id { get; set; }
        public int HabitoId { get; set; }
        public DateTime Data { get; set; }
        public bool Concluido { get; set; }
        public string? Observacoes { get; set; }
    }
}
