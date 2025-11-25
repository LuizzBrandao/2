using System.Collections.Generic;

namespace FitLifeAPI.Models
{
    /// <summary>
    /// Classe que representa um treino de musculação
    /// Demonstra HERANÇA - herda de Treino
    /// Demonstra POLIMORFISMO - implementa CalcularCalorias de forma específica
    /// </summary>
    public class TreinoMusculacao : Treino
    {
        public int Series { get; set; }
        public int Repeticoes { get; set; }
        public double CargaKg { get; set; }
        public List<string> GruposMusculares { get; set; } = new List<string>();

        /// <summary>
        /// Implementação específica do cálculo de calorias para musculação
        /// Fórmula simplificada: duração * intensidade * fator de carga
        /// </summary>
        public override int CalcularCalorias()
        {
            int fatorIntensidade = Intensidade switch
            {
                "baixa" => 4,
                "moderada" => 6,
                "alta" => 9,
                _ => 6
            };

            // Fator de carga: quanto mais peso, mais calorias
            double fatorCarga = 1 + (CargaKg / 100);

            // Calorias = duração * fator * fator de carga
            double calorias = DuracaoMinutos * fatorIntensidade * fatorCarga;
            return (int)calorias;
        }

        /// <summary>
        /// Calcula o volume total do treino (séries x repetições x carga)
        /// </summary>
        public double CalcularVolume()
        {
            return Series * Repeticoes * CargaKg;
        }

        public override bool Validar()
        {
            return base.Validar() && 
                   Series > 0 && 
                   Repeticoes > 0 &&
                   GruposMusculares.Count > 0;
        }
    }
}
