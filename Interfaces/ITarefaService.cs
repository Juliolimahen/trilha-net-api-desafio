using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrilhaApiDesafio.Dtos;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Interfaces
{
    public interface ITarefaService
    {
        Task<IEnumerable<TarefaDto>> GetAllAsync();
        Task<TarefaDto> GetByIdAsync(int id);
        Task<TarefaDto> InsertAsync(CriarTarefaDto dto);
        Task<TarefaDto> UpdateAsync(TarefaDto dto);
        Task<TarefaDto> DeleteAsync(int id);
        Task<IEnumerable<TarefaDto>> ObterPorTitulo(string titulo);
        Task<IEnumerable<TarefaDto>> ObterPorData(DateTime data);
        Task<IEnumerable<TarefaDto>> ObterPorStatus(EnumStatusTarefa status);
    }
}
