using System;

namespace FitLifeAPI.Models
{
    /// <summary>
    /// Classe que representa um registro de alimentação
    /// </summary>
    public class Alimentacao
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime Data { get; set; }
        public string Refeicao { get; set; } = string.Empty; // cafe_manha, almoco, jantar, lanche
        public string Descricao { get; set; } = string.Empty;
        public int Calorias { get; set; }
        public double Proteinas { get; set; } // em gramas
        public double Carboidratos { get; set; } // em gramas
        public double Gorduras { get; set; } // em gramas

        /// <summary>
        /// Calcula o percentual de proteínas em relação ao total de calorias
        /// </summary>
        public double CalcularPercentualProteinas()
        {
            if (Calorias == 0) return 0;
            // 1g de proteína = 4 calorias
            return Math.Round((Proteinas * 4 / Calorias) * 100, 2);
        }

        /// <summary>
        /// Calcula o percentual de carboidratos em relação ao total de calorias
        /// </summary>
        public double CalcularPercentualCarboidratos()
        {
            if (Calorias == 0) return 0;
            // 1g de carboidrato = 4 calorias
            return Math.Round((Carboidratos * 4 / Calorias) * 100, 2);
        }

        /// <summary>
        /// Calcula o percentual de gorduras em relação ao total de calorias
        /// </summary>
        public double CalcularPercentualGorduras()
        {
            if (Calorias == 0) return 0;
            // 1g de gordura = 9 calorias
            return Math.Round((Gorduras * 9 / Calorias) * 100, 2);
        }

        /// <summary>
        /// Valida se os dados da alimentação estão corretos
        /// </summary>
        public bool Validar()
        {
            return !string.IsNullOrEmpty(Descricao) && 
                   Calorias > 0 && 
                   UsuarioId > 0;
        }
    }
}
