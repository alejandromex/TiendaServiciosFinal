using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Carrito.RemoteModel;

namespace TiendaServicios.Api.Carrito.RemoteInterface
{
    public interface ILibroService
    {
        Task<(bool Resultado, LibroRemote Libro, string ErrorMessage)> GetLibro(Guid LibroId);

    }
}
