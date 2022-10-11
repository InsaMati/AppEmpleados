using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTO;
using Core.Entidades;

namespace API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<compania,CompaniaDTO>().ReverseMap();

            // Mapeo empleados
            CreateMap<empleado,EmpleadoUpsertDto>().ReverseMap();
            CreateMap<empleado,EmpleadoReadDto>()
                .ForMember(e => e.Compania, m => m.MapFrom(c => c.compania.Nombre));

        }
    }
}