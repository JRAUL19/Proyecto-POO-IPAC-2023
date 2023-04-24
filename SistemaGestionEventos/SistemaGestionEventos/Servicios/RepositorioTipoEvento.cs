using Dapper;
using Microsoft.Data.SqlClient;
using SistemaGestionEventos.Models;

namespace SistemaGestionEventos.Servicios
{
    public class RepositorioTipoEvento : IRepositorioTipoEvento
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        //Conectar con base de datos
        public RepositorioTipoEvento(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //Obtener valores de tipo evento
        public async Task<IEnumerable<TipoEvento>> Obtener() 
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<TipoEvento>
                (@"SELECT Id, Nombre, Descripcion
                FROM TiposEventos");
        }

        //Obtener Id de tipo de evento
        public async Task<TipoEvento> ObtenerPorId(int id) 
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<TipoEvento>
                (@"SELECT Id,Nombre,Descripcion FROM TiposEventos WHERE Id = @Id", new { id });
        }

        //Crear nuevo tipo de evento
        public async Task Crear (TipoEvento tipoEvento) 
        {
            using var connection = new SqlConnection(connectionString);
            var crear = await connection.QuerySingleAsync<int>
                ("TipoEvento_Insertar",
                new { Nombre = tipoEvento.Nombre, Descripcion = tipoEvento.Descripcion },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        //Eliminar un tipo de evento
        public async Task Eliminar(int id) 
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"DELETE FROM TiposEventos WHERE Id = @Id;", new { id });
        }

        //Verificar si el tipo de evento ya existe
        public async Task<bool> Existe(string nombre) 
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>
                (@"SELECT 1 FROM TiposEventos WHERE Nombre = @Nombre",new { nombre });
            return existe == 1;
        }

    }
}
