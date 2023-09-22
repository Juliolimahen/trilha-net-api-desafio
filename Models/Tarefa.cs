using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrilhaApiDesafio.Models
{
    public class Tarefa : Entity
    {
        public Tarefa() { }
        public Tarefa(string titulo, string descricao, DateTime data, EnumStatusTarefa status)
        {
            Titulo = titulo;
            Descricao = descricao;
            Data = data;
            Status = status;
        }

        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public EnumStatusTarefa Status { get; set; }
    }
}