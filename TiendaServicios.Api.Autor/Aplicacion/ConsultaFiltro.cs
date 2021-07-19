using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class ConsultaFiltro
    {
        public class AutorUnico : IRequest<AutorDto>
        {
            public string guid { get; set; }
        }

        public class Manejador : IRequestHandler<AutorUnico, AutorDto>
        {

            private readonly ContextoAutor _context;
            private readonly IMapper _mapper;
            public Manejador(ContextoAutor context, IMapper mapper)
            {
                this._context = context;
                this._mapper = mapper;
            }
            public async Task<AutorDto> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autor = await _context.AutorLibro.Where(x => x.AutorLibroGuid == request.guid).FirstOrDefaultAsync();

                var autorDto = _mapper.Map<AutorLibro, AutorDto>(autor);

                if(autorDto != null)
                {
                    return autorDto;
                }

                throw new Exception("No se encontro el autor del libro");


            }
        }
    }
}
