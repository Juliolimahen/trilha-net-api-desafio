using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TrilhaApiDesafio.Dtos;
using TrilhaApiDesafio.Interfaces;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository _repository;
        private readonly IMapper _mapper;

        public TarefaService(ITarefaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TarefaDto> DeleteAsync(int id)
        {
            var tarefa = await _repository.RemoveAsync(id);
            return _mapper.Map<TarefaDto>(tarefa);
        }

        public async Task<IEnumerable<TarefaDto>> GetAllAsync()
        {
            var tarefas = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TarefaDto>>(tarefas);
        }

        public async Task<TarefaDto> GetByIdAsync(int id)
        {
            var tarefa = await _repository.GetByIdAsync(id);
            return _mapper.Map<TarefaDto>(tarefa);
        }

        public async Task<TarefaDto> InsertAsync(CriarTarefaDto tarefaDto)
        {
            var tarefa = _mapper.Map<Tarefa>(tarefaDto);
            tarefa = await _repository.AddAsync(tarefa);
            return _mapper.Map<TarefaDto>(tarefa);
        }

        public async Task<IEnumerable<TarefaDto>> ObterPorData(DateTime data)
        {
            var tarefa = await _repository.FindAsync(t => t.Data.Date.Equals(data.Date));
            return _mapper.Map<IEnumerable<TarefaDto>>(tarefa);
        }

        public async Task<IEnumerable<TarefaDto>> ObterPorStatus(EnumStatusTarefa status)
        {
            var tarefa = await _repository.FindAsync(t => t.Status.Equals(status));
            return _mapper.Map<IEnumerable<TarefaDto>>(tarefa);
        }

        public async Task<IEnumerable<TarefaDto>> ObterPorTitulo(string titulo)
        {
            var tarefa = await _repository.FindAsync(t => t.Titulo.Contains(titulo));
            return _mapper.Map<IEnumerable<TarefaDto>>(tarefa);
        }

        public async Task<TarefaDto> UpdateAsync(TarefaDto tarefaDto)
        {
            var tarefa = _mapper.Map<Tarefa>(tarefaDto);
            tarefa = await _repository.UpdateAsync(tarefa);
            return _mapper.Map<TarefaDto>(tarefa);
        }
    }
}