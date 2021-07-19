using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Aplicacion;

namespace TiendaServicios.Api.Libro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly IMediator mediator;
        public LibroController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CreateLibro(Nuevo.EjecutaNuevo data)
        {
            return await mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<LibroMaterialDto>>> GetAllLibros()
        {
            return await mediator.Send(new Consulta.EjecutaConsulta());
        }

        [HttpGet("search")]
        public async Task<ActionResult<LibroMaterialDto>> SearchLibro(Guid libro)
        {
            return await mediator.Send(new ConsultaFiltro.EjecutaFiltro() { LibroGuid = libro });
        }
    }
}
