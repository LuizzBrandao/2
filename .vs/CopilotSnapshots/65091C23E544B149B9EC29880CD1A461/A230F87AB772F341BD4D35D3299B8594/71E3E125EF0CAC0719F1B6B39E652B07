using System.ComponentModel.DataAnnotations;

namespace FitLifeAPI.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Idade { get; set; }
        public double Peso { get; set; }
        public double Altura { get; set; }
        public string Objetivo { get; set; } = string.Empty;
        public double IMC { get; set; }
        public int TotalTreinos { get; set; }
    }

    public class CriarUsuarioDTO
    {
        [Required]
        public string Nome { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public int Idade { get; set; }
        public double Peso { get; set; }
        public double Altura { get; set; }
        public string Objetivo { get; set; } = "Manter Peso";
    }

    // DTO for updating a user
    public class AtualizarUsuarioDTO
    {
        [Required]
        public string Nome { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public int Idade { get; set; }
        public double Peso { get; set; }
        public double Altura { get; set; }
        public string Objetivo { get; set; } = "Manter Peso";
    }
}