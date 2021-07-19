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
    public class ConsultaFiltro
    {
        public class EjecutaFiltro : IRequest<LibroMaterialDto>
        {
            public Guid LibroGuid { get; set; }
        }

        public class Validator : AbstractValidator<EjecutaFiltro>
        {
            public Validator()
            {
                RuleFor(x => x.LibroGuid).NotEmpty().WithMessage("El identificador del libro es necesario");
            }
        }

        public class Manejador : IRequestHandler<EjecutaFiltro, LibroMaterialDto>
        {
            private readonly ContextoLibreria context;
            private readonly IMapper mapper;
            public Manejador(ContextoLibreria context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }
            public async Task<LibroMaterialDto> Handle(EjecutaFiltro request, CancellationToken cancellationToken)
            {
                var libro = await context.LibreriaMaterial.Where(x => x.LibreriaMaterialId == request.LibroGuid).FirstOrDefaultAsync();

                if(libro != null)
                {
                    return mapper.Map<LibreriaMaterial, LibroMaterialDto>(libro);
                }

                throw new Exception("No se encontro el libro");
            }
        }
    }
}
