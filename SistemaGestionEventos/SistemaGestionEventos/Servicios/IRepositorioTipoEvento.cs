using SistemaGestionEventos.Models;

namespace SistemaGestionEventos.Servicios
{
    public interface IRepositorioTipoEvento
    {
        Task Crear(TipoEvento tipoEvento);
        Task Eliminar(int id);
        Task<bool> Existe(string nombre);
        Task<IEnumerable<TipoEvento>> Obtener();
        Task<TipoEvento> ObtenerPorId(int id);
    }
}