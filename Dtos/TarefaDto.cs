using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrilhaApiDesafio.Dtos
{
    public class TarefaDto : CriarTarefaDto
    {
        public int Id { get; set; }

        public static implicit operator Task<object>(TarefaDto v)
        {
            throw new NotImplementedException();
        }
    }
}