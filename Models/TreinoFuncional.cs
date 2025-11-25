namespace FitLifeAPI.Models
{
    /// <summary>
    /// Classe que representa um treino funcional
    /// Demonstra HERANÇA - herda de Treino
    /// Demonstra POLIMORFISMO - implementa CalcularCalorias de forma específica
    /// </summary>
    public class TreinoFuncional : Treino
    {
        public string TipoFuncional { get; set; } = string.Empty; // HIIT, CrossFit, Calistenia, etc
        public int NumeroExercicios { get; set; }
        public bool UsaEquipamento { get; set; }

        /// <summary>
        /// Implementação específica do cálculo de calorias para treino funcional
        /// Fórmula simplificada: duração * intensidade * fator de exercícios
        /// </summary>
        public override int CalcularCalorias()
        {
            int fatorIntensidade = Intensidade switch
            {
                "baixa" => 6,
                "moderada" => 10,
                "alta" => 15,
                _ => 10
            };

            // Fator de exercícios: quanto mais exercícios, mais calorias
            double fatorExercicios = 1 + (NumeroExercicios * 0.1);

            // Calorias = duração * fator * fator de exercícios
            double calorias = DuracaoMinutos * fatorIntensidade * fatorExercicios;
            return (int)calorias;
        }

        public override bool Validar()
        {
            return base.Validar() && 
                   NumeroExercicios > 0 && 
                   !string.IsNullOrEmpty(TipoFuncional);
        }
    }
}
