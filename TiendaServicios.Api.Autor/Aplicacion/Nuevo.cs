using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class Nuevo
    {
        public class EjecutaNuevo : IRequest
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set; }
        }

        public class Validator : AbstractValidator<EjecutaNuevo>
        {
            public Validator()
            {
                RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre es requerido");
                RuleFor(x => x.Apellido).NotEmpty().WithMessage("El appelido es requerido");
            }
        }

        public class Manejador : IRequestHandler<EjecutaNuevo>
        {

            private readonly ContextoAutor _context;
            public Manejador(ContextoAutor context)
            {
                this._context = context;
            }
            public async Task<Unit> Handle(EjecutaNuevo request, CancellationToken cancellationToken)
            {
                AutorLibro autor = new AutorLibro
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    FechaNacimiento = request.FechaNacimiento
                };

                _context.AutorLibro.Add(autor);
                var res = await _context.SaveChangesAsync();

                if(res > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Error al crear autor");
            }
        }
    }
}
