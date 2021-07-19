using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class Nuevo
    {
        public class EjecutaNuevo : IRequest
        {
            public string titulo { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public Guid? AutorLibro { get; set; }
        }


        public class Validator : AbstractValidator<EjecutaNuevo>
        {
            public Validator()
            {
                RuleFor(x => x.titulo).NotEmpty().WithMessage("El titulo es obligatorio");
            }
        }

        public class Manejador : IRequestHandler<EjecutaNuevo>
        {
            private readonly ContextoLibreria _context;
            public Manejador(ContextoLibreria context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(EjecutaNuevo request, CancellationToken cancellationToken)
            {
                LibreriaMaterial libreriaMaterial = new LibreriaMaterial()
                {
                    AutorLibro = request.AutorLibro,
                    Titulo = request.titulo,
                    FechaPublicacion = request.FechaPublicacion
                };

                _context.LibreriaMaterial.Add(libreriaMaterial);
                var resp = await _context.SaveChangesAsync();
                if(resp > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Error al crear el libro");
            }
        }
    }
}
