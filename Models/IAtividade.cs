namespace FitLifeAPI.Models
{
    /// <summary>
    /// Interface que define o contrato para atividades físicas
    /// Demonstra o conceito de INTERFACE em POO
    /// </summary>
    public interface IAtividade
    {
        /// <summary>
        /// Calcula as calorias queimadas na atividade
        /// </summary>
        int CalcularCalorias();

        /// <summary>
        /// Obtém a duração da atividade em minutos
        /// </summary>
        int ObterDuracao();

        /// <summary>
        /// Valida se a atividade está com dados corretos
        /// </summary>
        bool Validar();
    }
}
