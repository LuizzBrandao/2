using System.ComponentModel.DataAnnotations;

namespace FitLifeAPI.DTOs
{
    public class RefeicaoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int Calorias { get; set; }
        public double ProteinaGramas { get; set; }
        public double CarboidratosGramas { get; set; }
        public double GordurasGramas { get; set; }
        public DateTime HorarioRefeicao { get; set; }
        public string TipoRefeicao { get; set; } = string.Empty;
        public bool EstaBalanceada { get; set; }
    }

    public class CriarRefeicaoDTO
    {
        [Required]
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int Calorias { get; set; }
        public double ProteinaGramas { get; set; }
        public double CarboidratosGramas { get; set; }
        public double GordurasGramas { get; set; }
        public string TipoRefeicao { get; set; } = "Almoço";
        [Required]
        public int UsuarioId { get; set; }
    }

    // DTO used for updating a refeição. Does not allow changing UsuarioId or HorarioRefeicao.
    public class AtualizarRefeicaoDTO
    {
        [Required]
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int Calorias { get; set; }
        public double ProteinaGramas { get; set; }
        public double CarboidratosGramas { get; set; }
        public double GordurasGramas { get; set; }
        public string TipoRefeicao { get; set; } = "Almoço";
    }
}