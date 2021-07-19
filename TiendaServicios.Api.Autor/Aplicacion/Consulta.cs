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
    public class Consulta
    {
        public class EjecutaConsulta : IRequest<List<AutorDto>>
        {

        }

        public class Manejador : IRequestHandler<EjecutaConsulta, List<AutorDto>>
        {
            private readonly ContextoAutor _contexto;
            private readonly IMapper _mapper;
            public Manejador(ContextoAutor contexto, IMapper mapper)
            {
                this._contexto = contexto;
                this._mapper = mapper;
            }
            public async Task<List<AutorDto>> Handle(EjecutaConsulta request, CancellationToken cancellationToken)
            {
                var autores = await _contexto.AutorLibro.ToListAsync();

                var autorDto = _mapper.Map<List<AutorLibro>, List<AutorDto>>(autores);

                if(autorDto != null && autorDto.Count > 0)
                {
                    return autorDto;
                }

                throw new Exception("No se encontraron autores");
            }
        }
    }
}
