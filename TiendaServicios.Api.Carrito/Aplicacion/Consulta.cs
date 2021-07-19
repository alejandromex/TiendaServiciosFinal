using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Carrito.Persistencia;
using TiendaServicios.Api.Carrito.RemoteInterface;

namespace TiendaServicios.Api.Carrito.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta : IRequest<CarritoDto>
        {
            public int CarritoSesionId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDto>
        {
            private readonly CarritoContexto context;
            private readonly ILibroService libroService;
            public Manejador(CarritoContexto context, ILibroService libroService)
            {
                this.context = context;
                this.libroService = libroService;
            }
            public async Task<CarritoDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = await context.CarritoSesion.Where(x => x.CarritoSesionId == request.CarritoSesionId).FirstOrDefaultAsync();
                var carritoSesionDetalle = await context.CarritoSesionDetalles.Where(x => x.CarritoSesionId == request.CarritoSesionId).ToListAsync();

                List<CarritoDetalleDto> ListaCarritoDto = new List<CarritoDetalleDto>();

                foreach(var libro in carritoSesionDetalle)
                {
                    var response = await libroService.GetLibro(new Guid(libro.ProductoSeleccionado));
                    if (response.Resultado)
                    {
                        var objectoLibro = response.Libro;
                        var carritoDetalle = new CarritoDetalleDto()
                        {
                            FechaPublicacion = objectoLibro.FechaPublicacion,
                            TituloLibro = objectoLibro.Titulo,
                            LibroId = objectoLibro.LibreriaMaterialId
                        };
                        ListaCarritoDto.Add(carritoDetalle);
                    }
                }

                CarritoDto carritoSesionDto = new CarritoDto()
                {
                    Productos = ListaCarritoDto,
                    CarritoId = carritoSesion.CarritoSesionId,
                    FechaCreactionSesion = carritoSesion.FechaCreacion
                };

                return carritoSesionDto;
            }
        }
    }
}
