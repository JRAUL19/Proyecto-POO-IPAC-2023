using SistemaGestionEventos.Models;

namespace SistemaGestionEventos.Servicios
{
    public interface IRepositorioUbicacion
    {
        Task Crear(Ubicacion ubicacion);
        Task Editar(UbicacionEditarViewModel modelo);
        Task Eliminar(int id);
        Task<bool> Existe(string nombre);
        Task<IEnumerable<Ubicacion>> Obtener();
        Task<Ubicacion> ObtenerId(int id);
    }
}
