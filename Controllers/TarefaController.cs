using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Dtos;
using TrilhaApiDesafio.Interfaces;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _service;

        public TarefaController(OrganizadorContext context, ITarefaService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var tarefa = await _service.GetByIdAsync(id);
            return tarefa is null ? NotFound() : Ok(tarefa);
        }

        [HttpGet("ObterTodos")]
        public async Task<IActionResult> ObterTodos()
        {
            var tarefas = await _service.GetAllAsync();
            return tarefas is null ? NotFound() : Ok(tarefas);
        }

        [HttpGet("ObterPorTitulo")]
        public async Task<IActionResult> ObterPorTitulo(string titulo)
        {
            var tarefa = await _service.ObterPorTitulo(titulo);
            return tarefa is null ? NotFound() : Ok(tarefa);
        }

        [HttpGet("ObterPorData")]
        public async Task<IActionResult> ObterPorData(DateTime data)
        {
            var tarefa = await _service.ObterPorData(data);
            return Ok(tarefa);
        }

        [HttpGet("ObterPorStatus")]
        public async Task<IActionResult> ObterPorStatus(EnumStatusTarefa status)
        {
            var tarefa = await _service.ObterPorStatus(status);
            return tarefa is null ? NotFound() : Ok(tarefa);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarTarefaDto tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            var tarefaInserida = await _service.InsertAsync(tarefa);

            return CreatedAtAction(nameof(ObterPorId), new { id = tarefaInserida.Id }, tarefaInserida);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, TarefaDto tarefa)
        {
            // var tarefaBanco = _context.Tarefas.Find(id);
            var tarefaBanco = _service.GetByIdAsync(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            var tarefaInserida = await _service.UpdateAsync(tarefa);
            return Ok(tarefaInserida);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var tarefaBanco = await _service.GetByIdAsync(id);

            if (tarefaBanco == null)
                return NotFound();

            await _service.DeleteAsync(tarefaBanco.Id);
            return NoContent();
        }
    }
}
