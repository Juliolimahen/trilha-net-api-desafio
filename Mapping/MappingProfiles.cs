using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TrilhaApiDesafio.Dtos;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Tarefa, TarefaDto>().ReverseMap();
            CreateMap<Tarefa, CriarTarefaDto>().ReverseMap();
        }
    }
}