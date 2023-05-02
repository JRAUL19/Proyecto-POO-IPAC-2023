using SistemaGestionEventos.Models;

namespace SistemaGestionEventos.Servicios
{
    public interface IRepositorioEvento
    {
        Task Crear(Evento evento);
        Task Eliminar(int id);
        Task<bool> Existe(string nombre);
        Task<IEnumerable<Evento>> Obtener();
        Task<Evento> ObtenerId(int id);
    }
}