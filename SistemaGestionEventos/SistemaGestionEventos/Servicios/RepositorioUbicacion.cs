using Dapper;
using Microsoft.Data.SqlClient;
using SistemaGestionEventos.Models;

namespace SistemaGestionEventos.Servicios
{
    public class RepositorioUbicacion : IRepositorioUbicacion
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        //Conectar a base de datos
        public RepositorioUbicacion(IConfiguration configuration) 
        {
            this.configuration = configuration;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //Obtener datos de ubicaciones
        public async Task<IEnumerable<Ubicacion>> Obtener() 
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Ubicacion>
                (@"SELECT Id,Nombre,Direccion,DireccionUrl,CapacidadPersonas,Servicios,Descripcion
                FROM Ubicaciones");
        }


        //Obtener Id de la ubicacion
        public async Task<Ubicacion> ObtenerId(int id) 
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Ubicacion>
                (@"SELECT Id,Nombre,Direccion,DireccionUrl,CapacidadPersonas,Servicios,Descripcion
                FROM Ubicaciones WHERE Id = @Id", new { id });
        }

        //Verificar la si la ubicacion existe
        public async Task<bool> Existe(string nombre) 
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>
                (@"SELECT 1 FROM Ubicaciones WHERE Nombre = @Nombre", new { nombre });
            return existe == 1;
        }

        //Crear nueva ubicacion

        public async Task Crear(Ubicacion ubicacion) 
        {
            using var connection = new SqlConnection(connectionString);
            var crear = await connection.QuerySingleAsync<int>
                ("Ubicacion_Insertar",
                new 
                {
                    Nombre = ubicacion.Nombre,
                    Direccion = ubicacion.Direccion,
                    DireccionUrl = ubicacion.DireccionUrl,
                    CapacidadPersonas = ubicacion.CapacidadPersonas,
                    Servicios = ubicacion.Servicios,
                    Descripcion = ubicacion.Descripcion,
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        //Eliminar una ubicacion
        public async Task Eliminar(int id) 
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"DELETE FROM Ubicaciones WHERE Id = @Id;", new { id });
        }

        //Editar una ubicacion
        public async Task Editar(UbicacionEditarViewModel ubicacion) 
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync
                (@"
                UPDATE Ubicaciones 
                SET Nombre = @Nombre,
	                Direccion = @Direccion,
	                DireccionUrl = @DireccionUrl,
	                CapacidadPersonas = @CapacidadPersonas,
	                Servicios = @Servicios,
	                Descripcion = @Descripcion
                WHERE Id = @Id", ubicacion);
        }

    }
}
