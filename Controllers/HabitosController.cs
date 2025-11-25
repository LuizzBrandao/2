using Microsoft.AspNetCore.Mvc;
using FitLifeAPI.Data;
using FitLifeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FitLifeAPI.Controllers
{
    /// <summary>
    /// Controller para gerenciar hábitos
    /// Demonstra uso de CRUD e LINQ
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class HabitosController : ControllerBase
    {
        private readonly DataContext _context;

        public HabitosController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém todos os hábitos (GET)
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Habito>> GetHabitos()
        {
            return Ok(_context.Habitos);
        }

        /// <summary>
        /// Obtém um hábito específico por ID (GET)
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<Habito> GetHabito(int id)
        {
            var habito = _context.Habitos.FirstOrDefault(h => h.Id == id);
            
            if (habito == null)
                return NotFound(new { mensagem = "Hábito não encontrado" });

            return Ok(habito);
        }

        /// <summary>
        /// Obtém hábitos por usuário (GET) - Demonstra LINQ
        /// </summary>
        [HttpGet("usuario/{usuarioId}")]
        public ActionResult<IEnumerable<Habito>> GetHabitosPorUsuario(int usuarioId)
        {
            var habitos = _context.Habitos
                .Where(h => h.UsuarioId == usuarioId)
                .OrderByDescending(h => h.DataInicio)
                .ToList();

            return Ok(habitos);
        }

        /// <summary>
        /// Obtém hábitos ativos (GET) - Demonstra LINQ
        /// </summary>
        [HttpGet("usuario/{usuarioId}/ativos")]
        public ActionResult<IEnumerable<Habito>> GetHabitosAtivos(int usuarioId)
        {
            var habitos = _context.Habitos
                .Where(h => h.UsuarioId == usuarioId && h.Ativo)
                .ToList();

            return Ok(habitos);
        }

        /// <summary>
        /// Cria um novo hábito (POST)
        /// </summary>
        [HttpPost]
        public ActionResult<Habito> CreateHabito([FromBody] Habito habito)
        {
            if (string.IsNullOrEmpty(habito.Titulo))
                return BadRequest(new { mensagem = "Título do hábito é obrigatório" });

            habito.Id = _context.Habitos.Any() ? _context.Habitos.Max(h => h.Id) + 1 : 1;
            habito.DataInicio = DateTime.Now;
            habito.Ativo = true;

            _context.Habitos.Add(habito);
            _context.SalvarDados();

            return CreatedAtAction(nameof(GetHabito), new { id = habito.Id }, habito);
        }

        /// <summary>
        /// Registra conclusão de um hábito (POST)
        /// </summary>
        [HttpPost("{id}/registrar")]
        public ActionResult RegistrarConclusao(int id, [FromQuery] bool concluido = true)
        {
            var habito = _context.Habitos.FirstOrDefault(h => h.Id == id);
            
            if (habito == null)
                return NotFound(new { mensagem = "Hábito não encontrado" });

            habito.RegistrarConclusao(DateTime.Now, concluido);
            _context.SalvarDados();

            return Ok(new 
            { 
                mensagem = "Conclusão registrada com sucesso",
                sequencia = habito.CalcularSequencia(),
                taxaConclusao = habito.CalcularTaxaConclusao()
            });
        }

        /// <summary>
        /// Atualiza um hábito (PUT)
        /// </summary>
        [HttpPut("{id}")]
        public ActionResult<Habito> UpdateHabito(int id, [FromBody] Habito habitoAtualizado)
        {
            var habito = _context.Habitos.FirstOrDefault(h => h.Id == id);
            
            if (habito == null)
                return NotFound(new { mensagem = "Hábito não encontrado" });

            habito.Titulo = habitoAtualizado.Titulo;
            habito.Descricao = habitoAtualizado.Descricao;
            habito.Categoria = habitoAtualizado.Categoria;
            habito.Frequencia = habitoAtualizado.Frequencia;
            habito.Ativo = habitoAtualizado.Ativo;

            _context.SalvarDados();

            return Ok(habito);
        }

        /// <summary>
        /// Deleta um hábito (DELETE)
        /// </summary>
        [HttpDelete("{id}")]
        public ActionResult DeleteHabito(int id)
        {
            var habito = _context.Habitos.FirstOrDefault(h => h.Id == id);
            
            if (habito == null)
                return NotFound(new { mensagem = "Hábito não encontrado" });

            _context.Habitos.Remove(habito);
            _context.SalvarDados();

            return Ok(new { mensagem = "Hábito deletado com sucesso" });
        }

        /// <summary>
        /// Obtém estatísticas de hábitos (GET) - Demonstra LINQ avançado
        /// </summary>
        [HttpGet("estatisticas/{usuarioId}")]
        public ActionResult GetEstatisticas(int usuarioId)
        {
            var habitos = _context.Habitos
                .Where(h => h.UsuarioId == usuarioId)
                .ToList();

            if (!habitos.Any())
                return Ok(new { mensagem = "Nenhum hábito encontrado" });

            var estatisticas = new
            {
                TotalHabitos = habitos.Count,
                HabitosAtivos = habitos.Count(h => h.Ativo),
                HabitosInativos = habitos.Count(h => !h.Ativo),
                HabitosPorCategoria = habitos
                    .GroupBy(h => h.Categoria)
                    .Select(g => new { Categoria = g.Key, Quantidade = g.Count() })
                    .ToList(),
                MelhorSequencia = habitos.Any() ? habitos.Max(h => h.CalcularSequencia()) : 0,
                MediaTaxaConclusao = habitos.Any() ? habitos.Average(h => h.CalcularTaxaConclusao()) : 0
            };

            return Ok(estatisticas);
        }
    }
}
