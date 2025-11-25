namespace FitLifeAPI.Models
{
    /// <summary>
    /// Classe que representa um treino de cardio
    /// Demonstra HERANÇA - herda de Treino
    /// Demonstra POLIMORFISMO - implementa CalcularCalorias de forma específica
    /// </summary>
    public class TreinoCardio : Treino
    {
        public double DistanciaKm { get; set; }
        public int FrequenciaCardiacaMedia { get; set; }
        public string TipoCardio { get; set; } = string.Empty; // corrida, ciclismo, natacao, etc

        /// <summary>
        /// Implementação específica do cálculo de calorias para cardio
        /// Fórmula simplificada: duração * intensidade * fator de distância
        /// </summary>
        public override int CalcularCalorias()
        {
            int fatorIntensidade = Intensidade switch
            {
                "baixa" => 5,
                "moderada" => 8,
                "alta" => 12,
                _ => 8
            };

            // Calorias = duração * fator * (1 + distância/10)
            double calorias = DuracaoMinutos * fatorIntensidade * (1 + DistanciaKm / 10);
            return (int)calorias;
        }

        public override bool Validar()
        {
            return base.Validar() && 
                   DistanciaKm > 0 && 
                   !string.IsNullOrEmpty(TipoCardio);
        }
    }
}
