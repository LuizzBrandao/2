using Microsoft.AspNetCore.Mvc;
using FitLifeAPI.Servicos;
using FitLifeAPI.DTOs;

namespace FitLifeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreinosController : ControllerBase
    {
        private readonly IServicoTreino _servico;

        public TreinosController(IServicoTreino servico)
        {
            _servico = servico;
        }

        // GET: api/treinos/usuario/5
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<List<TreinoDTO>>> ObterPorUsuario(int usuarioId)
        {
            var treinos = await _servico.ObterTodosPorUsuarioAsync(usuarioId);
            return Ok(treinos);
        }

        // POST: api/treinos/cardio
        [HttpPost("cardio")]
        public async Task<ActionResult<TreinoDTO>> CriarCardio([FromBody] CriarTreinoCardioDTO dto)
        {
            var treino = await _servico.CriarTreinoCardioAsync(dto);
            return CreatedAtAction(nameof(ObterPorUsuario), new { usuarioId = dto.UsuarioId }, treino);
        }

        // POST: api/treinos/forca
        [HttpPost("forca")]
        public async Task<ActionResult<TreinoDTO>> CriarForca([FromBody] CriarTreinoForcaDTO dto)
        {
            var treino = await _servico.CriarTreinoForcaAsync(dto);
            return CreatedAtAction(nameof(ObterPorUsuario), new { usuarioId = dto.UsuarioId }, treino);
        }

        // DELETE: api/treinos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Deletar(int id)
        {
            var sucesso = await _servico.DeletarAsync(id);
            if (!sucesso)
                return NotFound($"Treino com ID {id} não encontrado.");

            return NoContent();
        }
    }
}