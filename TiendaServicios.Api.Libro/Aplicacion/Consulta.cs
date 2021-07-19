using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class Consulta
    {
        public class EjecutaConsulta : IRequest<List<LibroMaterialDto>>
        {
        }


        public class Manejador : IRequestHandler<EjecutaConsulta, List<LibroMaterialDto>>
        {
            private readonly ContextoLibreria context;
            private readonly IMapper mapper;
            public Manejador(ContextoLibreria context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }
            public async Task<List<LibroMaterialDto>> Handle(EjecutaConsulta request, CancellationToken cancellationToken)
            {
                var libros = await context.LibreriaMaterial.ToListAsync();

                var libroDto = mapper.Map<List<LibreriaMaterial>, List<LibroMaterialDto>>(libros);

                if(libros != null && libros.Count > 0)
                {
                    return libroDto;
                }

                throw new Exception("No se encontraron los libros");
            }
        }


    }
}
