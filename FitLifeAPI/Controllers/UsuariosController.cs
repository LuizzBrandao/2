using Microsoft.AspNetCore.Mvc;
using FitLifeAPI.Servicos;
using FitLifeAPI.DTOs;

namespace FitLifeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IServicoUsuario _servico;

        public UsuariosController(IServicoUsuario servico)
        {
            _servico = servico;
        }

        // GET: api/usuarios
        [HttpGet]
        public async Task<ActionResult<List<UsuarioDTO>>> ObterTodos()
        {
            var usuarios = await _servico.ObterTodosAsync();
            return Ok(usuarios);
        }

        // GET: api/usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> ObterPorId(int id)
        {
            var usuario = await _servico.ObterPorIdAsync(id);
            if (usuario == null)
                return NotFound($"Usuário com ID {id} não encontrado.");

            return Ok(usuario);
        }

        // POST: api/usuarios
        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> Criar([FromBody] CriarUsuarioDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = await _servico.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = usuario.Id }, usuario);
        }

        // PUT: api/usuarios/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioDTO>> Atualizar(int id, [FromBody] AtualizarUsuarioDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var atualizado = await _servico.AtualizarAsync(id, dto);
            if (atualizado == null)
                return NotFound($"Usuário com ID {id} não encontrado.");

            return Ok(atualizado);
        }

        // DELETE: api/usuarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Deletar(int id)
        {
            var sucesso = await _servico.DeletarAsync(id);
            if (!sucesso)
                return NotFound($"Usuário com ID {id} não encontrado.");

            return NoContent();
        }
    }
}