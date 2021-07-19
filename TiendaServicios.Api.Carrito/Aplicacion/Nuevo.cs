using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Carrito.Modelo;
using TiendaServicios.Api.Carrito.Persistencia;

namespace TiendaServicios.Api.Carrito.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public DateTime? FechaCreacion { get; set; }
            public List<string> Productos { get; set; }
        }

        public class Validator : AbstractValidator<Ejecuta>
        {
            public Validator()
            {
                RuleFor(x => x.FechaCreacion).NotEmpty();
                RuleFor(x => x.Productos).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CarritoContexto context;
            public Manejador(CarritoContexto context)
            {
                this.context = context;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var CarritoSesion = new CarritoSesion()
                {
                    FechaCreacion = request.FechaCreacion
                };

                context.CarritoSesion.Add(CarritoSesion);

                var res = await context.SaveChangesAsync();
                if(res == 0)
                {
                    throw new Exception("Error al crear la sesion del carrito");
                }

                int id = CarritoSesion.CarritoSesionId;
                
                
                foreach(string producto in request.Productos)
                {
                    var detalleSesion = new CarritoSesionDetalle()
                    {
                        CarritoSesionId = id,
                        FechaCreacion = DateTime.Now,
                        ProductoSeleccionado = producto
                    };

                    context.CarritoSesionDetalles.Add(detalleSesion);
                }

                res = await context.SaveChangesAsync();
                if(res > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Erro al generar el carrito de compra");
            }
        }


    }
}
