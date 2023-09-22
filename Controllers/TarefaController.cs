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
        private readonly OrganizadorContext _context;
        private readonly ITarefaService _service;

        public TarefaController(OrganizadorContext context, ITarefaService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            // TODO: Buscar o Id no banco utilizando o EF
            // TODO: Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
            // caso contrário retornar OK com a tarefa encontrada
            var tarefa = await _service.GetByIdAsync(id);

            if (tarefa is null)
                return NotFound();

            return Ok(tarefa);
        }

        [HttpGet("ObterTodos")]
        public async Task<IActionResult> ObterTodos()
        {
            // TODO: Buscar todas as tarefas no banco utilizando o EF
            var tarefas = await _service.GetAllAsync();
            return Ok(tarefas);
        }

        [HttpGet("ObterPorTitulo")]
        public async Task<IActionResult> ObterPorTitulo(string titulo)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            // var tarefa = _context.Tarefas.Where(x => x.Titulo.Contains(titulo));
            var tarefa = await _service.ObterPorTitulo(titulo);
            return Ok(tarefa);
        }

        [HttpGet("ObterPorData")]
        public async Task<IActionResult> ObterPorData(DateTime data)
        {
            // var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
            var tarefa = await _service.ObterPorData(data);
            return Ok(tarefa);
        }

        [HttpGet("ObterPorStatus")]
        public async Task<IActionResult> ObterPorStatus(EnumStatusTarefa status)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            // var tarefa = _context.Tarefas.Where(x => x.Status == status);
            var tarefa = await _service.ObterPorStatus(status);
            return Ok(tarefa);
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

            // TODO: Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
            // TODO: Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes)
            var tarefaInserida = await _service.UpdateAsync(tarefa);
            return Ok(tarefaInserida);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            // var tarefaBanco = _context.Tarefas.Find(id);
            var tarefaBanco = await _service.GetByIdAsync(id);

            if (tarefaBanco == null)
                return NotFound();

            // TODO: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes)
            await _service.DeleteAsync(tarefaBanco.Id);
            return NoContent();
        }
    }
}
