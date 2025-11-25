using Microsoft.AspNetCore.Mvc;
using FitLifeAPI.Data;
using FitLifeAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace FitLifeAPI.Controllers
{
    /// <summary>
    /// Controller para gerenciar ranking de usuários
    /// Demonstra uso intensivo de LINQ
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RankingController : ControllerBase
    {
        private readonly DataContext _context;

        public RankingController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém o ranking geral de usuários (GET)
        /// Demonstra LINQ: Join, GroupBy, OrderBy, Select
        /// </summary>
        [HttpGet]
        public ActionResult GetRanking([FromQuery] int limite = 10)
        {
            // Usa LINQ para calcular pontuação de cada usuário
            var ranking = _context.Treinos
                .Where(t => t.Status == "concluido")
                .GroupBy(t => t.UsuarioId)
                .Select(g => new
                {
                    UsuarioId = g.Key,
                    TotalTreinos = g.Count(),
                    TotalCalorias = g.Sum(t => t.CaloriasQueimadas),
                    TotalMinutos = g.Sum(t => t.DuracaoMinutos),
                    // Pontuação = calorias + (treinos * 100)
                    Pontuacao = g.Sum(t => t.CaloriasQueimadas) + (g.Count() * 100)
                })
                .OrderByDescending(r => r.Pontuacao)
                .Take(limite)
                .Select((r, index) => new
                {
                    Posicao = index + 1,
                    r.UsuarioId,
                    NomeUsuario = _context.Usuarios
                        .FirstOrDefault(u => u.Id == r.UsuarioId)?.Nome ?? "Usuário Desconhecido",
                    r.TotalTreinos,
                    r.TotalCalorias,
                    r.TotalMinutos,
                    r.Pontuacao
                })
                .ToList();

            return Ok(ranking);
        }

        /// <summary>
        /// Obtém ranking por tipo de treino (GET)
        /// Demonstra LINQ com OfType (polimorfismo)
        /// </summary>
        [HttpGet("tipo/{tipo}")]
        public ActionResult GetRankingPorTipo(string tipo, [FromQuery] int limite = 10)
        {
            IEnumerable<Treino> treinosFiltrados = tipo.ToLower() switch
            {
                "cardio" => _context.Treinos.OfType<TreinoCardio>(),
                "musculacao" => _context.Treinos.OfType<TreinoMusculacao>(),
                "funcional" => _context.Treinos.OfType<TreinoFuncional>(),
                _ => _context.Treinos
            };

            var ranking = treinosFiltrados
                .Where(t => t.Status == "concluido")
                .GroupBy(t => t.UsuarioId)
                .Select(g => new
                {
                    UsuarioId = g.Key,
                    TotalTreinos = g.Count(),
                    TotalCalorias = g.Sum(t => t.CaloriasQueimadas),
                    Pontuacao = g.Sum(t => t.CaloriasQueimadas) + (g.Count() * 100)
                })
                .OrderByDescending(r => r.Pontuacao)
                .Take(limite)
                .Select((r, index) => new
                {
                    Posicao = index + 1,
                    r.UsuarioId,
                    NomeUsuario = _context.Usuarios
                        .FirstOrDefault(u => u.Id == r.UsuarioId)?.Nome ?? "Usuário Desconhecido",
                    TipoTreino = tipo,
                    r.TotalTreinos,
                    r.TotalCalorias,
                    r.Pontuacao
                })
                .ToList();

            return Ok(ranking);
        }

        /// <summary>
        /// Obtém ranking por calorias queimadas (GET)
        /// Demonstra LINQ: OrderBy, Select
        /// </summary>
        [HttpGet("calorias")]
        public ActionResult GetRankingPorCalorias([FromQuery] int limite = 10)
        {
            var ranking = _context.Treinos
                .Where(t => t.Status == "concluido")
                .GroupBy(t => t.UsuarioId)
                .Select(g => new
                {
                    UsuarioId = g.Key,
                    TotalCalorias = g.Sum(t => t.CaloriasQueimadas),
                    MediaCalorias = g.Average(t => t.CaloriasQueimadas)
                })
                .OrderByDescending(r => r.TotalCalorias)
                .Take(limite)
                .Select((r, index) => new
                {
                    Posicao = index + 1,
                    r.UsuarioId,
                    NomeUsuario = _context.Usuarios
                        .FirstOrDefault(u => u.Id == r.UsuarioId)?.Nome ?? "Usuário Desconhecido",
                    r.TotalCalorias,
                    MediaCalorias = (int)r.MediaCalorias
                })
                .ToList();

            return Ok(ranking);
        }

        /// <summary>
        /// Obtém posição de um usuário específico no ranking (GET)
        /// Demonstra LINQ: FindIndex
        /// </summary>
        [HttpGet("usuario/{usuarioId}")]
        public ActionResult GetPosicaoUsuario(int usuarioId)
        {
            var rankingCompleto = _context.Treinos
                .Where(t => t.Status == "concluido")
                .GroupBy(t => t.UsuarioId)
                .Select(g => new
                {
                    UsuarioId = g.Key,
                    TotalTreinos = g.Count(),
                    TotalCalorias = g.Sum(t => t.CaloriasQueimadas),
                    Pontuacao = g.Sum(t => t.CaloriasQueimadas) + (g.Count() * 100)
                })
                .OrderByDescending(r => r.Pontuacao)
                .ToList();

            var posicao = rankingCompleto.FindIndex(r => r.UsuarioId == usuarioId);

            if (posicao == -1)
                return NotFound(new { mensagem = "Usuário não encontrado no ranking" });

            var usuario = rankingCompleto[posicao];

            return Ok(new
            {
                Posicao = posicao + 1,
                TotalUsuarios = rankingCompleto.Count,
                usuario.UsuarioId,
                NomeUsuario = _context.Usuarios
                    .FirstOrDefault(u => u.Id == usuarioId)?.Nome ?? "Usuário Desconhecido",
                usuario.TotalTreinos,
                usuario.TotalCalorias,
                usuario.Pontuacao
            });
        }

        /// <summary>
        /// Obtém ranking de hábitos mais mantidos (GET)
        /// Demonstra LINQ avançado
        /// </summary>
        [HttpGet("habitos")]
        public ActionResult GetRankingHabitos([FromQuery] int limite = 10)
        {
            var ranking = _context.Habitos
                .Where(h => h.Ativo)
                .Select(h => new
                {
                    h.UsuarioId,
                    h.Titulo,
                    Sequencia = h.CalcularSequencia(),
                    TaxaConclusao = h.CalcularTaxaConclusao()
                })
                .OrderByDescending(h => h.Sequencia)
                .ThenByDescending(h => h.TaxaConclusao)
                .Take(limite)
                .Select((h, index) => new
                {
                    Posicao = index + 1,
                    h.UsuarioId,
                    NomeUsuario = _context.Usuarios
                        .FirstOrDefault(u => u.Id == h.UsuarioId)?.Nome ?? "Usuário Desconhecido",
                    h.Titulo,
                    h.Sequencia,
                    TaxaConclusao = $"{h.TaxaConclusao:F2}%"
                })
                .ToList();

            return Ok(ranking);
        }
    }
}
