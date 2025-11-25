namespace FitLifeAPI.DTOs
{
    // ============================================
    // DTOs de Hábito
    // ============================================
    public class HabitoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Frequencia { get; set; } = string.Empty;
        public bool Completado { get; set; }
        public int SequenciaDias { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? CompletadoEm { get; set; }
    }

    public class CriarHabitoDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Frequencia { get; set; } = "Diário";
        public int UsuarioId { get; set; }
    }
}

    