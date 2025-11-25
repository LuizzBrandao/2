using Microsoft.AspNetCore.Mvc;
using FitLifeAPI.Data;
using FitLifeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FitLifeAPI.Controllers
{
    /// <summary>
    /// Controller para gerenciar alimentação
    /// Demonstra uso de CRUD e LINQ
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AlimentacaoController : ControllerBase
    {
        private readonly DataContext _context;

        public AlimentacaoController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém todas as alimentações (GET)
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Alimentacao>> GetAlimentacoes()
        {
            return Ok(_context.Alimentacoes);
        }

        /// <summary>
        /// Obtém uma alimentação específica por ID (GET)
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<Alimentacao> GetAlimentacao(int id)
        {
            var alimentacao = _context.Alimentacoes.FirstOrDefault(a => a.Id == id);
            
            if (alimentacao == null)
                return NotFound(new { mensagem = "Alimentação não encontrada" });

            return Ok(alimentacao);
        }

        /// <summary>
        /// Obtém alimentações por usuário (GET) - Demonstra LINQ
        /// </summary>
        [HttpGet("usuario/{usuarioId}")]
        public ActionResult<IEnumerable<Alimentacao>> GetAlimentacoesPorUsuario(int usuarioId)
        {
            var alimentacoes = _context.Alimentacoes
                .Where(a => a.UsuarioId == usuarioId)
                .OrderByDescending(a => a.Data)
                .ToList();

            return Ok(alimentacoes);
        }

        /// <summary>
        /// Filtra alimentações por tipo de refeição (GET) - Demonstra LINQ
        /// </summary>
        [HttpGet("refeicao/{refeicao}")]
        public ActionResult<IEnumerable<Alimentacao>> GetAlimentacoesPorRefeicao(string refeicao)
        {
            var alimentacoes = _context.Alimentacoes
                .Where(a => a.Refeicao.ToLower() == refeicao.ToLower())
                .ToList();

            return Ok(alimentacoes);
        }

        /// <summary>
        /// Cria uma nova alimentação (POST)
        /// </summary>
        [HttpPost]
        public ActionResult<Alimentacao> CreateAlimentacao([FromBody] Alimentacao alimentacao)
        {
            if (!alimentacao.Validar())
                return BadRequest(new { mensagem = "Dados da alimentação inválidos" });

            alimentacao.Id = _context.Alimentacoes.Any() ? _context.Alimentacoes.Max(a => a.Id) + 1 : 1;
            alimentacao.Data = DateTime.Now;

            _context.Alimentacoes.Add(alimentacao);
            _context.SalvarDados();

            return CreatedAtAction(nameof(GetAlimentacao), new { id = alimentacao.Id }, alimentacao);
        }

        /// <summary>
        /// Atualiza uma alimentação (PUT)
        /// </summary>
        [HttpPut("{id}")]
        public ActionResult<Alimentacao> UpdateAlimentacao(int id, [FromBody] Alimentacao alimentacaoAtualizada)
        {
            var alimentacao = _context.Alimentacoes.FirstOrDefault(a => a.Id == id);
            
            if (alimentacao == null)
                return NotFound(new { mensagem = "Alimentação não encontrada" });

            alimentacao.Refeicao = alimentacaoAtualizada.Refeicao;
            alimentacao.Descricao = alimentacaoAtualizada.Descricao;
            alimentacao.Calorias = alimentacaoAtualizada.Calorias;
            alimentacao.Proteinas = alimentacaoAtualizada.Proteinas;
            alimentacao.Carboidratos = alimentacaoAtualizada.Carboidratos;
            alimentacao.Gorduras = alimentacaoAtualizada.Gorduras;

            _context.SalvarDados();

            return Ok(alimentacao);
        }

        /// <summary>
        /// Deleta uma alimentação (DELETE)
        /// </summary>
        [HttpDelete("{id}")]
        public ActionResult DeleteAlimentacao(int id)
        {
            var alimentacao = _context.Alimentacoes.FirstOrDefault(a => a.Id == id);
            
            if (alimentacao == null)
                return NotFound(new { mensagem = "Alimentação não encontrada" });

            _context.Alimentacoes.Remove(alimentacao);
            _context.SalvarDados();

            return Ok(new { mensagem = "Alimentação deletada com sucesso" });
        }

        /// <summary>
        /// Obtém estatísticas nutricionais (GET) - Demonstra LINQ avançado
        /// </summary>
        [HttpGet("estatisticas/{usuarioId}")]
        public ActionResult GetEstatisticas(int usuarioId)
        {
            var alimentacoes = _context.Alimentacoes
                .Where(a => a.UsuarioId == usuarioId)
                .ToList();

            if (!alimentacoes.Any())
                return Ok(new { mensagem = "Nenhuma alimentação encontrada" });

            var estatisticas = new
            {
                TotalRefeicoes = alimentacoes.Count,
                TotalCalorias = alimentacoes.Sum(a => a.Calorias),
                MediaCalorias = alimentacoes.Average(a => a.Calorias),
                TotalProteinas = alimentacoes.Sum(a => a.Proteinas),
                TotalCarboidratos = alimentacoes.Sum(a => a.Carboidratos),
                TotalGorduras = alimentacoes.Sum(a => a.Gorduras),
                RefeicoesPorTipo = alimentacoes
                    .GroupBy(a => a.Refeicao)
                    .Select(g => new { Refeicao = g.Key, Quantidade = g.Count() })
                    .ToList()
            };

            return Ok(estatisticas);
        }
    }
}
