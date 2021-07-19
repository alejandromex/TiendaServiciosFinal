using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.Carrito.Aplicacion
{
    public class CarritoDto
    {
        public int CarritoId { get; set; }
        public DateTime? FechaCreactionSesion { get; set; }
        public List<CarritoDetalleDto> Productos { get; set; }
    }
}
