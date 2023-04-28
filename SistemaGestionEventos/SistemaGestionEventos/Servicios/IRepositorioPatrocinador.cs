using SistemaGestionEventos.Models;

namespace SistemaGestionEventos.Servicios
{
    public interface IRepositorioPatrocinador
    {
        Task Crear(Patrocinador patrocinador);
        Task Editar(EditarPatrocinadorViewModel patrocinador);
        Task Eliminar(int id);
        Task<bool> Existe(string nombre);
        Task<IEnumerable<Patrocinador>> Obtener();
        Task<Patrocinador> ObtenerId(int id);
    }
}