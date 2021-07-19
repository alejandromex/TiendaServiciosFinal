using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Aplicacion;
using TiendaServicios.Api.Autor.Modelo;

namespace TiendaServicios.Api.Autor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator mediator;
        public AuthorController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Unit>> CreateAuthor(Nuevo.EjecutaNuevo data)
        {
            return await mediator.Send(data);
        }

        [HttpGet("list")]
        public async Task<List<AutorDto>> ListAuthor()
        {
            return await mediator.Send(new Consulta.EjecutaConsulta());
        }

        [HttpGet("unico")]
        public async Task<ActionResult<AutorDto>> AutorUnico(string guid)
        {
            return await mediator.Send(new ConsultaFiltro.AutorUnico() { guid = guid });
        }
    }
}
